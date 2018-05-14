using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Models.Cards;

namespace CollectibleCardGame.ViewModels.Elements
{
    public class CardViewModel : BaseViewModel
    {
        private int _cost;
        private string _name;
        private string _description;
        private string _imagePath;
        private int _attack;
        private int _health;
        private bool _isUnitCard;

        public Card Card { get; }

        public bool IsUnitCard
        {
            get => _isUnitCard;
            set
            {
                _isUnitCard = value;
                NotifyPropertyChanged(nameof(IsUnitCard));
            }
        }

        public int Cost
        {
            get => _cost;
            set
            {
                _cost = value;
                NotifyPropertyChanged(nameof(Cost));
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

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                NotifyPropertyChanged(nameof(Description));
            }
        }

        public string ImagePath
        {
            get => _imagePath;
            set
            {
                _imagePath = value;
                NotifyPropertyChanged(nameof(ImagePath));
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

        public CardViewModel(Card card)
        {
            Card = card;
            _cost = card.Cost;
            _description = card.Description;
            _name = card.Name;
            _imagePath = card.ImagePath;

            if (card is UnitCard unitCard)
            {
                _isUnitCard = true;
                _attack = unitCard.BaseAttack;
                _health = unitCard.BaseHP;
            }
        }
    }
}
