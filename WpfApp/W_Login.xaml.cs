﻿using BusinessObjects;
using DataAccess.Repository;
using LuuThanhDatWPF;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;
using System.Windows;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for W_Login.xaml
    /// </summary>
    public partial class W_Login : Window
    {
        private readonly ICustomerRepository _customerRepository;

        public W_Login()
        {
            InitializeComponent();
            _customerRepository = DIService.Instance.ServiceProvider.GetService<ICustomerRepository>();
        }

        public void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            string email = tbEmail.Text;
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Email is empty, please enter!", "Warning");
                return;
            }

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

            if (_customerRepository.IsAdmin(email, password))
            {
                W_Admin adminWindow = new W_Admin();
                adminWindow.Show();
                Close();
                return;
            }

            Customer customer = _customerRepository.FindByEmailAndPassword(email, password);
            if (customer == null)
            {
                MessageBox.Show("Email or Password wrong!");
                return;
            }
            else
            {
                if(customer.CustomerStatus == CustomerStatus.Active)
                {
                    W_Customer customerWindown = new W_Customer(customer);
                    customerWindown.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Your account are disable, please contact with Hotel!");
                }
            }
        }

        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        public void btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
