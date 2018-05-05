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
    class MainMenuFramePageViewModel : BaseViewModel
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

        public MainMenuFramePageViewModel()
        {
            Menus = new ObservableCollection<MainMenuPart>
            {
                new MainMenuPart
                {
                    ImagePath = "/Images/Icons/search.png",
                    Title = "Играть",
                    FramePage = new GoGameFramePage()
                },
                new MainMenuPart
                {
                    ImagePath = "/Images/Icons/cards.png",
                    Title = "Колоды",
                    FramePage = new DecksSettingsFramePage()
                },
                new MainMenuPart
                {
                    ImagePath = "/Images/Icons/question.png",
                    Title = "Помощь",
                    FramePage = new HelpFramePage()
                },
                new MainMenuPart
                {
                    ImagePath = "/Images/Icons/settingsChange.png",
                    Title = "Настройки",
                    FramePage = new SettingsFramePage()
                }

            };
            //BusyMessage = "Подождите...";
            //IsBusy = false;
           
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
