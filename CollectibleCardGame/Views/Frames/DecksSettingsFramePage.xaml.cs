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
using CollectibleCardGame.ViewModels.UserControls;
using CollectibleCardGame.Views.UserControls;
using Unity.Attributes;

namespace CollectibleCardGame.Views.Frames
{
    /// <summary>
    /// Логика взаимодействия для DecksSettingsFramePage.xaml
    /// </summary>
    public partial class DecksSettingsFramePage : Page
    {
        [Dependency]
        public DeckSettingsViewModel ViewModel
        {
            get => DataContext as DeckSettingsViewModel;
            set => DataContext = value;
        }

        public DecksSettingsFramePage()
        {
            InitializeComponent();
        }

    }
}
