﻿using System;
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
using CollectibleCardGame.ViewModels.Windows;
using Unity.Attributes;
using CollectibleCardGame.ViewModels.Frames;

namespace CollectibleCardGame.Views.Frames
{
    /// <summary>
    /// Логика взаимодействия для MainMenuFramePage.xaml
    /// </summary>
    public partial class MainMenuFramePage : Page
    {
        [Dependency]
        public MenuFramePageViewModel ViewModel
        {
            get => DataContext as MenuFramePageViewModel;
            set => DataContext = value;
        }

        public MainMenuFramePage()
        {
            InitializeComponent();
        }
    }
}
