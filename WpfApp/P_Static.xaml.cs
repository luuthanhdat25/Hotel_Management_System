using BusinessObject;
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
		private readonly IBookingReservationRepository _bookingReservationRepository;

		class Static
		{
			public DateOnly Date { get; set; }
			public int NumberOfCustomer { get; set; }
			public decimal Revenue { get; set; }
        }

        public P_Static()
        {
            InitializeComponent();
            _bookingReservationRepository = DIService.Instance.ServiceProvider.GetService<IBookingReservationRepository>();
        }

        private void btn_Sort(object sender, RoutedEventArgs e)
        {
            DateTime? startDate = dp_StarDate.SelectedDate;
            DateTime? endDate = dp_EndDate.SelectedDate;

            if (startDate == null || endDate == null)
            {
                System.Windows.MessageBox.Show("Please choose Start Date and End Date!");
                return;
            }

            if (endDate.Value.Date > DateTime.Now.Date)
            {
                System.Windows.MessageBox.Show("End Date must be today or in the past!");
                return;
            }

            if (startDate.Value.CompareTo(endDate.Value) > 0)
            {
                System.Windows.MessageBox.Show("Invalid Pick Date, try again!");
                return;
            }

            DateOnly startDateReadOnly = DateOnly.FromDateTime(startDate.Value);
            DateOnly endDateReadOnly = DateOnly.FromDateTime(endDate.Value);

            List<BookingReservation> bookingReservationList = _bookingReservationRepository.GetAll();  
            var listStatic = new List<Static>();

            foreach (var bookingReservation in bookingReservationList)
            {
                if(bookingReservation.BookingDate >= startDateReadOnly && bookingReservation.BookingDate <= endDateReadOnly)
                {
                    var staticNeedFind = listStatic.FirstOrDefault(sta => sta.Date.CompareTo(bookingReservation.BookingDate) == 0);

                    if (staticNeedFind == null)
                    {
                        staticNeedFind = new Static
                        {
                            Date = bookingReservation.BookingDate,
                            NumberOfCustomer = 0,
                            Revenue = 0
                        };
                        listStatic.Add(staticNeedFind);
                    }

                    staticNeedFind.NumberOfCustomer++;
                    staticNeedFind.Revenue += bookingReservation.TotalPrice;
                }
            }

            if(listStatic .Count > 0)
            {
                dataGrid.ItemsSource = listStatic.OrderByDescending(sta => sta.Revenue).ToList();
            }

            decimal totalRevenue = 0;
            listStatic.ForEach(stat => totalRevenue += stat.Revenue);
            tbTotalPrice.Text = totalRevenue.ToString();
        }
    }
}
