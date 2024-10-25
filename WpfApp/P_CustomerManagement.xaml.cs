using BusinessObjects;
using DataAccess.Repository.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace LuuThanhDatWPF
{
    /// <summary>
    /// Interaction logic for P_CustomerManagement.xaml
    /// </summary>
    public partial class P_CustomerManagement : Page
    {
        private Customer currentSelect;
		private readonly ICustomerRepository customerRepository;

		public P_CustomerManagement()
        {
            InitializeComponent();
			customerRepository = DIService.Instance.ServiceProvider.GetService<ICustomerRepository>();

			UpdateDataGrid();
        }


        public void UpdateDataGrid()
        {
			dataGrid.ItemsSource = customerRepository.GetAll(); 
		}

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is DataGrid dataGrid)
            {
                var selectedEntiry = dataGrid.SelectedItem as Customer;

                if (selectedEntiry != null)
                {

                    tbId.Text = selectedEntiry.CustomerId.ToString();
                    tbFullName.Text = selectedEntiry.CustomerFullName;
                    tbTelephone.Text = selectedEntiry.Telephone;
                    tbEmail.Text = selectedEntiry.EmailAddress;
                    tbBirthday.Text = selectedEntiry.CustomerBirthday.ToString();
                    tbStatus.Text = selectedEntiry.CustomerStatus.ToString();

                    currentSelect = selectedEntiry;
					if (selectedEntiry.CustomerStatus.ToString() == CustomerStatus.Active.ToString())
					{
						btn_SwitchStatus.Content = CustomerStatus.Disable.ToString();
					}
					else
					{
						btn_SwitchStatus.Content = CustomerStatus.Active.ToString();
					}
				}
            }
        }

        private void btn_Delete(object sender, RoutedEventArgs e)
        {
			if (currentSelect == null) return;

            if(btn_SwitchStatus.Content.ToString() == CustomerStatus.Active.ToString())
            {
				btn_SwitchStatus.Content = CustomerStatus.Disable.ToString();
				customerRepository.UpdateCustomerStatus(currentSelect, CustomerStatus.Active);
				tbStatus.Text = currentSelect.CustomerStatus.ToString();
			}
            else
            {
				MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Disable Customer Confirm?", MessageBoxButton.YesNo);
				if (messageBoxResult == MessageBoxResult.Yes)
                {
				    btn_SwitchStatus.Content = CustomerStatus.Active.ToString();
				    customerRepository.UpdateCustomerStatus(currentSelect, CustomerStatus.Disable);
				    tbStatus.Text = currentSelect.CustomerStatus.ToString();
				}
			}
            UpdateDataGrid();
		}

        private void btn_Create(object sender, RoutedEventArgs e)
        {
            W_CreateCustomer createCustomer = new W_CreateCustomer(this);
            createCustomer.Show();
        }

        private void btn_SearchByName(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbSearchbyText.Text))
            {
                UpdateDataGrid();
            }
            else
            {
                dataGrid.ItemsSource = customerRepository.FindByName(tbSearchbyText.Text);
            }
        }
    }
}
