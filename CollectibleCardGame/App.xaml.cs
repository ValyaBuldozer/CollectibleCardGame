using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using CollectibleCardGame;
using CollectibleCardGame.ViewModels;
using CollectibleCardGame.ViewModels.Frames;
using CollectibleCardGame.ViewModels.UserControls;
using CollectibleCardGame.Views.Frames;
using CollectibleCardGame.Views.FramesShell;
using CollectibleCardGame.Views.UserControls;

namespace CollectibleCardGame
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow window = new MainWindow();

            //CardDecksFramePage frame = new CardDecksFramePage() { DataContext = new CardDecksPageViewModel() };
            //frame.f1.DataContext = new FractionUserControlViewModel() { Name = "Калвария", Description = "Северная фракция", ImagePath = "/Images/northPicture.jpg" };
            //frame.f2.DataContext = new FractionUserControlViewModel() { Name = "Магдебург", Description = "Южная фракцция", ImagePath = "/Images/southPicture.jpg" };
            //frame.f3.DataContext = new FractionUserControlViewModel() { Name = "Чудовища", Description = "Монстры", ImagePath = "/Images/darksidePicture.jpg" };


            //LogInFramePageShell frame = new LogInFramePageShell(){DataContext = new LogInFramePageShellViewModel()};


            TESTFramePage frame = new TESTFramePage() { DataContext = new GameProccesPageViewModel() };
            frame.c1.DataContext=new UnitCardUserControlViewModel(){Name = "Владик", Description = "Я хожу в качалку, но только по воскресеньям... Как же круто!", ImagePath = "/UnitImages/testResize375x420.jpg", Cost = 5, Attack = 9, Health = 6, TapeBrush = "#007ACC", TapeBorderBrush = "Blue"};
            //frame.f1.DataContext = new FractionUserControlViewModel() { Name = "Калвария", Description = "Северная фракция", ImagePath = "/Images/northPicture.jpg" };
            //frame.f2.DataContext = new FractionUserControlViewModel() { Name = "Магдебург", Description = "Южная фракцция", ImagePath = "/Images/southPicture.jpg" };
            //frame.f3.DataContext = new FractionUserControlViewModel() { Name = "Чудовища", Description = "Монстры", ImagePath = "/Images/darksidePicture.jpg" };

            window.MainFrame.Content = frame;
            window.Show();
        }
    }
}
