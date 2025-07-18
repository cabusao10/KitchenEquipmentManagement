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
using KitchenEquipmentManagement.ApplicationLayer.Interfaces;
using KitchenEquipmentManagement.WPF.ViewModels;

namespace KitchenEquipmentManagement.WPF.Views
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private readonly MainPageViewModel _viewmodel;
        private readonly ICurrentUserService _currentuser;
        public MainPage(MainPageViewModel viewmodel , ICurrentUserService currentuser)
        {
            InitializeComponent();
            DataContext = viewmodel;
            _viewmodel = viewmodel;
            _currentuser = currentuser;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _viewmodel.CanViewUsers = _currentuser.GetUserRole() == Domain.Entities.EnumUserType.SuperAdmin;
        }
    }
}
