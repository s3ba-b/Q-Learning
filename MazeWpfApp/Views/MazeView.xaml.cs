﻿using MazeWpfApp.Helpers;
using MazeWpfApp.ViewModels;
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

namespace MazeWpfApp.Views
{
    /// <summary>
    /// Interaction logic for MazeView.xaml
    /// </summary>
    public partial class MazeView : UserControl
    {
        public MazeView(MazeSettings settings)
        {
            InitializeComponent();
            ViewModel = new MazeViewModel(settings);
            DataContext = ViewModel; 
        }

        public MazeViewModel ViewModel { get; private set; }
    }
}
