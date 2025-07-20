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
using KitchenEquipmentManagement.ApplicationLayer.Command.Sites;
using KitchenEquipmentManagement.ApplicationLayer.Interfaces;
using KitchenEquipmentManagement.ApplicationLayer.Models;
using KitchenEquipmentManagement.ApplicationLayer.Queries.Sites;
using KitchenEquipmentManagement.WPF.Helper;
using KitchenEquipmentManagement.WPF.Views;
using MediatR;

namespace KitchenEquipmentManagement.WPF.ViewModels
{
    public class SiteEquipmentViewModel : INotifyPropertyChanged
    {
        private readonly IMediator _mediator;
        private readonly IMainNavigateService _nav;
        private readonly AssignSiteEquipment _assigg;
        private readonly AssignSiteEquipmentViewModel _assignViewmodel;
        private readonly ICurrentUserService _currentUser;

        public ObservableCollection<EquipmentModel> Equipments { get; set; }
        private int _siteId;
        public ICommand AssignEquipmentCommand { get; }
        public ICommand BackToListCommand { get; }
        public ICommand UseEquipmentCommand { get; }
        public ICommand UnUseEquipmentCommand { get; }
        public ICommand DeleteEquipmentCommand { get; }

        public SiteEquipmentViewModel(IMediator mediator,
            IMainNavigateService mainNavigateService, ICurrentUserService currentUser,
            AssignSiteEquipment assignSite, AssignSiteEquipmentViewModel assignSiteEquipmentModel)
        {
            this._mediator = mediator;
            this._nav = mainNavigateService;

            this._assignViewmodel = assignSiteEquipmentModel;

            this._currentUser = currentUser;
            this._assigg = assignSite;

            this.BackToListCommand = new RelayCommand(async _ => await BackToList());
            this.AssignEquipmentCommand = new RelayCommand(async _ => await AssignEquipment());
            this.UseEquipmentCommand = new RelayCommand(async (equipmentid) => await UseEquipment(equipmentid));
            this.UnUseEquipmentCommand = new RelayCommand(async (equipmentid) => await UnUseEquipment(equipmentid));
            this.DeleteEquipmentCommand = new RelayCommand(async (equipmentid) => await DeleteEquipment(equipmentid));

        }

        public async Task UnUseEquipment(object equipmentid)
        {
            if (MessageBox.Show("Do you want to stop using the equipment on this site?", "Stop using equipment", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var res = await _mediator.Send(new UnuseEquipmentCommand
                {
                    EquipmentId = (int)equipmentid,
                    SiteId = _siteId,
                });


                if (res.IsSuccess)
                {
                    await GetSiteEquipments(_siteId);

                    MessageBox.Show("Success", "Stop using equipment", MessageBoxButton.OK, MessageBoxImage.Information);

                }


            }
        }
        public async Task DeleteEquipment(object equipmentid)
        {
            if(MessageBox.Show("Do want to remove the equipment on this site?","Removing equipment", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var res = await _mediator.Send(new RemoveEquipmentCommand
                {
                    EquipId = (int)equipmentid,
                    SiteId = _siteId,
                });


                if (res.IsSuccess)
                {
                    await GetSiteEquipments(_siteId);

                    MessageBox.Show("Success removing the equipment", "Removing equipment", MessageBoxButton.OK, MessageBoxImage.Information);

                }


            }
        }
        public async Task UseEquipment(object equipmentid)
        {

            if(MessageBox.Show("Do you want to use this equipment?\n" +
                "Using an equipment , the site will be assigned to you.","Use equipment", MessageBoxButton.YesNo, MessageBoxImage.Question)== MessageBoxResult.Yes)
            {
              var res =  await _mediator.Send(new EquipmentAssignmentCommand
                {
                    UserId = this._currentUser.GetCurrentUser().UserId,
                    EquipmentId = (int)equipmentid,
                    SiteId = this._siteId,
                });

                if (res.IsSuccess)
                {
                    var equipment = this.Equipments.Where(x => x.EquipmentId == (int)(equipmentid)).FirstOrDefault();
                    await GetSiteEquipments(_siteId);

                    MessageBox.Show("Success using the equipment", "Use equipment", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }

        }
        public async Task BackToList()
        {
            _nav.NavigateTo<SitePage>();
        }

        public async Task AssignEquipment()
        {
            var assignwindow = new AssignSiteEquipment(_assignViewmodel);

            await _assignViewmodel.GetAvailbleEquips(_siteId);

            if (assignwindow.ShowDialog() == true)
            {
                GetSiteEquipments(_siteId).Wait();
            }
        }
        public async Task GetSiteEquipments(int siteid)
        {
            _siteId = siteid;
            var res = await _mediator.Send(new GetSiteEquipmentQuery
            {
                SiteId = siteid
            });

            if (res.IsSuccess)
            {

                Equipments = new ObservableCollection<EquipmentModel>(res.Equipments);
                OnPropertyChanged(nameof(Equipments));

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
