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
using KitchenEquipmentManagement.WPF.ViewModels;

namespace KitchenEquipmentManagement.WPF.Views
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        private readonly RegisterViewModel _registermodel;
        public RegisterPage(RegisterViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            _registermodel = viewModel;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _registermodel.Password = txtPassword.Password;
        }
    }
}
