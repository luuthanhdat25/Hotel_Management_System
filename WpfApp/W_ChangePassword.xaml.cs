using BusinessObjects;
using DataAccess.Repository.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace LuuThanhDatWPF
{
    /// <summary>
    /// Interaction logic for W_ChangePassword.xaml
    /// </summary>
    public partial class W_ChangePassword : Window
    {
        private Customer customer;
        private readonly ICustomerRepository _customerRepository;

        public W_ChangePassword(Customer customer)
        {
            InitializeComponent();
            this.customer = customer;
            _customerRepository = DIService.Instance.ServiceProvider.GetService<ICustomerRepository>();
        }

        public void btn_submit(object sender, RoutedEventArgs e)
        {
            string oldPass = pbOldPassword.Password;
            string newPass = pbNewPassword.Password;
            string confirmNewPass = pbReNewPassword.Password;

            if (newPass.Equals(oldPass))
            {
                MessageBox.Show("New password equal Old password, please input new password!");
            }
            else if (!newPass.Equals(confirmNewPass))
            {
                MessageBox.Show("Confirm password invalid, please input again!");
            }
            else
            {
                customer.Password = newPass;
                _customerRepository.Update(customer);
                MessageBox.Show("Update Password Successfully!");
                Close();
            }
        }

        public void btn_cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
