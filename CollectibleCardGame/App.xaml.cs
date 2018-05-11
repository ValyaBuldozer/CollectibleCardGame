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
using CollectibleCardGame.ViewModels.Elements;
using CollectibleCardGame.ViewModels.Frames;
using CollectibleCardGame.ViewModels.UserControls;
using CollectibleCardGame.ViewModels.Windows;
using CollectibleCardGame.Views.Frames;
using CollectibleCardGame.Views.FramesShell;
using CollectibleCardGame.Views.UserControls;
using GameData.Models.Cards;
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
            //MainWindow window = new MainWindow();
            //GameEngineFramePage framePage = new GameEngineFramePage();
            //GameEngineViewModel viewModel = new GameEngineViewModel();
            //    viewModel.PlayerCards.Add(new CardViewModel(new UnitCard()
            //    {
            //        Cost = 5, Name = "test", Description = "test", BaseAttack = 1, BaseHP = 1
            //    }));
            //viewModel.PlayerCards.Add(new CardViewModel(new UnitCard()
            //{
            //    Cost = 5,
            //    Name = "test",
            //    Description = "test",
            //    BaseAttack = 1,
            //    BaseHP = 1
            //}));
            //viewModel.PlayerCards.Add(new CardViewModel(new UnitCard()
            //{
            //    Cost = 5,
            //    Name = "test",
            //    Description = "test",
            //    BaseAttack = 1,
            //    BaseHP = 1
            //}));
            //viewModel.PlayerCards.Add(new CardViewModel(new SpellCard()
            //{
            //    Cost = 5,
            //    Name = "test",
            //    Description = "test"
            //}));
            //framePage.ViewModel = viewModel;
            //window.MainFrame.Content = framePage;
            //window.Show();
            UnityKernel.InitializeKernel();
            UnityKernel.Get<MainWindow>().Show();
            UnityKernel.Get<IGlobalController>().OnStartup();


        }
    }
}
