using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Threading;
using BaseNetworkArchitecture.Common;
using CollectibleCardGame.Models;
using CollectibleCardGame.Network.Controllers;
using CollectibleCardGame.Services;
using CollectibleCardGame.ViewModels.UserControls;
using CollectibleCardGame.Views.Frames;
using GameData.Controllers.Data;
using GameData.Enums;
using GameData.Models.Cards;
using GameData.Network.Messages;
using Unity.Interception.Utilities;

namespace CollectibleCardGame.ViewModels.Frames
{
    public class DeckSettingsViewModel : BaseViewModel
    {
        private readonly IDataRepositoryController<Card> _cardRepositoryController;
        private readonly Dispatcher _dispatcher;
        private readonly ILogger _logger;
        private readonly Lazy<INetworkController> _networkController;
        private readonly CurrentUserService _userService;

        private RelayCommand _confirmDeckCommand;

        private Page _currentFramePage;

        //todo : УБРАТЬ LAZY - ввести уровень абстракции, отпарвлять не на прямую
        public DeckSettingsViewModel(IDataRepositoryController<Card> cardRepositoryController,
            Lazy<INetworkController> networkController, ILogger logger, CurrentUserService userService)
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
            _logger = logger;
            _userService = userService;
            _cardRepositoryController = cardRepositoryController;
            _networkController = networkController;

            var northCards =
                _cardRepositoryController.GetCollection().Where(c => c.Fraction == Fraction.North);
            var southCards
                = _cardRepositoryController.GetCollection().Where(c => c.Fraction == Fraction.South);
            var darkCards
                = _cardRepositoryController.GetCollection().Where(c => c.Fraction == Fraction.Dark);
            var commonCards
                = _cardRepositoryController.GetCollection().Where(c => c.Fraction == Fraction.Common);

            MenuItems = new ObservableCollection<DeckTabItem>
            {
                new DeckTabItem("Кальвария", new DeckFramePage
                {
                    ViewModel = new DeckViewModel(northCards.Concat(commonCards), Fraction.North,
                        _userService.GetDeckByFraction(
                            Fraction.North), _userService.GetHeroByFraction(Fraction.North))
                }),
                new DeckTabItem("Магдербург", new DeckFramePage
                {
                    ViewModel = new DeckViewModel(southCards.Concat(commonCards), Fraction.South,
                        _userService.GetDeckByFraction(
                            Fraction.South), _userService.GetHeroByFraction(Fraction.South))
                }),
                new DeckTabItem("Чудовища", new DeckFramePage
                {
                    ViewModel = new DeckViewModel(darkCards.Concat(commonCards), Fraction.Dark,
                        _userService.GetDeckByFraction(
                            Fraction.Dark), _userService.GetHeroByFraction(Fraction.Dark))
                })
            };

            //MenuItems = new ObservableCollection<DeckTabItem>()
            //{
            //    new DeckTabItem("Кальвария",new DeckViewModel(northCards.Concat(commonCards),Fraction.North)),
            //    new DeckTabItem("Магдербург",new DeckViewModel(southCards.Concat(commonCards),Fraction.South)),
            //    new DeckTabItem("Чудовища",new DeckViewModel(darkCards.Concat(commonCards),Fraction.Dark))
            //};
        }

        public ObservableCollection<DeckTabItem> MenuItems { get; }

        public DeckTabItem SelectedMenuItem
        {
            set => CurrentFramePage = value?.FramePage;
        }

        public Page CurrentFramePage
        {
            get => _currentFramePage;
            set
            {
                _currentFramePage = value;
                NotifyPropertyChanged(nameof(CurrentFramePage));
            }
        }

        public RelayCommand ConfirmDeckCommand => _confirmDeckCommand ?? (_confirmDeckCommand = new RelayCommand(
                                                      o =>
                                                      {
                                                          var viewModel = (CurrentFramePage as DeckFramePage)
                                                              ?.ViewModel;

                                                          if (viewModel.HeroUnit?.BaseUnit == null)
                                                          {
                                                              _logger.LogAndPrint("Выберите героя");
                                                              return;
                                                          }

                                                          if (viewModel.DeckCards.Count != 30)
                                                          {
                                                              _logger.LogAndPrint("Необходимо 30 карт в колоде");
                                                              return;
                                                          }

                                                          var deckIds = new List<int>();
                                                          viewModel.DeckCards.ForEach(c => deckIds.Add(c.Card.ID));

                                                          _networkController.Value.SendMessage(new MessageBase(
                                                              MessageBaseType.SetDeckMesage,
                                                              new SetDeckMessage
                                                              {
                                                                  Fraction = viewModel.Fraction,
                                                                  HeroCardId = viewModel.HeroUnit.BaseUnit.BaseCard.ID,
                                                                  DeckIDs = deckIds.ToArray()
                                                              }));
                                                      }));

        /// <summary>
        ///     Обновить колоды из UserService
        /// </summary>
        public void UpdateDecks()
        {
            //убрать этот метод
            var northCards =
                _cardRepositoryController.GetCollection().Where(c => c.Fraction == Fraction.North);
            var southCards
                = _cardRepositoryController.GetCollection().Where(c => c.Fraction == Fraction.South);
            var darkCards
                = _cardRepositoryController.GetCollection().Where(c => c.Fraction == Fraction.Dark);
            var commonCards
                = _cardRepositoryController.GetCollection().Where(c => c.Fraction == Fraction.Common);

            _dispatcher.Invoke(() =>
            {
                MenuItems.Clear();
                MenuItems.Add(new DeckTabItem("Кальвария", new DeckFramePage
                {
                    ViewModel = new DeckViewModel(northCards.Concat(commonCards), Fraction.North,
                        _userService.GetDeckByFraction(
                            Fraction.North), _userService.GetHeroByFraction(Fraction.North))
                }));
                MenuItems.Add(new DeckTabItem("Магдербург", new DeckFramePage
                {
                    ViewModel = new DeckViewModel(southCards.Concat(commonCards), Fraction.South,
                        _userService.GetDeckByFraction(
                            Fraction.South), _userService.GetHeroByFraction(Fraction.South))
                }));
                MenuItems.Add(new DeckTabItem("Чудовища", new DeckFramePage
                {
                    ViewModel = new DeckViewModel(darkCards.Concat(commonCards), Fraction.Dark,
                        _userService.GetDeckByFraction(
                            Fraction.Dark), _userService.GetHeroByFraction(Fraction.Dark))
                }));
            });
        }
    }

    public class DeckTabItem
    {
        public DeckTabItem(string title, Page framePage)
        {
            Title = title;
            FramePage = framePage;
        }

        public string Title { get; }

        public Page FramePage { get; }
    }
}