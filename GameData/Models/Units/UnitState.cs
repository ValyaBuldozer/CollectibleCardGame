using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GameData.Models.Units
{
    /// <summary>
    /// Класс отвечает за все параметры юнита - текущее здоровье, атаку, приоритет, возможность атаки
    /// </summary>
    public class UnitState : INotifyPropertyChanged, IEquatable<UnitState>
    {
        private Unit _unit;
        private int _attackPriority;
        private int _baseHealth;
        private int _recievedDamage;
        private int _attack;

        [JsonIgnore]
        public Unit Unit
        {
            get => _unit;
            set
            {
                _unit = value;
                if (value != null)
                {
                    _baseHealth = value.BaseCard.BaseHP;
                    _attack = value.BaseCard.BaseAttack;
                    CanAttack = value.BaseCard.CanAttack;
                    _attackPriority = value.BaseCard.AttackPriority;
                }
                else
                {
                    _baseHealth = 0;
                    _attack = 0;
                    CanAttack = false;
                    _attackPriority = 0;
                }

                NotifyPropertyChanged(nameof(Unit));
            }
        }

        public int AttackPriority
        {
            get => _attackPriority;
            set
            {
                if(_attackPriority == value) return;

                _attackPriority = value;
                NotifyPropertyChanged(nameof(AttackPriority));
            }
        }

        public bool CanAttack { get; set; }

        public int BaseHealth
        {
            get => _baseHealth;
            set
            {
                if(_baseHealth == value) return;

                _baseHealth = value < 1 ? 1 : value;

                if (GetResultHealth < 1)
                    RecievedDamage = 0;

                NotifyPropertyChanged(nameof(BaseHealth));
            }
        }

        public int RecievedDamage
        {
            get => _recievedDamage;
            set
            {
                if(_recievedDamage  == value) return;

                _recievedDamage = value;
                NotifyPropertyChanged(nameof(RecievedDamage));
            }
        }

        [JsonIgnore]
        public int GetResultHealth => _baseHealth - _recievedDamage;

        public int Attack
        {
            get => _attack;
            set
            {
                if(_attack == value) return;

                _attack = value < 0 ? 0 : value;
                NotifyPropertyChanged(nameof(Attack));
            }
        }

        public void RecieveDamage(int value)
        {
            if(value == 0)
                return;

            RecievedDamage += value;
            if(GetResultHealth <= 0)
                ZeroHpEvent?.Invoke(this,new ZeroHpEventArgs(Unit));
        }

        public void Heal(int value)
        {
            if(value == 0)
                return;

            if (_recievedDamage <= value)
                RecievedDamage = 0;
            else
                RecievedDamage -= value;
        }

        public UnitState(Unit unit)
        {
            Unit = unit;
            _recievedDamage = 0;
        }

        public UnitState SetState
        {
            set
            {
                if(value == null) return;

                Attack = value.Attack;
                AttackPriority = value.AttackPriority;
                BaseHealth = value.BaseHealth;
                CanAttack = value.CanAttack;
                RecievedDamage = value.RecievedDamage;
                
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<ZeroHpEventArgs> ZeroHpEvent;

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as UnitState);
        }

        public bool Equals(UnitState other)
        {
            return other != null &&
                   AttackPriority == other.AttackPriority &&
                   CanAttack == other.CanAttack &&
                   BaseHealth == other.BaseHealth &&
                   RecievedDamage == other.RecievedDamage &&
                   GetResultHealth == other.GetResultHealth &&
                   Attack == other.Attack;
        }
    }
}
