using System.Windows.Controls;
using CollectibleCardGame.ViewModels.Frames;
using Unity.Attributes;

namespace CollectibleCardGame.Views.FramesShell
{
    /// <summary>
    ///     Логика взаимодействия для LogInFramePageShell.xaml
    /// </summary>
    public partial class LogInFramePageShell : Page
    {
        public LogInFramePageShell()
        {
            InitializeComponent();
        }

        [Dependency]
        public LogInFramePageShellViewModel ViewModel
        {
            get => DataContext as LogInFramePageShellViewModel;
            set => DataContext = value;
        }
    }
}