using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  System.Windows.Media;

namespace CollectibleCardGame.ViewModels.UserControls
{
    class UnitCardUserControlViewModel: BaseViewModel
    {
        private string _name;
        private string _description;
        private string _imagePath;
        private string _tapeBrush;
        private string _tapeBorderBrush;
        private int _cost;
        private int _attack;
        private int _health;


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

        public string TapeBrush
        {
            get => _tapeBrush;
            set
            {
                _tapeBrush = value;
                NotifyPropertyChanged(nameof(TapeBrush));
            }
        }

        public string TapeBorderBrush
        {
            get => _tapeBorderBrush;
            set
            {
                _tapeBorderBrush = value;
                NotifyPropertyChanged(nameof(TapeBorderBrush));
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

        public int Cost
        {
            get => _cost;
            set
            {
                _cost = value;
                NotifyPropertyChanged(nameof(Cost));
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
        //+ свойство для добавления элементов в ИтемКонтрол "AbilityStack"
    }
}
