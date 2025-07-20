using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using KitchenEquipmentManagement.ApplicationLayer.Command.Equipment;
using KitchenEquipmentManagement.ApplicationLayer.Models;
using KitchenEquipmentManagement.ApplicationLayer.Queries.Equipment;
using KitchenEquipmentManagement.WPF.Helper;
using KitchenEquipmentManagement.WPF.Views;
using MediatR;

namespace KitchenEquipmentManagement.WPF.ViewModels
{
    public class EquipmentViewModel : INotifyPropertyChanged
    {
        private readonly IMediator _mediator;
        private readonly IMainNavigateService _nav;
        public ICommand AddNewEquipmentCommand { get; }
        public ICommand EditEquipmentCommand { get; }
        public ICommand DeleteEquipmentCommand { get; }
        public ObservableCollection<EquipmentModel> Equipments { get; set; }
        private readonly AddEditEquipmentViewModel _addEditEquipmentViewModel;
        public EquipmentViewModel(IMediator mediator, IMainNavigateService nav, AddEditEquipmentViewModel viewmodel)
        {
            _mediator = mediator;
            _nav = nav;

            _addEditEquipmentViewModel = viewmodel;
            this.AddNewEquipmentCommand = new RelayCommand(async _ => await AddNewEquips());
            this.EditEquipmentCommand = new RelayCommand(async (id) => await EditEquipment(id));
            this.DeleteEquipmentCommand = new RelayCommand(async (id) => await DeleteEquipment(id));
        }

        public async Task DeleteEquipment(object equipmentid)
        {

            if (MessageBox.Show("Continue deleting the equipment?\n" +
                  "It will be removed on all the sites and users thaat is using it", "Equipment deleting", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var res = await _mediator.Send(new DeleteEquipmentCommand
                {
                    EquipId = (int)equipmentid
                });

                if (res.IsSuccess)
                {
                    await this.GetEquipments();
                }

                MessageBox.Show(res.Message, "Equipment deleting", MessageBoxButton.OK, res.IsSuccess ? MessageBoxImage.Information : MessageBoxImage.Error);
            }

        }
        public async Task EditEquipment(object equipmentid)
        {
            var equipid = (int)equipmentid;

            if (equipid > 0)
            {
                await _addEditEquipmentViewModel.ViewEquip(equipid);
                _nav.NavigateTo<AddEditEquipment>();
            }
        }


        private string _serialnumber;
        public string SerialNumber
        {

            get { return _serialnumber; }
            set
            {
                _serialnumber = value;
                OnPropertyChanged();
            }
        }
        private string _descrition;
        public string Description
        {
            get { return _descrition; }
            set { _descrition = value; OnPropertyChanged(); }
        }

        private string _condition;
        public string Condition
        {
            get { return _condition; }
            set { _condition = value; OnPropertyChanged(); }
        }
        public async Task AddNewEquips()
        {
            _nav.NavigateTo<AddEditEquipment>();
        }
        public async Task GetEquipments()
        {
            var res = await _mediator.Send(new GetEquipmentQuery());

            if (res.IsSuccess)
            {
                Equipments = new ObservableCollection<EquipmentModel>(res.Equipments);
                OnPropertyChanged("Equipments");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
