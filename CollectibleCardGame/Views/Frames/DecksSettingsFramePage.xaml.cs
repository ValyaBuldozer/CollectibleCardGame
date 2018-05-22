using System.Windows.Controls;
using CollectibleCardGame.ViewModels.Frames;
using Unity.Attributes;

namespace CollectibleCardGame.Views.Frames
{
    /// <summary>
    ///     Логика взаимодействия для DecksSettingsFramePage.xaml
    /// </summary>
    public partial class DecksSettingsFramePage : Page
    {
        public DecksSettingsFramePage()
        {
            InitializeComponent();
        }

        [Dependency]
        public DeckSettingsViewModel ViewModel
        {
            get => DataContext as DeckSettingsViewModel;
            set => DataContext = value;
        }
    }
}