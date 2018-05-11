using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CollectibleCardGame.ViewModels.Frames;
using Unity.Attributes;

namespace CollectibleCardGame.Views.Frames
{
    /// <summary>
    /// Логика взаимодействия для GameEngineFramePage.xaml
    /// </summary>
    public partial class GameEngineFramePage : Page
    {
        [Dependency]
        public GameEngineViewModel ViewModel
        {
            set => DataContext = value;
            get => DataContext as GameEngineViewModel;
        }

        public GameEngineFramePage()
        {
            InitializeComponent();
        }

        private void ViewBoxOnMouseEnter(object sender, MouseEventArgs e)
        {
            var viewBox = sender as Viewbox;

            viewBox.Height *= 1.5;
            viewBox.Width *= 1.5;
        }

        private void ViewBoxOnMouseLeave(object sender, MouseEventArgs e)
        {
            var viewBox = sender as Viewbox;

            viewBox.Height /= 1.5;
            viewBox.Width /= 1.5;
        }
    }
}
