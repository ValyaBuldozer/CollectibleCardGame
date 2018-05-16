using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Models.Units;

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

        private string _abilityImagePath;

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
                _baseUnit.State.PropertyChanged += State_PropertyChanged;
                NotifyPropertyChanged(nameof(ImagePath));

                AbilityImagePath = value.State.AttackPriority != 2 ? "../../Images/IconsUnit/sword.png" :
                    "../../Images/healthShield.png";

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

        public bool IsNotHeroUnit
        {
            get => _isNotHeroUnit;
            set
            {
                _isNotHeroUnit = value;
                NotifyPropertyChanged(nameof(IsNotHeroUnit));
            }
        }

        private void State_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Attack = _baseUnit.State.Attack;
            Health = _baseUnit.State.GetResultHealth;
        }

        public UnitViewModel(Unit unit)
        {
            _isNotHeroUnit = !(unit is HeroUnit);
            _baseCardViewModel = new CardViewModel();
            BaseUnit = unit;
        }

        public UnitViewModel()
        {
            _baseCardViewModel = new CardViewModel();
            BaseUnit = null;
        }
    }
}
