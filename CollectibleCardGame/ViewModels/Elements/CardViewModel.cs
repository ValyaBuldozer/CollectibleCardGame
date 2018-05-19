using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using GameData.Enums;
using GameData.Models.Cards;
using ArgumentOutOfRangeException = System.ArgumentOutOfRangeException;

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

        private Brush _tapeBrush;
        private Brush _tapeBorderBrush;

        private Card _card;

        public Card Card
        {
            get => _card;
            set
            {
                _card = value;
                NotifyPropertyChanged(nameof(Card));

                if(value == null) return;

                Description = _card.Description;
                Name = _card.Name;
                Cost = _card.Cost;

                _imagePath = _card.ImagePath;

                if (_card is UnitCard unitCard)
                {
                    _isUnitCard = true;
                    _attack = unitCard.BaseAttack;
                    _health = unitCard.BaseHP;

                    switch(unitCard.Fraction)
                        {
                            case Fraction.Common:
                                TapeBorderBrush = null;
                                TapeBrush = null;
                                break;
                            case Fraction.North:
                                TapeBorderBrush = new SolidColorBrush(Color.FromRgb(156, 162, 156));
                                TapeBrush = new SolidColorBrush(Color.FromRgb(0, 97, 225));
                                break;
                            case Fraction.South:
                                TapeBorderBrush = new SolidColorBrush(Color.FromRgb(206, 130, 57));
                                TapeBrush = new SolidColorBrush(Color.FromRgb(57, 58, 60));
                                break;
                            case Fraction.Dark:
                                TapeBorderBrush = new SolidColorBrush(Color.FromRgb(148, 182, 178));
                                TapeBrush = new SolidColorBrush(Color.FromRgb(159, 0, 22));
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                }
            }
        }

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

        public Brush TapeBrush
        {
            get => _tapeBrush;
            set
            {
                _tapeBrush = value;
                NotifyPropertyChanged(nameof(TapeBrush));
            }
        }

        public Brush TapeBorderBrush
        {
            get => _tapeBorderBrush;
            set
            {
                _tapeBorderBrush = value;
                NotifyPropertyChanged(nameof(TapeBorderBrush));
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

        public CardViewModel()
        {
            Card = new SpellCard();
        }
    }
}
