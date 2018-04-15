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
using CollectibleCardGame.Logic.Controllers;
using CollectibleCardGame.Network.Controllers;
using CollectibleCardGame.Unity;
using CollectibleCardGame.ViewModels;
using CollectibleCardGame.ViewModels.Frames;
using CollectibleCardGame.ViewModels.UserControls;
using CollectibleCardGame.Views.Frames;
using CollectibleCardGame.Views.FramesShell;
using CollectibleCardGame.Views.UserControls;
using GameData.Network.Messages;
using Unity;

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


            //LogInFramePageShell frame = new LogInFramePageShell() { DataContext = new LogInFramePageShellViewModel() };


            // TESTFramePage frame = new TESTFramePage() { DataContext = new GameProccesPageViewModel() };
            //frame.u1.DataContext = new UnitCardUserControlViewModel() { Name = "Владик", Description = "Я хожу в качалку, но только по воскресеньям... Как же круто!", ImagePath = "/ImagesUnit/testResize210x253.jpg", Cost = 5, Attack = 9, Health = 6, TapeBrush = "#393A3C", TapeBorderBrush = "#CE8239" };
            //frame.s1.DataContext = new SpellCardUserControlViewModel() { Name = "Град стрел", Description = "Мне показалось, что солнце на секунду пропало...", ImagePath = "/ImagesSpell/SpellGradStrel.jpg", Cost = 6 };
            //frame.mu1.DataContext = new UnitCardUserControlViewModel() { Name = "Владик", Description = "Я хожу в качалку, но только по воскресеньям... Как же круто!", ImagePath = "/ImagesUnit/testResize210x253.jpg", Cost = 5, Attack = 9, Health = 6, TapeBrush = "#393A3C", TapeBorderBrush = "#CE8239" };

            //frame.f1.DataContext = new FractionUserControlViewModel() { Name = "Калвария", Description = "Северная фракция", ImagePath = "/Images/northPicture.jpg" };
            //frame.f2.DataContext = new FractionUserControlViewModel() { Name = "Магдебург", Description = "Южная фракцция", ImagePath = "/Images/southPicture.jpg" };
            //frame.f3.DataContext = new FractionUserControlViewModel() { Name = "Чудовища", Description = "Монстры", ImagePath = "/Images/darksidePicture.jpg" };


            MainMenuFramePage frame = new MainMenuFramePage() {DataContext = new MainMenuFramePageViewModel()};

            window.MainFrame.Content = frame;
            window.Show();




            //UnityKernel.InitializeKernel();
            //UnityKernel.Get<MainWindow>().Show();
            //UnityKernel.Get<GlobalAppStateController>().OnStartup();
        }
    }
}
