using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CollectibleCardGame.ViewModels.Frames;
using Unity.Attributes;

namespace CollectibleCardGame.Views.Frames
{
    /// <summary>
    ///     Логика взаимодействия для GameEngineFramePage.xaml
    /// </summary>
    public partial class GameEngineFramePage : Page
    {
        public GameEngineFramePage()
        {
            InitializeComponent();
        }

        [Dependency]
        public GameEngineViewModel ViewModel
        {
            set => DataContext = value;
            get => DataContext as GameEngineViewModel;
        }

        private void ViewBoxOnMouseEnter(object sender, MouseEventArgs e)
        {
            var viewBox = sender as Viewbox;

            viewBox.Height *= 1.5;
            viewBox.Width *= 1.5;

            viewBox.Margin = new Thickness(0, -100, 0, 0);
        }

        private void ViewBoxOnMouseLeave(object sender, MouseEventArgs e)
        {
            var viewBox = sender as Viewbox;

            viewBox.Height /= 1.5;
            viewBox.Width /= 1.5;
            viewBox.Margin = new Thickness(0, 0, 0, 0);
            // viewBox.Margin = new Thickness(0,0,0,0);
        }
    }
}