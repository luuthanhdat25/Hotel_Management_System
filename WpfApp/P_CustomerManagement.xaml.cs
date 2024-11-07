using BusinessObject;
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
        private readonly IAccountRepository accountRepository;

		public P_CustomerManagement()
        {
            InitializeComponent();
			customerRepository = DIService.Instance.ServiceProvider.GetService<ICustomerRepository>();
            accountRepository = DIService.Instance.ServiceProvider.GetService<IAccountRepository>();

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
                    tbEmail.Text = selectedEntiry.Account.EmailAddress;
                    tbBirthday.Text = selectedEntiry.CustomerBirthday.ToString();
                    tbStatus.Text = selectedEntiry.Account.AccountStatus.ToString();

                    currentSelect = selectedEntiry;
					if (selectedEntiry.Account.AccountStatus == AccountStatus.Active)
					{
						btn_SwitchStatus.Content = AccountStatus.Disable.ToString();
					}
					else
					{
						btn_SwitchStatus.Content = AccountStatus.Active.ToString();
					}
				}
            }
        }

        private void btn_Delete(object sender, RoutedEventArgs e)
        {
			if (currentSelect == null) return;

            if(btn_SwitchStatus.Content.ToString() == AccountStatus.Active.ToString())
            {
				btn_SwitchStatus.Content = AccountStatus.Disable.ToString();
                Account accountUpdate = currentSelect.Account;
                accountUpdate.AccountStatus = AccountStatus.Active;
                accountRepository.Update(accountUpdate);
				tbStatus.Text = accountUpdate.AccountStatus.ToString();
			}
            else
            {
				MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Disable Customer Confirm?", MessageBoxButton.YesNo);
				if (messageBoxResult == MessageBoxResult.Yes)
                {
				    btn_SwitchStatus.Content = AccountStatus.Active.ToString();
                    Account accountUpdate = currentSelect.Account;
                    accountUpdate.AccountStatus = AccountStatus.Disable;

                    accountRepository.Update(accountUpdate);
                    tbStatus.Text = accountUpdate.AccountStatus.ToString();
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
                dataGrid.ItemsSource = customerRepository.GetAllByName(tbSearchbyText.Text);
            }
        }
    }
}
