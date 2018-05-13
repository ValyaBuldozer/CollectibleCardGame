using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectibleCardGame.ViewModels.UserControls
{
    class PlayerUserControlViewModel:BaseViewModel
    {
        private string _playerName;
        private string _heroName;
        private int _manaСurrent;
        private int _manaMax;
        private int _cardsInHand;
        private int _cardsInDeck;
       


        public string PlayerName
        {
            get => _playerName;
            set
            {
                _playerName = value;
                NotifyPropertyChanged(nameof(PlayerName));
            }
        }

        public string HeroName
        {
            get => _heroName;
            set
            {
                _heroName = value;
                NotifyPropertyChanged(nameof(HeroName));
            }
        }

        public int ManaСurrent
        {
            get => _manaСurrent;
            set
            {
                _manaСurrent = value;
                NotifyPropertyChanged(nameof(ManaСurrent));
            }
        }

        public int ManaMax
        {
            get => _manaMax;
            set
            {
                _manaMax = value;
                NotifyPropertyChanged(nameof(ManaMax));
            }
        }

        public int CardsInHand
        {
            get => _cardsInHand;
            set
            {
                _cardsInHand = value;
                NotifyPropertyChanged(nameof(CardsInHand));
            }
        }
        public int CardsInDeck
        {
            get => _cardsInDeck;
            set
            {
                _cardsInDeck = value;
                NotifyPropertyChanged(nameof(CardsInDeck));
            }
        }

       
    }
}
