using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectibleCardGame.ViewModels.Elements;
using GameData.Models;
using GameData.Models.Units;

namespace CollectibleCardGame.ViewModels.UserControls
{
    public class PlayerUserControlViewModel:BaseViewModel
    {
        private string _playerName;
        private string _heroName;
        private int _manaСurrent;
        private int _manaMax;
        private int _cardsInHand;
        private int _cardsInDeck;

        private HeroUnit _heroUnit;
        private Player _player;
        private PlayerMana _playerMana;
        private UnitViewModel _unitViewModel;

        public HeroUnit HeroUnit
        {
            get => _heroUnit;
            set
            {
                _heroUnit = value;
                NotifyPropertyChanged(nameof(HeroUnit));

                if(value == null) return;

                HeroName = value.BaseCard?.Name;
                HeroUnitViewModel.BaseUnit = value;
                NotifyPropertyChanged(nameof(HeroName));
                NotifyPropertyChanged(nameof(HeroUnitViewModel));
                //HeroUnitViewModel = new UnitViewModel(_heroUnit);
            }
        }

        public UnitViewModel HeroUnitViewModel
        {
            get => _unitViewModel;
            set
            {
                _unitViewModel = value;
                NotifyPropertyChanged(nameof(HeroUnitViewModel));
            }
        }

        public Player Player
        {
            get => _player;
            set
            {
                if(value == null) return;

                _player = value;
                NotifyPropertyChanged(nameof(Player));

                HeroUnit = _player.HeroUnit;
                HeroName = _player.HeroUnit?.BaseCard?.Name;
                PlayerName = _player.Username;
                PlayerMana = _player.Mana;
            }
        }

        public PlayerMana PlayerMana
        {
            get => _playerMana;
            set
            {
                if(value == null) return;

                _playerMana = value;
                NotifyPropertyChanged(nameof(PlayerMana));
                NotifyPropertyChanged(nameof(ManaMax));
                NotifyPropertyChanged(nameof(ManaСurrent));
            }
        }

        public string PlayerName
        {
            get => _player?.Username;
            set
            {
                if(value == null) return;

                _player.Username = value;
                NotifyPropertyChanged(nameof(PlayerName));
            }
        }

        public string HeroName
        {
            get => _heroUnit?.BaseCard?.Name;
            set
            {
                if(value == null) return;

                _heroUnit.BaseCard.Name = value;
                NotifyPropertyChanged(nameof(HeroName));
            }
        }

        public int ManaСurrent
        {
            get => _playerMana.Current;
            set
            {
                _playerMana.Current = value;
                NotifyPropertyChanged(nameof(ManaСurrent));
            }
        }

        public int ManaMax
        {
            get => _playerMana.Base;
            set
            {
                _playerMana.Base = value;
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

        public PlayerUserControlViewModel()
        {
            _heroUnit = new HeroUnit(null,null);
            _playerMana = new PlayerMana();
            _player = new Player(null);
            _unitViewModel = new UnitViewModel();
        }

        public PlayerUserControlViewModel(Player player)
        {
            _unitViewModel = new UnitViewModel(player?.HeroUnit);
            _player = player;
            _heroUnit = player.HeroUnit;
            _playerMana = player.Mana;
        }
    }
}
