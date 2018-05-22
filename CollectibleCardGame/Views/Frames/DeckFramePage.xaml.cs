using System.Windows.Controls;
using System.Windows.Input;
using CollectibleCardGame.ViewModels.UserControls;

namespace CollectibleCardGame.Views.Frames
{
    /// <summary>
    ///     Логика взаимодействия для DeckFramePage.xaml
    /// </summary>
    public partial class DeckFramePage : Page
    {
        public DeckFramePage()
        {
            InitializeComponent();
        }

        public DeckViewModel ViewModel
        {
            get => DataContext as DeckViewModel;
            set => DataContext = value;
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var scrollviewer = sender as ScrollViewer;
            if (e.Delta > 0)
                scrollviewer.LineLeft();
            else
                scrollviewer.LineRight();
            e.Handled = true;
        }
    }
}