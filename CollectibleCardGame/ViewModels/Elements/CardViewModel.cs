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

        public Card Card { get; }

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

        public CardViewModel(Card card)
        {
            Card = card;
            _cost = card.Cost;
            _description = card.Description;
            _name = card.Name;

        }
    }
}
