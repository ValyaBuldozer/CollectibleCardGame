using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Models.Units;
using System.Windows.Media;

namespace CollectibleCardGame.ViewModels.Elements
{
    public class UnitViewModel : BaseViewModel
    {
        private Unit _baseUnit;
        private int _attack;
        private int _health;
        private string _name;
        private bool _isNotHeroUnit;
        private CardViewModel _baseCardViewModel;
        private bool _isCanAttack;

        private string _abilityImagePath;
        private System.Windows.Media.Brush _borderBrush;
        private System.Windows.Media.Color _shadowColor;

        public Unit BaseUnit
        {
            get => _baseUnit;
            set
            {
                _baseUnit = value;
                NotifyPropertyChanged(nameof(BaseUnit));

                IsNotHeroUnit = !(value is HeroUnit);

                if(value?.State == null) return;
                BaseCardViewModel.Card = _baseUnit.BaseCard;

                Attack = value.State.Attack;
                Health = value.State.GetResultHealth;
                Name = value.BaseCard.Name;
                IsCanAttack = value.State.CanAttack;
                _baseUnit.State.PropertyChanged += State_PropertyChanged;
                NotifyPropertyChanged(nameof(ImagePath));

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
                _attack = value;
                NotifyPropertyChanged(nameof(Attack));
            }
        }

        public int Health
        {
            get => _health;
            set
            {
                _health = value;
                NotifyPropertyChanged(nameof(Health));
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

        public System.Windows.Media.Brush BorderBrush
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

        private void State_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Attack = _baseUnit.State.Attack;
            Health = _baseUnit.State.GetResultHealth;

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

        public UnitViewModel(Unit unit)
        {
            _isNotHeroUnit = !(unit is HeroUnit);
            _baseCardViewModel = new CardViewModel();
            BaseUnit = unit;
            _borderBrush = null;
            IsCanAttack = unit.State.CanAttack;
        }

        public UnitViewModel()
        {
            _baseCardViewModel = new CardViewModel();
            BaseUnit = null;
            _borderBrush = null;
        }

        public void SetTargeting()
        {
            //ShadowColor = Color.FromRgb(0,198,0);
            ShadowColor = Color.FromRgb(0,255,255);
        }

        public void ResetTargeting()
        {
            //ой неочевидно - в сеттере свойства ставится нужный цвет бордера переделать бы

            IsCanAttack = _isCanAttack;
        }
    }
}
