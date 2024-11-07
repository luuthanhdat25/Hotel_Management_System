using BusinessObject;
using BusinessObjects;
using DataAccess.Repository.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LuuThanhDatWPF
{
    /// <summary>
    /// Interaction logic for P_ReservationHistory.xaml
    /// </summary>
    public partial class P_CustomerReservationHistory : Page
    {
        private readonly IBookingDetailRepository _bookDetailRepository;
        private readonly IBookingReservationRepository _bookingReservationRepository;
        private Customer currentCustomer;

        public P_CustomerReservationHistory(Customer customer)
        {
            InitializeComponent();
            currentCustomer = customer;
            _bookDetailRepository = DIService.Instance.ServiceProvider.GetService<IBookingDetailRepository>();
            _bookingReservationRepository = DIService.Instance.ServiceProvider.GetService<IBookingReservationRepository>();
        }

        public void Reset()
        {
            dg_BookingReservation.ItemsSource = _bookingReservationRepository.GetAll().ToList();
            
            dg_BookDetail.ItemsSource = null;
            dp_StarDate.Text = dp_EndDate.Text = string.Empty;
        }

        private void btn_SortBookingReservationDate_onClick(object sender, RoutedEventArgs e)
        {
            DateTime? startDate = dp_StarDate.SelectedDate;
            DateTime? endDate = dp_EndDate.SelectedDate;

            if (startDate == null || endDate == null)
            {
                MessageBox.Show("Please choose Start Date and End Date!");
                return;
            }

            if (startDate.Value.CompareTo(endDate.Value) > 0)
            {
                MessageBox.Show("Invalid Pick Date, try again!");
                return;
            }

            DateOnly startDateOnly = DateOnly.FromDateTime(startDate.Value);
            DateOnly endDateOnly = DateOnly.FromDateTime(endDate.Value);

            List<BookingReservation> bookingReservationList = _bookingReservationRepository.GetAllByCustomerId(currentCustomer.CustomerId).ToList();

            bookingReservationList = (from bookingReservation in bookingReservationList
                                      where bookingReservation.BookingDate >= startDateOnly && bookingReservation.BookingDate <= endDateOnly
                                      select bookingReservation).ToList();

            dg_BookingReservation.ItemsSource = bookingReservationList;
            dg_BookDetail.ItemsSource = null;
        }

        private void dgBookingReservation_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is System.Windows.Controls.DataGrid dataGrid)
            {
                var selectedEntity = dataGrid.SelectedItem as BookingReservation;
                if (selectedEntity != null)
                {
                    dg_BookDetail.ItemsSource = _bookDetailRepository
                        .GetByBookingReservationId(selectedEntity.BookingReservationId)
                        .ToList();
                }
            }
        }
    }
}
