using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Threading;
using GameData.Models.Units;

namespace CollectibleCardGame.ViewModels.Elements
{
    public class UnitViewModel : BaseViewModel
    {
        private readonly Dispatcher _curretnDispatcher;

        private string _abilityImagePath;
        private int _attack;
        private Color _attackShadowColor;
        private CardViewModel _baseCardViewModel;
        private Unit _baseUnit;
        private Brush _borderBrush;
        private int _health;
        private Brush _healthForeground;
        private Color _healthShdowColor;
        private bool _isCanAttack;
        private bool _isNotHeroUnit;
        private string _name;
        private Color _shadowColor;

        public UnitViewModel(Unit unit)
        {
            _curretnDispatcher = Dispatcher.CurrentDispatcher;
            _isNotHeroUnit = !(unit is HeroUnit);
            _baseCardViewModel = new CardViewModel();
            BaseUnit = unit;
            _borderBrush = null;

            if (unit?.State != null)
                IsCanAttack = unit.State.CanAttack;

            _healthShdowColor = Color.FromArgb(0, 0, 0, 0);
        }

        public UnitViewModel()
        {
            _curretnDispatcher = Dispatcher.CurrentDispatcher;
            _baseCardViewModel = new CardViewModel();
            BaseUnit = null;
            _borderBrush = null;
            _healthShdowColor = Color.FromArgb(0, 0, 0, 0);
        }

        public Unit BaseUnit
        {
            get => _baseUnit;
            set
            {
                _baseUnit = value;
                NotifyPropertyChanged(nameof(BaseUnit));

                IsNotHeroUnit = !(value is HeroUnit);

                if (value?.State == null) return;
                BaseCardViewModel.Card = _baseUnit.BaseCard;

                Attack = value.State.Attack;
                Health = value.State.GetResultHealth;
                Name = value.BaseCard.Name;
                IsCanAttack = value.State.CanAttack;
                _baseUnit.State.PropertyChanged += State_PropertyChanged;
                NotifyPropertyChanged(nameof(ImagePath));

                if (_baseUnit is HeroUnit) return;

                switch (value.State.AttackPriority)
                {
                    case 0:
                        AbilityImagePath = "../../Images/IconsUnit/disguiestIco.png";
                        break;
                    case 1:
                        AbilityImagePath = "../../Images/IconsUnit/sword.png";
                        break;
                    case 2:
                        AbilityImagePath = "../../Images/IconsUnit/shieldIco.png";
                        break;
                    default:
                        break;
                }
            }
        }

        public CardViewModel BaseCardViewModel
        {
            get => _baseCardViewModel;
            set
            {
                _baseCardViewModel = value;
                NotifyPropertyChanged(nameof(BaseCardViewModel));
            }
        }

        public string ImagePath
        {
            get => _baseUnit?.BaseCard?.ImagePath;
            set => _baseUnit.BaseCard.ImagePath = value;
        }

        public string AbilityImagePath
        {
            get => _abilityImagePath;
            set
            {
                _abilityImagePath = value;
                NotifyPropertyChanged(nameof(AbilityImagePath));
            }
        }

        public int Attack
        {
            get => _attack;
            set
            {
                if (_attack == value) return;

                _attack = value;
                NotifyPropertyChanged(nameof(Attack));

                //обновление DependencySource в dispatcher потоке
                _curretnDispatcher.Invoke(() =>
                {
                    if (BaseUnit.State.Attack < BaseUnit.BaseCard.BaseAttack)
                        AttackShadowColor = Color.FromRgb(255, 0, 0);
                    else if (BaseUnit.State.Attack > BaseUnit.BaseCard.BaseAttack)
                        AttackShadowColor = Color.FromRgb(0, 255, 0);
                    else
                        AttackShadowColor = Color.FromArgb(0, 0, 0, 0);
                });
            }
        }

        public int Health
        {
            get => _health;
            set
            {
                if (_health == value) return;

                _health = value;
                NotifyPropertyChanged(nameof(Health));

                //обновление DependencySource в dispatcher потоке
                _curretnDispatcher.Invoke(() =>
                {
                    if (BaseUnit.State.RecievedDamage != 0)
                        HealthShadowColor = Color.FromRgb(255, 51, 0);
                    else if (BaseUnit.State.BaseHealth > BaseUnit.BaseCard.BaseHP)
                        HealthShadowColor = Color.FromRgb(0, 255, 0);
                    else
                        HealthShadowColor = Color.FromArgb(0, 0, 0, 0);
                });
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }

        public bool IsCanAttack
        {
            get => _isCanAttack;
            set
            {
                _isCanAttack = value;
                NotifyPropertyChanged(nameof(IsCanAttack));

                ShadowColor = value ? Color.FromArgb(255, 255, 102, 0) : Color.FromArgb(0, 0, 0, 0);
            }
        }

        public bool IsNotHeroUnit
        {
            get => _isNotHeroUnit;
            set
            {
                _isNotHeroUnit = value;
                NotifyPropertyChanged(nameof(IsNotHeroUnit));
            }
        }

        public Brush BorderBrush
        {
            get => _borderBrush;
            set
            {
                _borderBrush = value;
                NotifyPropertyChanged(nameof(BorderBrush));
            }
        }

        public Color ShadowColor
        {
            get => _shadowColor;
            set
            {
                _shadowColor = value;
                NotifyPropertyChanged(nameof(ShadowColor));
            }
        }

        public Color HealthShadowColor
        {
            get => _healthShdowColor;
            set
            {
                _healthShdowColor = value;
                NotifyPropertyChanged(nameof(HealthShadowColor));
            }
        }

        public Color AttackShadowColor
        {
            get => _attackShadowColor;
            set
            {
                _attackShadowColor = value;
                NotifyPropertyChanged(nameof(AttackShadowColor));
            }
        }

        public Brush HealthForeground
        {
            get => _healthForeground;
            set
            {
                _healthForeground = value;
                NotifyPropertyChanged(nameof(HealthForeground));
            }
        }

        private void State_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Attack = _baseUnit.State.Attack;
            Health = _baseUnit.State.GetResultHealth;

            if (_baseUnit is HeroUnit) return;

            switch (_baseUnit.State.AttackPriority)
            {
                case 0:
                    AbilityImagePath = "../../Images/IconsUnit/disguiestIco.png";
                    break;
                case 1:
                    AbilityImagePath = "../../Images/IconsUnit/sword.png";
                    break;
                case 2:
                    AbilityImagePath = "../../Images/IconsUnit/shieldIco.png";
                    break;
                default:
                    break;
            }
        }

        public void SetTargeting()
        {
            //ShadowColor = Color.FromRgb(0,198,0);
            ShadowColor = Color.FromRgb(0, 255, 255);
        }

        public void ResetTargeting()
        {
            //ой неочевидно - в сеттере свойства ставится нужный цвет бордера переделать бы

            IsCanAttack = _isCanAttack;
        }
    }
}