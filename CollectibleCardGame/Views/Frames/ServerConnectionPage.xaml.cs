using System.Windows.Controls;
using CollectibleCardGame.ViewModels.Frames;
using Unity.Attributes;

namespace CollectibleCardGame.Views.Frames
{
    /// <summary>
    ///     Логика взаимодействия для ServerConnectionPage.xaml
    /// </summary>
    public partial class ServerConnectionPage : Page
    {
        public ServerConnectionPage()
        {
            InitializeComponent();
        }

        [Dependency]
        public ServerConnectionViewModel ViewModel
        {
            get => DataContext as ServerConnectionViewModel;
            set => DataContext = value;
        }
    }
}