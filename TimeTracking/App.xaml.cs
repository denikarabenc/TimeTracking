using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TimeTracking.ViewModels;

namespace TimeTracking
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            MainWindowViewModel viewModel = new MainWindowViewModel();
            MainWindow window = new MainWindow(viewModel);
            window.Show();
        }
    }
}
