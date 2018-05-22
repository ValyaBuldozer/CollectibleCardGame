using System.Windows;
using CollectibleCardGame.Services;
using CollectibleCardGame.ViewModels.UserControls;

namespace CollectibleCardGame.ViewModels.Frames
{
    public class CardDecksPageViewModel : BaseViewModel
    {
        private RelayCommand _choseFractionCommand;

        public RelayCommand ChoseFractionCommand
        {
            get
            {
                return _choseFractionCommand ?? (_choseFractionCommand = new RelayCommand(obj =>
                {
                    MessageBox.Show(((FractionUserControlViewModel) obj).Description);
                }));
            }
        }
    }
}