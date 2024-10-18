using BusinessObjects;
using DataAccess.Repository;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LuuThanhDatWPF
{
    /// <summary>
    /// Interaction logic for P_ReservationHistory.xaml
    /// </summary>
    public partial class P_ReservationHistory : Page
    {
        private readonly IBookingDetailRepository _customerBookRoomRepository;
        private readonly IRoomRepository _roomRepository;

        class BookingReservationHistory
        {
            public DateOnly FromDate { get; set; }
            public DateOnly ToDate { get; set; }

            public Room Room { get; set; }
            public decimal PricePerDate { get; set; }
            public int NumberOfDay { get; set; }
            public decimal TotalPrice { get; set; }
        }

        public P_ReservationHistory(Customer customer)
        {
            InitializeComponent();
            _customerBookRoomRepository = DIService.Instance.ServiceProvider.GetService<IBookingDetailRepository>();
            _roomRepository = DIService.Instance.ServiceProvider.GetService<IRoomRepository>();

            var reservationHistoryList = new List<BookingReservationHistory>();
            foreach (var customerBookRoom in _customerBookRoomRepository.GetAll())
            {
                Room room = _roomRepository.FindById(customerBookRoom.RoomId);

                if(room != null)
                {
                    var numberOfDay = CalculateDaysBetween(customerBookRoom.StartDate, customerBookRoom.EndDate);
                    BookingReservationHistory reservationHistory = new BookingReservationHistory
                    {
                        FromDate = customerBookRoom.StartDate,
                        ToDate = customerBookRoom.EndDate,
                        Room = room,
                        PricePerDate = room.RoomPricePerDate,
                        NumberOfDay = numberOfDay,
                        TotalPrice =  numberOfDay * room.RoomPricePerDate
                    };
                    reservationHistoryList.Add(reservationHistory);
                }
            }

            dataGrid.ItemsSource = reservationHistoryList;
        }

        private int CalculateDaysBetween(DateOnly fromDate, DateOnly toDate)
        {
            return (toDate.ToDateTime(TimeOnly.MinValue) - fromDate.ToDateTime(TimeOnly.MinValue)).Days;
        }
    }
}
