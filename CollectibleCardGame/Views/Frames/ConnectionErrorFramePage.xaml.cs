using System.Windows.Controls;
using CollectibleCardGame.ViewModels.Frames;
using Unity.Attributes;

namespace CollectibleCardGame.Views.Frames
{
    /// <summary>
    ///     Логика взаимодействия для ConnectionErrorFramePage.xaml
    /// </summary>
    public partial class ConnectionErrorFramePage : Page
    {
        public ConnectionErrorFramePage()
        {
            InitializeComponent();
        }

        [Dependency]
        public ErrorFramePageViewModel ViewModel
        {
            get => DataContext as ErrorFramePageViewModel;
            set => DataContext = value;
        }
    }
}