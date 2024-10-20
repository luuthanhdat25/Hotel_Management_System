using BusinessObjects;
using System.Windows;
using System.Windows.Controls;
using WpfApp;

namespace LuuThanhDatWPF
{
    /// <summary>
    /// Interaction logic for W_Customer.xaml
    /// </summary>
    public partial class W_Customer : Window
    {
        private readonly P_CustomerProfile customerProfilePage;
        private readonly P_CustomerReservationHistory reservationHistoryPage;

        public W_Customer(Customer customer)
        {
            InitializeComponent();

            customerProfilePage = new P_CustomerProfile(customer);
            reservationHistoryPage = new P_CustomerReservationHistory(customer);
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ContentFrame == null) return;

            var selectedTab = (sender as TabControl)?.SelectedIndex;

            switch (selectedTab)
            {
                case 0:
                    ContentFrame.Navigate(customerProfilePage);
                    break;
               
                case 1:
                    reservationHistoryPage.Reset();
                    ContentFrame.Navigate(reservationHistoryPage);
                    break;

                case 2:
                    W_Login loginWindow = new W_Login();
                    loginWindow.Show();
                    this.Close();
                    break;

                case 3:
                    this.Close();
                    break;
            }
        }
    }
}
