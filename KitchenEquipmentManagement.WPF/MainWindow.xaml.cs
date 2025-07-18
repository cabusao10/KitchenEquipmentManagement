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
using System.Windows.Shapes;
using KitchenEquipmentManagement.WPF.Helper;
using KitchenEquipmentManagement.WPF.Views;

namespace KitchenEquipmentManagement.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(INavigateService navigate, IMainNavigateService mainnavigate, MainPage mainpage)
        {
            InitializeComponent();

            mainnavigate.SetFrame(mainpage.MainFrame);

            navigate.SetFrame(this.MainFrame);
            navigate.NavigateTo<LoginPage>();
        }
    }
}
