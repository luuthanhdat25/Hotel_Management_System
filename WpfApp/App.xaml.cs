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
            var startUpWindow = new W_Login();
            startUpWindow.Show();
        }
    }

}
