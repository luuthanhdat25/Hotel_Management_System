using BusinessObject;
using BusinessObjects;
using DataAccess.Repository;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace LuuThanhDatWPF
{
    /// <summary>
    /// Interaction logic for P_BookingReservation.xaml
    /// </summary>
    public partial class P_BookingReservation : Page
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IBookingDetailRepository _bookDetailRepository;
        private readonly IBookingReservationRepository _bookingReservationRepository;
        private Customer currentCustomer;

        public P_BookingReservation()
        {
            InitializeComponent();

            _customerRepository = DIService.Instance.ServiceProvider.GetService<ICustomerRepository>();
            _bookDetailRepository = DIService.Instance.ServiceProvider.GetService<IBookingDetailRepository>();
            _bookingReservationRepository = DIService.Instance.ServiceProvider.GetService<IBookingReservationRepository>();
        }

        public void Reset()
        {
            tbSearchCustomerByName.Text = string.Empty;
            tbCustomerFullName.Text = string.Empty;
            tbEmail.Text = string.Empty;

            dg_Customer.ItemsSource = _customerRepository.GetActiveList();
            dg_BookingReservation.ItemsSource = null;
            dg_BookDetail.ItemsSource = null;

            dp_StarDate.Text = dp_EndDate.Text = string.Empty;
        }

        private void btn_SearchCustomerName_onClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbSearchCustomerByName.Text))
            {
                dg_Customer.ItemsSource = _customerRepository.GetActiveList();
            }
            else
            {
                dg_Customer.ItemsSource = _customerRepository.FindActiveByName(tbSearchCustomerByName.Text);
            }
        }

        private void dgCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is System.Windows.Controls.DataGrid dataGrid)
            {
                var selectedEntity = dataGrid.SelectedItem as Customer;

                if (selectedEntity != null)
                {
                    tbCustomerFullName.Text = selectedEntity.CustomerFullName;
                    tbEmail.Text = selectedEntity.EmailAddress;

                    dg_BookingReservation.ItemsSource = _bookingReservationRepository.GetByCustomerId(selectedEntity.CustomerId);

                    dg_BookDetail.ItemsSource = null;
                    dp_StarDate.Text = dp_EndDate.Text = string.Empty;

                    currentCustomer = selectedEntity;
                }
            }
        }

        private void dgBookingReservation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is System.Windows.Controls.DataGrid dataGrid)
            {
                var selectedEntiry = dataGrid.SelectedItem as BookingReservation;

                if (selectedEntiry != null)
                {
                    dg_BookDetail.ItemsSource = _bookDetailRepository.GetByBookingReservationId(selectedEntiry.BookingReservationId);
                }
            }
        }

        private void btn_SortBookingReservationDate_onClick(object sender, RoutedEventArgs e)
        {
            if(currentCustomer == null)
            {
                MessageBox.Show("Please select 1 Customer!");
                return;
            }

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
            
            List<BookingReservation> bookingReservationList = _bookingReservationRepository.GetByCustomerId(currentCustomer.CustomerId);

            bookingReservationList = (from bookingReservation in bookingReservationList
                                      where bookingReservation.BookingDate >= startDateOnly && bookingReservation.BookingDate <= endDateOnly
                                      select bookingReservation).ToList();

            dg_BookingReservation.ItemsSource = bookingReservationList;
            dg_BookDetail.ItemsSource = null;
        }
    }
}
