using DataAccess.Repository;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;


namespace LuuThanhDatWPF
{
    /// <summary>
    /// Interaction logic for P_Static.xaml
    /// </summary>
    public partial class P_Static : Page
    {
		private DateTime fromDate;
		private DateTime toDate;
		private readonly IBookingDetailRepository _customerBookRoomRepository;
		private readonly IRoomRepository _roomRepository;

		class Static
		{
			public DateOnly Date { get; set; }
			public int NumberOfCustomer { get; set; }
			public decimal Revenue { get; set; }
        }

        public P_Static()
        {
            InitializeComponent();
			_customerBookRoomRepository = DIService.Instance.ServiceProvider.GetService<IBookingDetailRepository>();
			_roomRepository = DIService.Instance.ServiceProvider.GetService<IRoomRepository>();
        }

		private void FromDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
		{
			if (sender is DatePicker datePicker && datePicker.SelectedDate.HasValue)
			{
				fromDate = datePicker.SelectedDate.Value;
				DateTime selectedDate = datePicker.SelectedDate.Value;
				//MessageBox.Show($"From Date selected: {selectedDate.ToShortDateString()}");
			}
		}

		private void ToDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
		{
			if (sender is DatePicker datePicker && datePicker.SelectedDate.HasValue)
			{
				toDate = datePicker.SelectedDate.Value;
				DateTime selectedDate = datePicker.SelectedDate.Value;
				//MessageBox.Show($"To Date selected: {selectedDate.ToShortDateString()}");
			}
		}

        private void btn_Sort(object sender, RoutedEventArgs e)
        {
            if (toDate.CompareTo(fromDate) < 0)
            {
                MessageBox.Show($"Invalid Pick Date, try again!");
            }
            else
            {
                var listStatic = new List<Static>();
                var customerBookRoomList = _customerBookRoomRepository.GetAll();

                foreach (var customerBookRoom in customerBookRoomList)
                {
                    DateOnly start = customerBookRoom.StartDate > DateOnly.FromDateTime(fromDate) ? customerBookRoom.StartDate : DateOnly.FromDateTime(fromDate);
                    DateOnly end = customerBookRoom.EndDate < DateOnly.FromDateTime(toDate) ? customerBookRoom.EndDate : DateOnly.FromDateTime(toDate);

                    foreach (DateOnly day in EachDay(start, end))
                    {
                        var staticNeedFind = listStatic.FirstOrDefault(sta => sta.Date.CompareTo(day) == 0);

                        if (staticNeedFind == null)
                        {
                            staticNeedFind = new Static
                            {
                                Date = day,
                                NumberOfCustomer = 0,
                                Revenue = 0
                            };
                            listStatic.Add(staticNeedFind);
                        }

                        staticNeedFind.NumberOfCustomer++;
                        var room = _roomRepository.GetById(customerBookRoom.RoomId);
                        if (room != null)
                        {
                            staticNeedFind.Revenue += room.RoomPricePerDate;
                        }
                    }
                }

                if(listStatic .Count > 0)
                {
                    dataGrid.ItemsSource = listStatic.OrderByDescending(sta => sta.Revenue).ToList();
                }
                else
                {
                    MessageBox.Show($"Didn't have customer from {DateOnly.FromDateTime(fromDate)} to {DateOnly.FromDateTime(toDate)}!");
                }
            }
        }



        public IEnumerable<DateOnly> EachDay(DateOnly from, DateOnly thru)
        {
            for (var day = from; day <= thru; day = day.AddDays(1))
            {
                yield return day;
            }
        }
    }
}
