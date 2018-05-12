using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using CollectibleCardGame.Models;
using CollectibleCardGame.Services;
using CollectibleCardGame.ViewModels.Elements;
using GameData.Models;
using GameData.Models.Cards;
using GameData.Models.PlayerTurn;
using GameData.Models.Units;

namespace CollectibleCardGame.ViewModels.Frames
{
    public class GameEngineViewModel : BaseViewModel
    {
        private readonly CurrentUser _user;

        private Player _player;
        private Player _enemyPlayer;
        private string _currentPlayerUsername;
        private PlayerMana _playerMana;
        private HeroUnit _playerHeroUnit;
        private HeroUnit _enemyHeroUnit;
        private ObservableCollection<CardViewModel> _playerCards;
        private ObservableCollection<CardViewModel> _enemyCards;
        private ObservableCollection<UnitViewModel> _playerUnits;
        private ObservableCollection<UnitViewModel> _enemyUnits;

        private RelayCommand _cardDeployCommand;
        private RelayCommand _transferTurnCommand;

        public Dispatcher CurrentDispatcher { get; }

        public Player Player
        {
            get => _player;
            set
            {
                _player = value;
                PlayerHeroUnit = value?.HeroUnit;
                NotifyPropertyChanged(nameof(Player));
            }
        }

        public Player EnemyPlayer
        {
            get => _enemyPlayer;
            set
            {
                _enemyPlayer = value;
                EnemyHeroUnit = value?.HeroUnit;
                NotifyPropertyChanged(nameof(EnemyPlayer));
            }
        }

        public string CurrentPlayerUsername
        {
            get => _currentPlayerUsername;
            set
            {
                _currentPlayerUsername = value;
                NotifyPropertyChanged(nameof(CurrentPlayerUsername));
                NotifyPropertyChanged(nameof(TransferTurnCommand));
            }
        }

        public PlayerMana PlayerMana
        {
            get => _playerMana;
            set
            {
                _playerMana = value;
                NotifyPropertyChanged(nameof(PlayerMana));
            }
        }

        public HeroUnit PlayerHeroUnit
        {
            get => _playerHeroUnit;
            set
            {
                _playerHeroUnit = value;
                NotifyPropertyChanged(nameof(PlayerHeroUnit));
            }
        }

        public HeroUnit EnemyHeroUnit
        {
            get => _enemyHeroUnit;
            set
            {
                _enemyHeroUnit = value;
                NotifyPropertyChanged(nameof(EnemyHeroUnit));
            }
        }

        public ObservableCollection<CardViewModel> PlayerCards
        {
            get => _playerCards;
            set
            {
                _playerCards = value;
                NotifyPropertyChanged(nameof(PlayerCards));
            }
        }

        public ObservableCollection<CardViewModel> EnemyCards
        {
            get => _enemyCards;
            set
            {
                _enemyCards = value;
                NotifyPropertyChanged(nameof(EnemyCards));
            }
        }

        public ObservableCollection<UnitViewModel> PlayerUnits
        {
            get => _playerUnits;
            set
            {
                _playerUnits = value;
                NotifyPropertyChanged(nameof(PlayerUnits));
            }
        }

        public ObservableCollection<UnitViewModel> EnemyUnits
        {
            get => _enemyUnits;
            set
            {
                _enemyUnits = value;
                NotifyPropertyChanged(nameof(EnemyUnits));
            }
        }

        public GameEngineViewModel(CurrentUser user)
        {
            CurrentDispatcher = Dispatcher.CurrentDispatcher;

            PlayerCards = new ObservableCollection<CardViewModel>();
            EnemyCards = new ObservableCollection<CardViewModel>();
            PlayerUnits = new ObservableCollection<UnitViewModel>();
            EnemyUnits  = new ObservableCollection<UnitViewModel>();
            _user = user;
        }

        public event EventHandler<PlayerTurnRequestEventArgs> PlayerTurnEvent;

        public RelayCommand CardDeployCommand => _cardDeployCommand ??
               (_cardDeployCommand = new RelayCommand(o =>
               {
                   if(!(o is CardViewModel cardViewModel))
                       return;
                   PlayerTurnEvent?.Invoke(this,new PlayerTurnRequestEventArgs(new CardDeployPlayerTurn(
                       _player,cardViewModel.Card)));
               }));

        public RelayCommand TransferTurnCommand => _transferTurnCommand ??
                           (_transferTurnCommand = new RelayCommand(o =>
                           {
                                var playerTurn = new EndPlayerTurn(null);
                               PlayerTurnEvent?.Invoke(this,new PlayerTurnRequestEventArgs(playerTurn));
                           },
                               c=>_currentPlayerUsername == _user?.Username));
    }
}
