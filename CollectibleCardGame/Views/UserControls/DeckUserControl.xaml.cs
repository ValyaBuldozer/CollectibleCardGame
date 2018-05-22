using System.Windows.Controls;
using System.Windows.Input;
using CollectibleCardGame.ViewModels.UserControls;

namespace CollectibleCardGame.Views.UserControls
{
    /// <summary>
    ///     Логика взаимодействия для DeckUserControl.xaml
    /// </summary>
    public partial class DeckUserControl : UserControl
    {
        public DeckUserControl()
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