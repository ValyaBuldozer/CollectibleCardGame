using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CollectibleCardGame.Services;
using CollectibleCardGame.ViewModels.Elements;
using GameData.Models.Cards;
using Unity.Interception.Utilities;
using Xceed.Wpf.Toolkit;

namespace CollectibleCardGame.ViewModels.UserControls
{
    public class DeckViewModel : BaseViewModel
    {
        private string _fraction;

        private RelayCommand _dropToDeckCommand;

        public string Fraction
        {
            get => _fraction;
            set
            {
                _fraction = value;
                NotifyPropertyChanged(nameof(Fraction));
            }
        }

        public ObservableCollection<CardViewModel> Cards { set; get; }

        public ObservableCollection<CardViewModel> DeckCards { set; get; }

        public RelayCommand TransferTurnCommand => _dropToDeckCommand ??
                                                   (_dropToDeckCommand = new RelayCommand(o =>
       {
           if (!(o is CardViewModel viewModel)) return;

           if (DeckCards.Count(c => c.Card.ID == viewModel.Card.ID) > 2)
           {
               MessageBox.Show("Максмальное число одинковых карт 2");
               return;
           }

           DeckCards.Add(viewModel);
       }));

        public DeckViewModel(IEnumerable<Card> cards)
        {
            Cards = new ObservableCollection<CardViewModel>();
            cards.ForEach(c=>Cards.Add(new CardViewModel(c)));

            DeckCards = new ObservableCollection<CardViewModel>();
        }

        public List<Card> GetDeck()
        {
            var retList = new List<Card>();
            DeckCards.ForEach(vm => retList.Add(vm.Card));
            return retList;
        }
    }
}
