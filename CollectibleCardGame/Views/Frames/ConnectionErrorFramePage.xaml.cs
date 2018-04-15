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
    /// Логика взаимодействия для ConnectionErrorFramePage.xaml
    /// </summary>
    public partial class ConnectionErrorFramePage : Page
    {
        [Dependency]
        public ErrorFramePageViewModel ViewModel
        {
            get => DataContext as ErrorFramePageViewModel;
            set => DataContext = value;
        }

        public ConnectionErrorFramePage()
        {
            InitializeComponent();
        }
    }
}
