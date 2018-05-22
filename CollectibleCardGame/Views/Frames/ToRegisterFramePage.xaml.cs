using System.Windows.Controls;
using CollectibleCardGame.ViewModels.Frames;
using Unity.Attributes;

namespace CollectibleCardGame.Views.Frames
{
    /// <summary>
    ///     Логика взаимодействия для ToRegisterFramePage.xaml
    /// </summary>
    public partial class ToRegisterFramePage : Page
    {
        public ToRegisterFramePage()
        {
            InitializeComponent();
        }

        [Dependency]
        public RegistrationFramePageViewModel ViewModel
        {
            get => DataContext as RegistrationFramePageViewModel;
            set => DataContext = value;
        }
    }
}