using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectibleCardGame.Services;
using CollectibleCardGame.ViewModels.Elements;
using CollectibleCardGame.ViewModels.UserControls;
using GameData.Controllers.Data;
using GameData.Enums;
using GameData.Models.Cards;

namespace CollectibleCardGame.ViewModels.Frames
{
    public class DeckSettingsViewModel : BaseViewModel
    {
        private readonly IDataRepositoryController<Card> _cardRepositoryController;

        private RelayCommand _addCardCommand;

        public ObservableCollection<DeckTabItem> TabItems { get; }

        public DeckSettingsViewModel(IDataRepositoryController<Card> cardRepositoryController)
        {
            _cardRepositoryController = cardRepositoryController;

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
                new DeckTabItem("Север",new DeckViewModel(northCards.Concat(commonCards))),
                new DeckTabItem("Юг",new DeckViewModel(southCards.Concat(commonCards))),
                new DeckTabItem("Монстры",new DeckViewModel(darkCards.Concat(commonCards)))
            };
        }
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
