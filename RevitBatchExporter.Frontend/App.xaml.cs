using RevitBatchExporter.Frontend.Stores;
using RevitBatchExporter.Frontend.ViewModels;
using RevitBatchExporter.Frontend.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RevitBatchExporter.Frontend
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {

        }
        protected override void OnStartup(StartupEventArgs e)
        {
            NavigationStore navigationStore = new NavigationStore();
            navigationStore.CurrentViewModel = new HomeViewModel();
            MainView mainWindow = new MainView()
            {
                DataContext = new MainViewModel(navigationStore)
            };
            mainWindow.Show();

            base.OnStartup(e);
        }
    }
}
