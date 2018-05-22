using System.Windows.Controls;
using CollectibleCardGame.ViewModels.Elements;

namespace CollectibleCardGame.Views.UserControls
{
    /// <summary>
    ///     Логика взаимодействия для CardUserControl.xaml
    /// </summary>
    public partial class CardUserControl : UserControl
    {
        public CardUserControl()
        {
            InitializeComponent();
        }

        public CardViewModel ViewModel
        {
            set => DataContext = value;
            get => DataContext as CardViewModel;
        }
    }
}