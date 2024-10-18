using BusinessObjects;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LuuThanhDatWPF
{
    /// <summary>
    /// Interaction logic for P_CustomerProfile.xaml
    /// </summary>
    public partial class P_CustomerProfile : Page
    {
        private Customer customer;

        public P_CustomerProfile(Customer customer)
        {
            InitializeComponent();
            this.customer = customer;
            UpdateVisual();
        }

        private void btn_UpdateProfile(object sender, RoutedEventArgs e)
        {
            W_UpdateProfile updateProfile = new W_UpdateProfile(customer, this);
            updateProfile.Show();
        }

        private void btn_ChangePassword(object sender, RoutedEventArgs e)
        {
            W_ChangePassword changePassword = new W_ChangePassword(customer);
            changePassword.Show();
        }

        public void UpdateVisual()
        {
            tbFullName.Text = customer.CustomerFullName;
            tbTelephone.Text = customer.Telephone;
            tbEmail.Text = customer.EmailAddress;
            tbBirthday.Text = customer.CustomerBirthday.ToString();
        }
    }
}
