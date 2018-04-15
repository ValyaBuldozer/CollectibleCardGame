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
using CollectibleCardGame.Views.Frames;
using Unity.Attributes;

namespace CollectibleCardGame.Views.FramesShell
{
    /// <summary>
    /// Логика взаимодействия для LogInFramePageShell.xaml
    /// </summary>
    public partial class LogInFramePageShell : Page
    {
        [Dependency]
        public LogInFramePageShellViewModel ViewModel
        {
            get => DataContext as LogInFramePageShellViewModel;
            set => DataContext = value;
        }

        public LogInFramePageShell()
        {
            
            InitializeComponent();
           
        }

        
    }
}
