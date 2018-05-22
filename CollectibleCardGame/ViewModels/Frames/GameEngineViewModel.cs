using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Threading;
using CollectibleCardGame.Models;
using CollectibleCardGame.Services;
using CollectibleCardGame.ViewModels.Elements;
using CollectibleCardGame.ViewModels.UserControls;
using GameData.Enums;
using GameData.Models;
using GameData.Models.Cards;
using GameData.Models.PlayerTurn;
using GameData.Models.Units;
using Unity.Interception.Utilities;

namespace CollectibleCardGame.ViewModels.Frames
{
    public class GameEngineViewModel : BaseViewModel
    {
        private readonly CurrentUserService _userService;

        private RelayCommand _cardDeployCommand;
        private string _currentPlayerUsername;
        private ObservableCollection<CardViewModel> _enemyCards;
        private HeroUnit _enemyHeroUnit;
        private Player _enemyPlayer;
        private RelayCommand _enemyUnitCommand;
        private ObservableCollection<UnitViewModel> _enemyUnits;
        private PlayerUserControlViewModel _enemyViewModel;
        private bool _isAttackTargeting;

        private bool _isSpellTargeting;

        private Player _player;

        private ObservableCollection<CardViewModel> _playerCards;
        private HeroUnit _playerHeroUnit;
        private PlayerState _playerState;
        private RelayCommand _playerTargetCommand;
        private ObservableCollection<UnitViewModel> _playerUnits;

        private PlayerUserControlViewModel _playerViewModel;
        private RelayCommand _plyaerUnitCommand;
        private CardViewModel _spellTargetingViewModel;
        private RelayCommand _transferTurnCommand;
        private RelayCommand _unitTargetCommand;
        private UnitViewModel _unitTargetingViewModel;

        public GameEngineViewModel(CurrentUserService userService)
        {
            CurrentDispatcher = Dispatcher.CurrentDispatcher;

            _playerCards = new ObservableCollection<CardViewModel>();
            _playerCards.CollectionChanged +=
                (sender, args) => PlayerViewModel.CardsInHand = _playerCards.Count;
            _enemyCards = new ObservableCollection<CardViewModel>();
            _enemyCards.CollectionChanged +=
                (sender, args) => EnemyViewModel.CardsInHand = _enemyCards.Count;

            _playerUnits = new ObservableCollection<UnitViewModel>();
            _enemyUnits = new ObservableCollection<UnitViewModel>();

            _playerViewModel = new PlayerUserControlViewModel();
            _enemyViewModel = new PlayerUserControlViewModel();
            _userService = userService;
        }

        public Dispatcher CurrentDispatcher { get; }

        public Player Player
        {
            get => _player;
            set
            {
                _player = value;
                PlayerHeroUnit = value?.HeroUnit;
                NotifyPropertyChanged(nameof(Player));
                PlayerViewModel.Player = value;

                if (value == null) return;

                PlayerCards.Clear();
                _player.HandCards.ForEach(c => PlayerCards.Add(new CardViewModel(c)));

                PlayerUnits.Clear();
                _player.TableUnits.ForEach(u => PlayerUnits.Add(new UnitViewModel(u)));
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
                EnemyViewModel.Player = value;

                if (value == null) return;

                EnemyCards.Clear();
                _enemyPlayer.HandCards.ForEach(c => EnemyCards.Add(new CardViewModel(c)));

                EnemyUnits.Clear();
                _enemyPlayer.TableUnits.ForEach(u => EnemyUnits.Add(new UnitViewModel(u)));
            }
        }

        public PlayerUserControlViewModel PlayerViewModel
        {
            get => _playerViewModel;
            set
            {
                _playerViewModel = value;
                NotifyPropertyChanged(nameof(PlayerViewModel));
            }
        }

        public PlayerUserControlViewModel EnemyViewModel
        {
            get => _enemyViewModel;
            set
            {
                _enemyViewModel = value;
                NotifyPropertyChanged(nameof(EnemyViewModel));
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

                if (_currentPlayerUsername == _userService.Username)
                    _playerUnits.ForEach(u => u.IsCanAttack = true);
                else
                    _playerUnits.ForEach(u => u.IsCanAttack = false);

                PlayerUnits.ToList().ForEach(u => u.ResetTargeting());
                EnemyUnits.ToList().ForEach(u => u.ResetTargeting());
            }
        }

