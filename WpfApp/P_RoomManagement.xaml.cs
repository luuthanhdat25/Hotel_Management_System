using BusinessObjects;
using DataAccess.Repository.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace LuuThanhDatWPF
{
    /// <summary>
    /// Interaction logic for P_RoomManagement.xaml
    /// </summary>
    public partial class P_RoomManagement : Page
    {
        private Room currentSelect;
        private readonly IRoomRepository _roomRepository;

        public P_RoomManagement()
        {
            InitializeComponent();
			_roomRepository = DIService.Instance.ServiceProvider.GetService<IRoomRepository>();
            
            UpdateDataGrid();
        }

        public void UpdateDataGrid()
        {
            var rooms = _roomRepository.GetAll().ToList(); 
            dataGrid.ItemsSource = rooms; 
        }


        public void UpdateRoomSelected()
        {
            tbId.Text = currentSelect.RoomId.ToString();
            tbRoomNumber.Text = currentSelect.RoomName;
            tbDescription.Text = currentSelect.RoomDescription;
            tbMaxCapacity.Text = currentSelect.RoomMaxCapacity.ToString();
            tbStatus.Text = currentSelect.RoomStatus.ToString();
            tbPricePerDate.Text = currentSelect.RoomPricePerDate.ToString();
            tbRoomType.Text = currentSelect.RoomType.RoomTypeName.ToString();
        }

		private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is DataGrid dataGrid)
            {
                var selectedEntiry = dataGrid.SelectedItem as Room;

                if (selectedEntiry != null)
                {
                    tbId.Text = selectedEntiry.RoomId.ToString();
                    tbRoomNumber.Text = selectedEntiry.RoomName;
                    tbDescription.Text = selectedEntiry.RoomDescription;
                    tbMaxCapacity.Text = selectedEntiry.RoomMaxCapacity.ToString();
                    tbStatus.Text = selectedEntiry.RoomStatus.ToString();
                    tbPricePerDate.Text = selectedEntiry.RoomPricePerDate.ToString();
                    tbRoomType.Text = selectedEntiry.RoomType.RoomTypeName.ToString();

                    currentSelect = selectedEntiry;
                }
            }
        }

        private void btn_Create(object sender, RoutedEventArgs e)
        {
            var createWindown = new W_CreateRoom(this);
            createWindown.Show();
        }

        private void btn_Update(object sender, RoutedEventArgs e)
        {
            if (currentSelect == null) return;
            var updateWindown = new W_UpdateRoom(currentSelect, this);
            updateWindown.Show();
        }

        private void btn_Delete(object sender, RoutedEventArgs e)
        {
            if (currentSelect == null) return;
			MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                _roomRepository.Delete(currentSelect);
                currentSelect = null;
                UpdateDataGrid();

                tbId.Text = string.Empty;
                tbRoomNumber.Text = string.Empty;
                tbDescription.Text = string.Empty;
                tbMaxCapacity.Text = string.Empty;
                tbStatus.Text = string.Empty;
                tbPricePerDate.Text = string.Empty;
                tbRoomType.Text = string.Empty;
            }
		}

        private void btn_SearchById(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbSearchbyText.Text))
            {
                UpdateDataGrid();
            }
            else
            {
				int id;
				try
				{
					id = int.Parse(tbSearchbyText.Text);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Error");
					throw;
				}


				Room room = _roomRepository.GetById(id);

				if (room == null)
				{
					MessageBox.Show("Not find");
				}
				else
				{
					dataGrid.ItemsSource = new List<Room>() { room };
				}
			}
        }
    }
}
