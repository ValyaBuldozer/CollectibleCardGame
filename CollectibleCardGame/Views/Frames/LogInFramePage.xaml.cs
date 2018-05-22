using System.Windows.Controls;
using CollectibleCardGame.ViewModels.Frames;
using Unity.Attributes;

namespace CollectibleCardGame.Views.Frames
{
    /// <summary>
    ///     Логика взаимодействия для LogInFramePage.xaml
    /// </summary>
    public partial class LogInFramePage : Page
    {
        public LogInFramePage()
        {
            InitializeComponent();
        }

        [Dependency]
        public LogInFramePageViewModel ViewModel
        {
            get => DataContext as LogInFramePageViewModel;
            set => DataContext = value;
        }
    }
}