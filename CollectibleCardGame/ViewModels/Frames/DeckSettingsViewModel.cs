using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectibleCardGame.ViewModels.UserControls;

namespace CollectibleCardGame.ViewModels.Frames
{
    public class DeckSettingsViewModel : BaseViewModel
    {

    }

    public class DeckTabItem
    {
        public string Title { get; }

        public DeckViewModel ViewModel { get; }

        public DeckTabItem(string title, DeckViewModel viewModel)
        {
            Title = title;
            ViewModel = viewModel;
        }
    }
}
