using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using CollectibleCardGame.Services;
using CollectibleCardGame.ViewModels.Elements;
using GameData.Models.Cards;
using GameData.Models.Units;
using Xceed.Wpf.Toolkit;

namespace CollectibleCardGame.ViewModels.Frames
{
    public class GameEngineViewModel : BaseViewModel
    { 
        private HeroUnit _playerHeroUnit;
        private HeroUnit _enemyHeroUnit;
        private ObservableCollection<CardViewModel> _playerCards;
        private ObservableCollection<CardViewModel> _enemyCards;
        private ObservableCollection<Unit> _playerUnits;
        private ObservableCollection<Unit> _enemyUnits;

        private RelayCommand _cardDeployCommand;

        public Dispatcher CurrentDispatcher { get; }

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

        public ObservableCollection<Unit> PlayerUnits
        {
            get => _playerUnits;
            set
            {
                _playerUnits = value;
                NotifyPropertyChanged(nameof(PlayerUnits));
            }
        }

        public ObservableCollection<Unit> EnemyUnits
        {
            get => _enemyUnits;
            set
            {
                _enemyUnits = value;
                NotifyPropertyChanged(nameof(EnemyUnits));
            }
        }

        public GameEngineViewModel()
        {
            CurrentDispatcher = Dispatcher.CurrentDispatcher;

            PlayerCards = new ObservableCollection<CardViewModel>();
            EnemyCards = new ObservableCollection<CardViewModel>();
            PlayerUnits = new ObservableCollection<Unit>();
            EnemyUnits  = new ObservableCollection<Unit>();
        }

        public event EventHandler<PlayerTurnRequestEventArgs> PlayerTurnEvent;

        public RelayCommand CardDeployCommand => _cardDeployCommand ??
               (_cardDeployCommand = new RelayCommand(o =>
               {
                   if(!(o is Card card))
                       return;
                   MessageBox.Show(card.Name);
               }));
    }
}
