using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseNetworkArchitecture.Common;
using CollectibleCardGame.Network.Controllers;
using CollectibleCardGame.Services;
using CollectibleCardGame.ViewModels.Elements;
using CollectibleCardGame.ViewModels.UserControls;
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
        private readonly Lazy<INetworkController> _networkController;
        private readonly ILogger _logger;

        private RelayCommand _confirmDeckCommand;

        public ObservableCollection<DeckTabItem> TabItems { get; }

        //todo : УБРАТЬ LAZY - ввести уровень абстракции, отпарвлять не на прямую
        public DeckSettingsViewModel(IDataRepositoryController<Card> cardRepositoryController,
            Lazy<INetworkController> networkController,ILogger logger)
        {
            _logger = logger;
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

            TabItems = new ObservableCollection<DeckTabItem>()
            {
                new DeckTabItem("Север",new DeckViewModel(northCards.Concat(commonCards),Fraction.North)),
                new DeckTabItem("Юг",new DeckViewModel(southCards.Concat(commonCards),Fraction.South)),
                new DeckTabItem("Монстры",new DeckViewModel(darkCards.Concat(commonCards),Fraction.Dark))
            };
        }

        public RelayCommand ConfirmDeckCommand => _confirmDeckCommand ?? (_confirmDeckCommand = new RelayCommand(
                            o =>
                            {
                                if(!(o is DeckViewModel viewModel)) return;

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
                                viewModel.DeckCards.ForEach(c=>deckIds.Add(c.Card.ID));

                                _networkController.Value.SendMessage(new MessageBase(MessageBaseType.SetDeckMesage,
                                    new SetDeckMessage()
                                    {
                                        Fraction = viewModel.Fraction,
                                        HeroCardId = viewModel.HeroUnit.BaseUnit.BaseCard.ID,
                                        DeckIDs = deckIds.ToArray()
                                    }));
                            }));
    }

    public class DeckTabItem
    {
        public string Title { get; }

        public DeckViewModel ViewModel { get; }

        public DeckTabItem(string title, DeckViewModel viewModel)
        {
            Title = title;
            ViewModel = viewModel;
        }
    }
}
