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
    /// Логика взаимодействия для ServerConnectionPage.xaml
    /// </summary>
    public partial class ServerConnectionPage : Page
    {
        [Dependency]
        public ServerConnectionViewModel ViewModel
        {
            get => DataContext as ServerConnectionViewModel;
            set => DataContext = value;
        }

        public ServerConnectionPage()
        {
            InitializeComponent();
        }
    }
}
