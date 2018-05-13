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

        public Unit BaseUnit
        {
            get => _baseUnit;
            set
            {
                _baseUnit = value;
                NotifyPropertyChanged(nameof(BaseUnit));
                Attack = value.State.Attack;
                Health = value.State.GetResultHealth;
                Name = value.BaseCard.Name;
                _baseUnit.State.PropertyChanged += State_PropertyChanged;
                NotifyPropertyChanged(nameof(ImagePath));
            }
        }

        private void State_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Attack = _baseUnit.State.Attack;
            Health = _baseUnit.State.GetResultHealth;
        }

        public string ImagePath
        {
            get => _baseUnit.BaseCard.ImagePath;
            set => _baseUnit.BaseCard.ImagePath = value;
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

        public UnitViewModel(Unit unit)
        {
            BaseUnit = unit;
        }


    }
}
