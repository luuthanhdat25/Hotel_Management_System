using BusinessObject;
using DataAccess.Repository.Interface;
using Microsoft.Extensions.DependencyInjection;
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
using System.Windows.Shapes;

namespace LuuThanhDatWPF
{
    /// <summary>
    /// Interaction logic for W_BookReservation_PopUp.xaml
    /// </summary>
    public partial class W_BookReservation_PopUp : Window
    {
        private readonly IBookingDetailRepository _bookDetailRepository;
        private readonly IBookingReservationRepository _bookingReservationRepository;

        public W_BookReservation_PopUp(int bookingReservationId)
        {
            InitializeComponent();

            _bookDetailRepository = DIService.Instance.ServiceProvider.GetService<IBookingDetailRepository>();
            _bookingReservationRepository = DIService.Instance.ServiceProvider.GetService<IBookingReservationRepository>();

            BookingReservation bookingReservation = _bookingReservationRepository.GetById(bookingReservationId);
            tbCustomerFullName.Text = bookingReservation.Customer.CustomerFullName;
            tbEmail.Text = bookingReservation.Customer.Account.EmailAddress;
            dg_BookDetail.ItemsSource = _bookDetailRepository.GetByBookingReservationId(bookingReservationId);
            tbTotalPrice.Text = bookingReservation.TotalPrice.ToString();
        }

        private void btn_Close_onClick(object sender, RoutedEventArgs e)
        {
            this.Close(); 
        }

    }
}
