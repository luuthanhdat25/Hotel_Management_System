using BusinessObject;
using BusinessObjects;
using DataAccess.Repository.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace LuuThanhDatWPF
{
    /// <summary>
    /// Interaction logic for W_CreateRoom.xaml
    /// </summary>
    public partial class W_CreateRoom : Window
    {
		private readonly IRoomTypeRepository _roomTypeRepository;
		private readonly IRoomRepository _roomRepository;
		private readonly P_RoomManagement roomManagementPage;

        public W_CreateRoom(P_RoomManagement roomManagement)
        {
            InitializeComponent();
			_roomRepository = DIService.Instance.ServiceProvider.GetService<IRoomRepository>();
			_roomTypeRepository = DIService.Instance.ServiceProvider.GetService<IRoomTypeRepository>();
			this.roomManagementPage = roomManagement;

			List<RoomType> roomTypes = _roomTypeRepository.GetAll().ToList();
			cb_RoomType.ItemsSource = roomTypes;
		}

		public void btn_cancel(object sender, RoutedEventArgs e)
		{
			Close();
		}

		public void btn_submit(object sender, RoutedEventArgs e)
		{
			if (!IsAllTextboxEntered())
			{
				MessageBox.Show("Please input all field!");
				return;
			}

			string roomnNumber = tb_RoomNumber.Text;
			string roomDesciption = tb_RoomDescription.Text;

			int maxCapacity;
			try
			{
				maxCapacity = int.Parse(tb_RoomMaxCapacity.Text);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error");
				return;
			}

			decimal pricePerDate;
			try
			{
				pricePerDate = decimal.Parse(tb_PricePerDate.Text);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error");
				return;
			}

			RoomType roomType = (RoomType)cb_RoomType.SelectedItem;

			Room newRoom = new Room
			{
				RoomName = roomnNumber,
				RoomDescription = roomDesciption,
				RoomMaxCapacity = maxCapacity,
				RoomPricePerDate = pricePerDate,
				RoomStatus = RoomStatus.Active,
				RoomType = roomType,
				RoomTypeId = roomType.RoomTypeId
			};
			_roomRepository.Add(newRoom);
			MessageBox.Show("Add New Room Succesfully!");
			roomManagementPage.UpdateDataGrid();
			this.Close();
		}

		private bool IsAllTextboxEntered()
		{
			if (string.IsNullOrEmpty(tb_RoomNumber.Text)) return false;
			if (string.IsNullOrEmpty(tb_RoomDescription.Text)) return false;
			if (string.IsNullOrEmpty(tb_RoomMaxCapacity.Text)) return false;
			if (string.IsNullOrEmpty(tb_PricePerDate.Text)) return false;
			if (string.IsNullOrEmpty(cb_RoomType.Text)) return false;
			return true;
		}
	}
}
