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
using CollectibleCardGame.ViewModels.Windows;
using Unity.Attributes;

namespace CollectibleCardGame.Views.Frames
{
    /// <summary>
    /// Логика взаимодействия для LogInFramePage.xaml
    /// </summary>
    public partial class LogInFramePage : Page
    {
        [Dependency]
        public LogInFramePageViewModel ViewModel
        {
            get => DataContext as LogInFramePageViewModel;
            set => DataContext = value;
        }

        public LogInFramePage()
        {
            InitializeComponent();
        }

        
    }
}
