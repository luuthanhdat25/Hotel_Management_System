using DataAccess.Repository;
using LuuThanhDatWPF;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, EventArgs e)
        {
            var startUpWindow = new W_Admin();
            startUpWindow.Show();
        }
    }

}
