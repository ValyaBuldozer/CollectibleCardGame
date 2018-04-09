using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectibleCardGame.ViewModels.UserControls
{
    class SpellCardUserControlViewModel : BaseViewModel
    {
        private string _name;
        private string _description;
        private string _imagePath;
        private int _cost;
       


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

        public int Cost
        {
            get => _cost;
            set
            {
                _cost = value;
                NotifyPropertyChanged(nameof(Cost));
            }
        }

       

       
    }
}

