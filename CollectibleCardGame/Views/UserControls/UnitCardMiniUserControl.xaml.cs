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
using CollectibleCardGame.ViewModels.Elements;
using Unity.Attributes;

namespace CollectibleCardGame.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для UnitCardMiniUserControl.xaml
    /// </summary>
    public partial class UnitCardMiniUserControl : UserControl
    {
        [Dependency]
        public UnitViewModel ViewModel
        {
            get => DataContext as UnitViewModel;
            set => DataContext = value;
        }

        public UnitCardMiniUserControl()
        {
            InitializeComponent();
        }
    }
}
