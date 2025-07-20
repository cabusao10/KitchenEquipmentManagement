using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using KitchenEquipmentManagement.ApplicationLayer.Command.Sites;
using KitchenEquipmentManagement.ApplicationLayer.Models;
using KitchenEquipmentManagement.ApplicationLayer.Queries.Equipment;
using KitchenEquipmentManagement.WPF.Helper;
using MediatR;

namespace KitchenEquipmentManagement.WPF.ViewModels
{
    public class AssignSiteEquipmentViewModel : INotifyPropertyChanged
    {
        private readonly IMediator _mediator;
        private int _siteId;
        public ObservableCollection<AssignSiteEquipmentModel> AvailableEquips { get; set; }

        public ICommand SelectAllCommand { get; }
        public ICommand AssignCommand { get; }
        public AssignSiteEquipmentViewModel(IMediator mediator)
        {
            this._mediator = mediator;

            this.SelectAllCommand = new RelayCommand(async _ => await SelectAll());
            this.AssignCommand = new RelayCommand(async _ => await Assign());
        }

        public async Task Assign()
        {
            var selectedEquips = AvailableEquips.Where(x => x.IsSelected).ToArray();

            if(selectedEquips.Length > 0)
            {
                await _mediator.Send(new AssignEquipmentCommand
                {
                    Selected = selectedEquips,
                    SiteId = _siteId
                });
            }
        }
        public async Task SelectAll()
        {
            foreach (var item in AvailableEquips)
            {
                item.IsSelected = true;
            }
            AvailableEquips = new ObservableCollection<AssignSiteEquipmentModel>(AvailableEquips);
            OnPropertyChanged(nameof(AvailableEquips));
        }

        public async Task GetAvailbleEquips(int siteid)
        {
            _siteId = siteid;

            var res = await _mediator.Send(new GetAvailableEquipmentQuery());

            if (res.IsSuccess)
            {
                this.AvailableEquips = new ObservableCollection<AssignSiteEquipmentModel>(res.Equipments);
                OnPropertyChanged(nameof(AvailableEquips));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
