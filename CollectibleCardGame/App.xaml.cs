using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CollectibleCardGame;
using CollectibleCardGame.ViewModels;
using CollectibleCardGame.ViewModels.Frames;
using CollectibleCardGame.Views.Frames;
using CollectibleCardGame.Views.FramesShell;

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

            CardDecksFramePage frame = new CardDecksFramePage() { DataContext = new CardDecksPageViewModel() };
            frame.f1.DataContext = new FractionUserControlViewModel() { Name = "Калвария", Description = "Северная фракция", ImagePath = "/Images/northPicture.jpg" };
            frame.f2.DataContext = new FractionUserControlViewModel() { Name = "Магдебург", Description = "Южная фракцция", ImagePath = "/Images/southPicture.jpg" };
            frame.f3.DataContext = new FractionUserControlViewModel() { Name = "Чудовища", Description = "Монстры", ImagePath = "/Images/darksidePicture.jpg" };


            //LogInFramePageShell frame = new LogInFramePageShell();

            window.MainFrame.Content = frame;
            window.Show();
        }
    }
}
