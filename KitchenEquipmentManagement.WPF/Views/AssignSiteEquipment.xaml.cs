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
using KitchenEquipmentManagement.ApplicationLayer.Models;
using KitchenEquipmentManagement.WPF.ViewModels;

namespace KitchenEquipmentManagement.WPF.Views
{
    /// <summary>
    /// Interaction logic for AssignSiteEquipment.xaml
    /// </summary>
    public partial class AssignSiteEquipment : Window
    {
        public AssignSiteEquipment(AssignSiteEquipmentViewModel viewmodel)
        {
            InitializeComponent();
            DataContext = viewmodel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ((AssignSiteEquipmentViewModel)this.DataContext).Assign();
            this.DialogResult = true;
        }
    }
}
