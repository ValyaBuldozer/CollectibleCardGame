using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CollectibleCardGame.Services;

namespace CollectibleCardGame.ViewModels.Frames
{
    public class CardDecksPageViewModel : BaseViewModel
    {
        private RelayCommand _choseFractionCommand;

        public RelayCommand ChoseFractionCommand
        {
            get { return _choseFractionCommand ?? (_choseFractionCommand = new RelayCommand(obj =>
                             {

                                 MessageBox.Show(((FractionUserControlViewModel)obj).Description);
                             })); }
        }
    }
}
