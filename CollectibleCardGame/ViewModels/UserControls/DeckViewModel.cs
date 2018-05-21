using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CollectibleCardGame.Services;
using CollectibleCardGame.ViewModels.Elements;
using GameData.Enums;
using GameData.Models.Cards;
using GameData.Models.Units;
using Unity.Interception.Utilities;
using Xceed.Wpf.Toolkit;

namespace CollectibleCardGame.ViewModels.UserControls
{
    public class DeckViewModel : BaseViewModel
    {
        private Fraction _fraction;
        private UnitViewModel _heroUnit;
        private bool _heroUnitVisibility;

        private RelayCommand _dropToDeckCommand;
        private RelayCommand _heroSelectionCommand;
        private RelayCommand _removeCardCommand;

        public Fraction Fraction
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

        public ObservableCollection<CardViewModel> HeroCards { set; get; }

        public UnitViewModel HeroUnit
        {
            get
            {
                HeroUnitVisibility = _heroUnit != null;
                return _heroUnit;
            }
            set
            {
                _heroUnit = value;
                NotifyPropertyChanged(nameof(HeroUnit));
            }
        }

        public bool HeroUnitVisibility
        {
            get => _heroUnitVisibility;
            set
            {
                _heroUnitVisibility = value;
                NotifyPropertyChanged(nameof(HeroUnitVisibility));
            }
        }

        public int DeckCount => DeckCards.Count;

        public RelayCommand DropToDeckCommand => _dropToDeckCommand ?? 
               (_dropToDeckCommand = new RelayCommand(o =>
               {
                   if (!(o is CardViewModel viewModel)) return;

                   if (DeckCards.Count(c => c.Card.ID == viewModel.Card.ID) >= 2)
                   {
                       MessageBox.Show("Нельзя добавить больше двух одинковых карт в колоду");
                       return;
                   }

                   DeckCards.Add(viewModel);
               }));

        public RelayCommand HeroSelectionCommand => _heroSelectionCommand ??
               (_heroSelectionCommand = new RelayCommand(o =>
               {
                   if(!(o is CardViewModel viewModel)) return;

                   HeroUnit = new UnitViewModel(new HeroUnit(null,viewModel.Card as UnitCard));
               }));

        public RelayCommand RemoveCardCommand => _removeCardCommand ??
               (_removeCardCommand = new RelayCommand(o =>
               {
                   if (!(o is CardViewModel viewModel)) return;

                   DeckCards.Remove(viewModel);
               }));

        public List<Card> GetDeck()
        {
            var retList = new List<Card>();
            DeckCards.ForEach(vm => retList.Add(vm.Card));
            return retList;
        }

        public DeckViewModel(IEnumerable<Card> cards,Fraction fraction)
        {
            _fraction = fraction;

            cards = cards.OrderBy(c => c.Cost);

            HeroUnit = null;
            Cards = new ObservableCollection<CardViewModel>();
            HeroCards = new ObservableCollection<CardViewModel>();
            cards.ForEach(c=>
            {
                //герои
                if(c.ID >= 3000)
                    HeroCards.Add(new CardViewModel(c));
                else if(c.ID < 1000)  //cлужебные карты
                    Cards.Add(new CardViewModel(c));
            });

            DeckCards = new ObservableCollection<CardViewModel>();
            DeckCards.CollectionChanged += (sender, args) => NotifyPropertyChanged(nameof(DeckCount));
        }

    }
}
