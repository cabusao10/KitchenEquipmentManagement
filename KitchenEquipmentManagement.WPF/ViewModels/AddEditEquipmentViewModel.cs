using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using KitchenEquipmentManagement.ApplicationLayer.Command.Equipment;
using KitchenEquipmentManagement.ApplicationLayer.Queries.Equipment;
using KitchenEquipmentManagement.WPF.Helper;
using KitchenEquipmentManagement.WPF.Views;
using MediatR;

namespace KitchenEquipmentManagement.WPF.ViewModels
{
    public class AddEditEquipmentViewModel : INotifyPropertyChanged
    {
        private readonly IMediator _mediator;
        private readonly IMainNavigateService _nav;

        private string _description;
        private string _serialNumber;
        private int _condition;
        private int _equipmentId;


        public ICommand SaveCommand { get; }
        public ICommand BackToListCommand { get; }
        public AddEditEquipmentViewModel(IMediator mediator, IMainNavigateService nav)
        {
            this._mediator = mediator;
            this._nav = nav;

            this.SaveCommand = new RelayCommand(async _ => await Save());
            this.BackToListCommand = new RelayCommand(async _ => await TaskBackToList());

        }

        public async Task TaskBackToList()
        {
            _nav.NavigateTo<EquipmentsPage>();
        }
        public async Task ViewEquip(int equipmentId)
        {
            var res = await _mediator.Send(new GetEquipmentByIdQuery
            {
                EquipId = equipmentId
            });

            if (res.IsSuccess)
            {
                this.SerialNumber = res.Equipment.SerialNumber;
                this.Description = res.Equipment.Description;
                this._equipmentId = res.Equipment.EquipmentId;
                this.Condition = res.Equipment.Condition == "Working" ? 0 : 1;
            }
        }
        public async Task Save()
        {
            if (_equipmentId > 0)
            {
                var res = await _mediator.Send(new UpdateEquipmentCommand
                {
                    Equipment = new ApplicationLayer.Models.EquipmentModel
                    {
                        EquipmentId = _equipmentId,
                        SerialNumber = this.SerialNumber,
                        Description = this.Description,
                        Condition = _condition == 0 ? "Working" : "Not Working"
                    }
                });

                MessageBox.Show(res.Message, "Updating equipment", MessageBoxButton.OK, res.IsSuccess ? MessageBoxImage.Information : MessageBoxImage.Error);

            }
            else
            {
                var res = await _mediator.Send(new AddEquipementCommand()
                {
                    Condition = _condition == 0 ? "Working" : "Not Working"
               ,
                    Description = _description
               ,
                    SerialNumber = _serialNumber
                });

                if (res.IsSuccess)
                {
                    this.Description = string.Empty;
                    this.SerialNumber = string.Empty;
                    this.Condition = 0;

                }

                MessageBox.Show(res.Message, "Adding equipment", MessageBoxButton.OK, res.IsSuccess ? MessageBoxImage.Information : MessageBoxImage.Error);
            }

        }

        public int Condition
        {
            get { return _condition; }
            set { _condition = value; OnPropertyChanged(); }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(); }
        }

        public string SerialNumber
        {
            get { return _serialNumber; }
            set { _serialNumber = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
