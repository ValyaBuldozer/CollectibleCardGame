using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CollectibleCardGame.Views.ForComponents;
using CollectibleCardGame.Views.Frames;

namespace CollectibleCardGame.ViewModels.Frames
{
    public class MenuFramePageViewModel : BaseViewModel
    {
        private Page _currentPage;
        private MainMenuPart _selectedMenuItem;

        public Page CurrentPage
        {
            private set
            {
                _currentPage = value;
                NotifyPropertyChanged(nameof(CurrentPage));
            }
            get => _currentPage;
        }

        public MenuFramePageViewModel(GoGameFramePage goGameFramePage,DecksSettingsFramePage decksFramePage)
        {
            var mainPart = new MainMenuPart
            {
                ImagePath = "/Images/Icons/search.png",
                Title = "Играть",
                FramePage = goGameFramePage
            };

            Menus = new ObservableCollection<MainMenuPart>
            {
                mainPart,
                new MainMenuPart
                {
                    ImagePath = "/Images/Icons/cards.png",
                    Title = "Колоды",
                    FramePage = decksFramePage
                },
                new MainMenuPart
                {
                    ImagePath = "/Images/Icons/question.png",
                    Title = "Помощь",
                    FramePage = new HelpFramePage()
                }
            };
            SelectedMenuItem = mainPart;
        }

        public ObservableCollection<MainMenuPart> Menus
        {
            set;
            get;
        }

        public MainMenuPart SelectedMenuItem
        {
            get => _selectedMenuItem;
            set
            {
                if (value == null) return;
                _selectedMenuItem = value;
                CurrentPage = value.FramePage;
                NotifyPropertyChanged(nameof(SelectedMenuItem));
            }
        }
       


    }
}
