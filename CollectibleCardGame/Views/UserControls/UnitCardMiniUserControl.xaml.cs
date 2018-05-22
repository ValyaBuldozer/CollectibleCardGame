using System.Windows.Controls;
using CollectibleCardGame.ViewModels.Elements;
using Unity.Attributes;

namespace CollectibleCardGame.Views.UserControls
{
    /// <summary>
    ///     Логика взаимодействия для UnitCardMiniUserControl.xaml
    /// </summary>
    public partial class UnitCardMiniUserControl : UserControl
    {
        public UnitCardMiniUserControl()
        {
            InitializeComponent();
        }

        [Dependency]
        public UnitViewModel ViewModel
        {
            get => DataContext as UnitViewModel;
            set => DataContext = value;
        }
    }
}