using DataAccess.Repository.Interface;
using LuuThanhDatWPF;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
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
            //IAccountRepository repo = DIService.Instance.ServiceProvider.GetService<IAccountRepository>();  
            var startUpWindow = new W_Login();
            //var startUpWindow = new W_Admin(repo.GetByEmail("admin@gmail.com"));
            startUpWindow.Show();
        }
    }

}
