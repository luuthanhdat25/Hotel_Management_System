using BusinessObject;
using System.Windows;
using System.Windows.Controls;
using WpfApp;

namespace LuuThanhDatWPF
{
    /// <summary>
    /// Interaction logic for W_Admin.xaml
    /// </summary>
    public partial class W_Admin : Window
    {
        private readonly P_CustomerManagement customerManagementPage;
        private readonly P_RoomManagement roomManagementPage;
        private readonly P_Static staticPage;
        private readonly P_BookRoom bookRoomPage;
        private readonly P_BookingReservation bookingReservation;

        public W_Admin()
        {
            InitializeComponent();

            customerManagementPage = new P_CustomerManagement();
			roomManagementPage = new P_RoomManagement();
			staticPage = new P_Static();
            bookRoomPage = new P_BookRoom();
            bookingReservation = new P_BookingReservation();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ContentFrame == null) return;

            var selectedTab = (sender as TabControl)?.SelectedIndex;

            switch (selectedTab)
            {
                case 0:
                    bookRoomPage.Reset();
                    ContentFrame.Navigate(bookRoomPage);
                    break;

                case 1:
                    bookingReservation.Reset();
                    ContentFrame.Navigate(bookingReservation);
                    break;

                case 2:
                    ContentFrame.Navigate(customerManagementPage);
                    break;

                case 3:
                    ContentFrame.Navigate(roomManagementPage);
                    break;

                case 4:
                    ContentFrame.Navigate(staticPage);
                    break;

                case 5:
                    W_Login loginWindow = new W_Login();
                    loginWindow.Show();
                    this.Close();
                    break;
                case 6:
                    this.Close();
                    break;
            }
        }
    }
}
