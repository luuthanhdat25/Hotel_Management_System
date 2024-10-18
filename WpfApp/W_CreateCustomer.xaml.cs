using BusinessObjects;
using DataAccess.Repository;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;
using System.Windows;

namespace LuuThanhDatWPF
{
    /// <summary>
    /// Interaction logic for W_CreateCustomer.xaml
    /// </summary>
    public partial class W_CreateCustomer : Window
    {
        private readonly ICustomerRepository _customerRepository;
        private P_CustomerManagement customerManagementPage;

        public W_CreateCustomer(P_CustomerManagement customerManagementPage)
        {
            InitializeComponent();
            this.customerManagementPage = customerManagementPage;
            _customerRepository = DIService.Instance.ServiceProvider.GetService<ICustomerRepository>();
        }

        public void btn_cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void btn_submit(object sender, RoutedEventArgs e)
        {
            if (!IsAllTextboxEntered())
            {
                MessageBox.Show("Please input all fields!");
                return;
            }

            string fullName = tb_FullName.Text.Trim();
            string telephone = tb_Telephone.Text.Trim();
            string email = tb_EmailAddress.Text.Trim();

            if (!IsValidEmail(email))
            {
                MessageBox.Show("Invalid email format!", "Warning");
                return;
            }

            string password = pbPassword.Password;
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Password is empty, please enter!", "Warning");
                return;
            }


            var customerFindByEmail = _customerRepository.FindByEmail(email);
            if (customerFindByEmail != null)
            {
                MessageBox.Show(email + " already taken, choose another!");
                return;
            }

            DateTime? birthday = dp_Birthday.SelectedDate;

            if (fullName.Length < 3 || fullName.Length > 50)
            {
                MessageBox.Show("Full Name must be between 3 and 50 characters.");
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(telephone, @"^\d{10,15}$"))
            {
                MessageBox.Show("Telephone must be a number between 10 and 15 digits.");
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Invalid email address.");
                return;
            }

            if (!birthday.HasValue)
            {
                MessageBox.Show("Please select a valid birthday.");
                return;
            }

            int age = CalculateAge(birthday.Value);
            if (age < 18)
            {
                MessageBox.Show("Customer must be at least 18 years old.");
                return;
            }

            Customer customer = new Customer
            {
                CustomerFullName = fullName,
                Telephone = telephone,
                EmailAddress = email,
                Password = password,
                CustomerStatus = CustomerStatus.Active,
                CustomerBirthday = DateOnly.FromDateTime(birthday.Value)
            };
            

            _customerRepository.Add(customer);

            MessageBox.Show("Create new Customer successfully!");
            customerManagementPage.UpdateDataGrid();
            this.Close();
        }

        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        private bool IsAllTextboxEntered()
        {
            if (string.IsNullOrEmpty(tb_FullName.Text)) return false;
            if (string.IsNullOrEmpty(tb_Telephone.Text)) return false;
            if (string.IsNullOrEmpty(tb_EmailAddress.Text)) return false;
            if (string.IsNullOrEmpty(dp_Birthday.Text)) return false;
            return true;
        }

        private int CalculateAge(DateTime birthday)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthday.Year;

            if (birthday.Date > today.AddYears(-age)) age--;

            return age;
        }
    }
}
