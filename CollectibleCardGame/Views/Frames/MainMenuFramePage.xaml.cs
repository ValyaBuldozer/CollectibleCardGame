using System.Windows.Controls;
using CollectibleCardGame.ViewModels.Frames;
using Unity.Attributes;

namespace CollectibleCardGame.Views.Frames
{
    /// <summary>
    ///     Логика взаимодействия для MainMenuFramePage.xaml
    /// </summary>
    public partial class MainMenuFramePage : Page
    {
        public MainMenuFramePage()
        {
            InitializeComponent();
        }

        [Dependency]
        public MenuFramePageViewModel ViewModel
        {
            get => DataContext as MenuFramePageViewModel;
            set => DataContext = value;
        }
    }
}