        public PlayerState PlayerState
        {
            get => _playerState;
            set
            {
                _playerState = value;
                NotifyPropertyChanged(nameof(PlayerState));
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

        public RelayCommand PlayerUnitCommand => _plyaerUnitCommand ??
                                                 (_plyaerUnitCommand = new RelayCommand(o =>
                                                 {
                                                     if (!IsPlayerTurn) return;

                                                     UnitViewModel unitViewModel;
                                                     if (o is UnitViewModel model)
                                                         unitViewModel = model;
                                                     else
                                                         unitViewModel = ((PlayerUserControlViewModel) o)
                                                             .HeroUnitViewModel;

                                                     if (_isSpellTargeting)
                                                     {
                                                         //todo : проверка героя
                                                         PlayerUnits.ForEach(u => u.ResetTargeting());
                                                         EnemyUnits.ForEach(u => u.ResetTargeting());
                                                         PlayerViewModel.HeroUnitViewModel.ResetTargeting();
                                                         EnemyViewModel.HeroUnitViewModel.ResetTargeting();
                                                         PlayerTurnEvent?.Invoke(this, new PlayerTurnRequestEventArgs(
                                                             new CardDeployPlayerTurn(
                                                                 _player, _spellTargetingViewModel.Card,
                                                                 unitViewModel.BaseUnit)));
                                                         _unitTargetingViewModel = null;
                                                         _isSpellTargeting = false;
                                                         return;
                                                     }

                                                     if (!unitViewModel.IsCanAttack)
                                                         return;

                                                     if (o is PlayerUserControlViewModel) return;

                                                     _unitTargetingViewModel = unitViewModel;
                                                     _isAttackTargeting = true;

                                                     //выделяем юнитов которых можно атаковать
                                                     if (EnemyUnits.ToList().Exists(u =>
                                                         u.BaseUnit.State.AttackPriority == 2))
                                                     {
                                                         EnemyUnits.Where(u => u.BaseUnit.State.AttackPriority == 2)
                                                             .ForEach(u => u.SetTargeting());
                                                     }
                                                     else
                                                     {
                                                         EnemyUnits.Where(u => u.BaseUnit.State.AttackPriority != 0)
                                                             .ForEach(u => u.SetTargeting());
                                                         EnemyViewModel.HeroUnitViewModel.SetTargeting();
                                                     }
                                                 }));

        public RelayCommand EnemyUnitCommand => _enemyUnitCommand ??
                                                (_enemyUnitCommand = new RelayCommand(o =>
                                                {
                                                    UnitViewModel unitViewModel;
                                                    if (o is UnitViewModel model)
                                                        unitViewModel = model;
                                                    else
                                                        unitViewModel = ((PlayerUserControlViewModel) o)
                                                            .HeroUnitViewModel;

                                                    if (_isSpellTargeting)
                                                    {
                                                        PlayerUnits.ForEach(u => u.ResetTargeting());
                                                        EnemyUnits.ForEach(u => u.ResetTargeting());
                                                        PlayerViewModel.HeroUnitViewModel.ResetTargeting();
                                                        EnemyViewModel.HeroUnitViewModel.ResetTargeting();
                                                        PlayerTurnEvent?.Invoke(this, new PlayerTurnRequestEventArgs(
                                                            new CardDeployPlayerTurn(
                                                                _player, _spellTargetingViewModel.Card,
                                                                unitViewModel.BaseUnit)));
                                                        _unitTargetingViewModel = null;
                                                        _isSpellTargeting = false;
                                                        return;
                                                    }

                                                    if (!_isAttackTargeting) return;

                                                    //провкрка провокатора
                                                    if (unitViewModel.BaseUnit.State.AttackPriority != 2 &&
                                                        EnemyUnits.ToList().Exists(u =>
                                                            u.BaseUnit.State.AttackPriority == 2))
                                                        return;

                                                    //проверка маскировки
                                                    if (unitViewModel.BaseUnit.State.AttackPriority == 0)
                                                        return;

                                                    PlayerTurnEvent?.Invoke(this, new PlayerTurnRequestEventArgs(
                                                        new UnitAttackPlayerTurn(_player,
                                                            _unitTargetingViewModel.BaseUnit, unitViewModel.BaseUnit)));
                                                    _unitTargetingViewModel.IsCanAttack = false;
                                                    _unitTargetingViewModel = null;
                                                    _isAttackTargeting = false;
                                                    EnemyUnits.ForEach(u => u.ResetTargeting());
                                                    EnemyViewModel.HeroUnitViewModel.ResetTargeting();
                                                }));

        public RelayCommand CardDeployCommand => _cardDeployCommand ??
                                                 (_cardDeployCommand = new RelayCommand(o =>
                                                 {
                                                     if (!(o is CardViewModel cardViewModel))
                                                         return;

                                                     if (!IsPlayerTurn && !cardViewModel.Card.CanBePlayedOnEnemyTurn)
                                                         return;

                                                     switch (cardViewModel.Card)
                                                     {
                                                         case SpellCard spellCard when spellCard.ActionInfo.IsTargeted:
                                                             if (spellCard.ActionInfo.ParameterType ==
                                                                 ActionParameterType.Damage ||
                                                                 spellCard.ActionInfo.ParameterType ==
                                                                 ActionParameterType.Heal)
                                                             {
                                                                 PlayerViewModel.HeroUnitViewModel.SetTargeting();
                                                                 EnemyViewModel.HeroUnitViewModel.SetTargeting();
                                                             }

                                                             PlayerUnits.ForEach(u => u.SetTargeting());
                                                             EnemyUnits.ForEach(u => u.SetTargeting());
                                                             _isSpellTargeting = true;
                                                             _spellTargetingViewModel = cardViewModel;
                                                             return;
                                                         case UnitCard unitCard
                                                             when unitCard.BattleCryActionInfo?.IsTargeted == true:
                                                             if (unitCard.BattleCryActionInfo.ParameterType ==
                                                                 ActionParameterType.Damage ||
                                                                 unitCard.BattleCryActionInfo.ParameterType ==
                                                                 ActionParameterType.Heal)
                                                             {
                                                                 PlayerViewModel.HeroUnitViewModel.SetTargeting();
                                                                 EnemyViewModel.HeroUnitViewModel.SetTargeting();
                                                             }

                                                             PlayerUnits.ForEach(u => u.SetTargeting());
                                                             EnemyUnits.ForEach(u => u.SetTargeting());
                                                             PlayerViewModel.HeroUnitViewModel.SetTargeting();
                                                             EnemyViewModel.HeroUnitViewModel.SetTargeting();
                                                             _isSpellTargeting = true;
                                                             _spellTargetingViewModel = cardViewModel;
                                                             return;
                                                     }

                                                     PlayerTurnEvent?.Invoke(this, new PlayerTurnRequestEventArgs(
                                                         new CardDeployPlayerTurn(
                                                             _player, cardViewModel.Card)));
                                                 }));


        public RelayCommand PlayerTargetCommand => _playerTargetCommand ?? (_playerTargetCommand =
                                                       new RelayCommand(o =>
                                                       {
                                                           if (!(o is PlayerUserControlViewModel viewModel))
                                                               return;

                                                           if (_isAttackTargeting)
                                                           {
                                                               PlayerTurnEvent?.Invoke(this,
                                                                   new PlayerTurnRequestEventArgs(
                                                                       new UnitAttackPlayerTurn(_player,
                                                                           _unitTargetingViewModel.BaseUnit,
                                                                           viewModel.HeroUnit)));
                                                               _unitTargetingViewModel.IsCanAttack = false;
                                                               _unitTargetingViewModel = null;
                                                               _isAttackTargeting = false;
                                                               return;
                                                           }

                                                           if (_isSpellTargeting)
                                                           {
                                                               PlayerTurnEvent?.Invoke(this,
                                                                   new PlayerTurnRequestEventArgs(
                                                                       new CardDeployPlayerTurn(
                                                                           _player, _spellTargetingViewModel.Card,
                                                                           viewModel.HeroUnit)));
                                                               _unitTargetingViewModel = null;
                                                               _isSpellTargeting = false;
                                                           }
                                                       }));

        public RelayCommand TransferTurnCommand => _transferTurnCommand ??
                                                   (_transferTurnCommand = new RelayCommand(o =>
                                                   {
                                                       var playerTurn = new EndPlayerTurn(null);
                                                       PlayerTurnEvent?.Invoke(this,
                                                           new PlayerTurnRequestEventArgs(playerTurn));
                                                   }));

        public bool IsPlayerTurn => _userService.Username == CurrentPlayerUsername;

        public event EventHandler<PlayerTurnRequestEventArgs> PlayerTurnEvent;

        public void Clear()
        {
            PlayerViewModel = new PlayerUserControlViewModel();
            EnemyViewModel = new PlayerUserControlViewModel();
            PlayerUnits.Clear();
            EnemyUnits.Clear();
            PlayerCards.Clear();
            EnemyCards.Clear();
            CurrentPlayerUsername = "";
        }
    }
}