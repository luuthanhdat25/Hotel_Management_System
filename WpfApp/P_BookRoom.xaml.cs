using BusinessObject;
using BusinessObjects;
using DataAccess.Repository.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LuuThanhDatWPF
{
    /// <summary>
    /// Interaction logic for P_BookRoom.xaml
    /// </summary>
    public partial class P_BookRoom : Page
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IBookingDetailRepository _bookDetailRepository;
        private readonly IBookingReservationRepository _bookingReservationRepository;

        private Customer currentCustomer;
        private Room currentRoom;
        private ObservableCollection<BookingDetail> bookingDetailList = new ObservableCollection<BookingDetail>();

        public P_BookRoom()
        {
            InitializeComponent();
            _customerRepository = DIService.Instance.ServiceProvider.GetService<ICustomerRepository>();
			_roomRepository = DIService.Instance.ServiceProvider.GetService<IRoomRepository>();
			_roomTypeRepository = DIService.Instance.ServiceProvider.GetService<IRoomTypeRepository>();
            _bookDetailRepository = DIService.Instance.ServiceProvider.GetService<IBookingDetailRepository>();
            _bookingReservationRepository = DIService.Instance.ServiceProvider.GetService<IBookingReservationRepository>();

            List<RoomType> roomTypes = _roomTypeRepository.GetAll().ToList();
            cb_RoomType.ItemsSource = roomTypes;

            bookingDetailList.CollectionChanged += UpdateTotalPrice;
        }

        private void UpdateTotalPrice(object? sender, NotifyCollectionChangedEventArgs e)
        {
            decimal price = 0;
            foreach (var booking in bookingDetailList)
            {
                price += booking.ActualPrice;
            }
            tbTotalPrice.Text = price.ToString();
        }

        public void Reset()
        {
            tbSearchCustomerByName.Text = string.Empty;
            tbCustomerFullName.Text = string.Empty;
            tbEmail.Text = string.Empty;
            UpdateCustomerDataGrid();

            tbRoomId.Text = string.Empty;
            cb_RoomType.Text = string.Empty;
            dp_StarDate.Text = string.Empty;
            dp_EndDate.Text = string.Empty;

            bookingDetailList.Clear();
            UpdateBookDetailDataGrid();
            dg_Room.ItemsSource = new List<Room>();
        }

        public void UpdateBookDetailDataGrid()
        {
            dg_BookDetail.ItemsSource = bookingDetailList;
        }

        public void UpdateCustomerDataGrid()
        {
            dg_Customer.ItemsSource = _customerRepository.GetActiveList();
        }

        // Event for 'Remove' button in DataGrid (dgBookDetail)
        private void dgBookDetail_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is System.Windows.Controls.DataGrid dataGrid)
            {
                var selectedEntiry = dataGrid.SelectedItem as BookingDetail;

                if (selectedEntiry != null)
                {
                    MessageBoxResult result = MessageBox.Show("Do you want to remove Room " + selectedEntiry.RoomId, "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        bookingDetailList.Remove(selectedEntiry);
                    }
                }
            }
        }

        // Event for 'Book' button
        private void btn_Book_onClick(object sender, RoutedEventArgs e)
        {
            if (currentCustomer == null)
            {
                MessageBox.Show("Please select 1 Customer!");
                return;
            }

            if (bookingDetailList.Count == 0) {
                MessageBox.Show("Need to book atleast 1 Room!");
                return;
            }

            BookingReservation bookingReservation = new BookingReservation
            {
                BookingDate = DateOnly.FromDateTime(DateTime.Now),
                TotalPrice = decimal.Parse(tbTotalPrice.Text),
                CustomerId = currentCustomer.CustomerId,
                Customer = currentCustomer,
            };

            _bookingReservationRepository.Add(bookingReservation);

            int bookingReservationId = bookingReservation.BookingReservationId;

            foreach (var bookDetail in bookingDetailList)
            {
                bookDetail.BookingReservationId = bookingReservationId;
            }

            _bookDetailRepository.AddRange(bookingDetailList.ToList());

            W_BookReservation_PopUp w_BookReservation_PopUp = new W_BookReservation_PopUp(bookingReservationId);
            w_BookReservation_PopUp.Show();
            Reset();
        }

        // Event for 'Search' button for searching customer by name
        private void btn_SearchCustomerName_onClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbSearchCustomerByName.Text))
            {
                UpdateCustomerDataGrid();
            }
            else
            {
                dg_Customer.ItemsSource = _customerRepository.GetAllActiveByName(tbSearchCustomerByName.Text);
            }
        }

        // Event for 'SelectionChanged' in Customer DataGrid (dg_Customer)
        private void dgCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is System.Windows.Controls.DataGrid dataGrid)
            {
                var selectedEntiry = dataGrid.SelectedItem as Customer;

                if (selectedEntiry != null)
                {

                    tbCustomerFullName.Text = selectedEntiry.CustomerFullName;
                    tbEmail.Text = selectedEntiry.Account.EmailAddress;

                    currentCustomer = selectedEntiry;
                }
            }
        }

        // Event for 'Search' button for searching room by id
        private void btn_SortRoom_onClick(object sender, RoutedEventArgs e)
        {
            RoomType roomType = (RoomType)cb_RoomType.SelectedItem;
            DateTime? startDate = dp_StarDate.SelectedDate;
            DateTime? endDate = dp_EndDate.SelectedDate;

            if (startDate == null || endDate == null)
            {
                MessageBox.Show("Please choose Start Date and End Date!");
                return;
            }

            if (startDate.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show("Start Date must be today or in the future!");
                return;
            }

            if (startDate.Value.CompareTo(endDate.Value) > 0)
            {
                MessageBox.Show("Invalid Pick Date, try again!");
                return;
            }

            List<Room> roomList = _roomRepository.GetAllActive().ToList();
            List<BookingDetail> bookingDetailHistoryList = _bookDetailRepository.GetAll().ToList();

            if(roomType != null)
            {
                roomList = (from room in roomList
                            where room.RoomType.Equals(roomType)
                            select room).ToList();
            }

            DateOnly selectedStartDate = DateOnly.FromDateTime(startDate.Value);
            DateOnly selectedEndDate = DateOnly.FromDateTime(endDate.Value);

            roomList = roomList.Where(room =>
                !bookingDetailHistoryList.Any(bd =>
                    bd.RoomId == room.RoomId && IsOverlap(bd.StartDate, bd.EndDate, selectedStartDate, selectedEndDate)
                )).ToList();

            roomList = roomList.Where(room =>
                !bookingDetailList.Any(bd =>
                    bd.RoomId == room.RoomId && IsOverlap(bd.StartDate, bd.EndDate, selectedStartDate, selectedEndDate)
                )).ToList();

            dg_Room.ItemsSource = roomList;
        }

        public bool IsOverlap(DateOnly startDateA, DateOnly endDateA, DateOnly startDateB, DateOnly endDateB)
        {
            return startDateA <= endDateB && startDateB <= endDateA;
        }


        // Event for 'SelectionChanged' in Room DataGrid (dg_Room)
        private void dgRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is System.Windows.Controls.DataGrid dataGrid)
            {
                var selectedEntiry = dataGrid.SelectedItem as Room;

                if (selectedEntiry != null)
                {
                    tbRoomId.Text = "Room: " + selectedEntiry.RoomId.ToString();
                    tbRoomName.Text = selectedEntiry.RoomName;
                    currentRoom = selectedEntiry;
                }
            }
        }

        private void dgRoom_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dg_Room.SelectedItem is Room selectedRoom)
            {
                if (selectedRoom != null)
                {
                    DateTime? startDate = dp_StarDate.SelectedDate;
                    DateTime? endDate = dp_EndDate.SelectedDate;

                    if (startDate == null || endDate == null)
                    {
                        MessageBox.Show("Please choose Start Date and End Date!");
                        return;
                    }

                    if (startDate.Value.Date < DateTime.Now.Date)
                    {
                        MessageBox.Show("Start Date must be today or in the future!");
                        return;
                    }

                    if (startDate.Value.CompareTo(endDate.Value) > 0)
                    {
                        MessageBox.Show("Invalid Pick Date, try again!");
                        return;
                    }

                    DateOnly startDateOnly = DateOnly.FromDateTime(startDate.Value);
                    DateOnly endDateOnly = DateOnly.FromDateTime(endDate.Value);

                    if (IsOverLapInReservation(startDateOnly, endDateOnly))
                    {
                        MessageBox.Show($"Room {selectedRoom.RoomId} from {startDateOnly} to {endDateOnly} is already booked.");
                    }
                    else
                    {
                        var result = MessageBox.Show($"Do you want to add Room {selectedRoom.RoomId} to your booking from {startDateOnly} to {endDateOnly}?",
                                                     "Confirm Booking",
                                                     MessageBoxButton.YesNo,
                                                     MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                            BookingDetail bookingDetail = new BookingDetail()
                            {
                                RoomId = selectedRoom.RoomId,
                                Room = selectedRoom,
                                StartDate = startDateOnly,
                                EndDate = endDateOnly,
                                ActualPrice = (decimal)CalculateDaysBetween(startDateOnly, endDateOnly) * selectedRoom.RoomPricePerDate
                            };
                            bookingDetailList.Add(bookingDetail);
                        }
                    }
                }
            }
        }



        private bool IsOverLapInReservation(DateOnly startDate, DateOnly endDate)
        {
            foreach (var bookingDetail in bookingDetailList)
            {
                if(currentRoom.RoomId == bookingDetail.RoomId)
                {
                    if (IsOverlap(startDate, endDate, bookingDetail.StartDate, bookingDetail.EndDate))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private int CalculateDaysBetween(DateOnly fromDate, DateOnly toDate)
        {
            return (toDate.ToDateTime(TimeOnly.MinValue) - fromDate.ToDateTime(TimeOnly.MinValue)).Days;
        }
    }
}
