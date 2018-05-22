using System.Windows.Controls;
using CollectibleCardGame.ViewModels.Frames;
using Unity.Attributes;

namespace CollectibleCardGame.Views.Frames
{
    /// <summary>
    ///     Логика взаимодействия для GoGameFramePage.xaml
    /// </summary>
    public partial class GoGameFramePage : Page
    {
        public GoGameFramePage()
        {
            InitializeComponent();
        }

        [Dependency]
        public GoGameFramePageViewModel ViewModel
        {
            set => DataContext = value;
            get => DataContext as GoGameFramePageViewModel;
        }
    }
}