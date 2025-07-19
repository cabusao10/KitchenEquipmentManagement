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
using KitchenEquipmentManagement.ApplicationLayer.Command.Registration;
using KitchenEquipmentManagement.ApplicationLayer.Command.Sites;
using KitchenEquipmentManagement.ApplicationLayer.Command.Users;
using KitchenEquipmentManagement.ApplicationLayer.DTOs;
using KitchenEquipmentManagement.ApplicationLayer.Interfaces;
using KitchenEquipmentManagement.ApplicationLayer.Models;
using KitchenEquipmentManagement.ApplicationLayer.Queries.Login;
using KitchenEquipmentManagement.WPF.Helper;
using KitchenEquipmentManagement.WPF.Views;
using MediatR;
using static KitchenEquipmentManagement.Domain.Entities.UserEntity;

namespace KitchenEquipmentManagement.WPF.ViewModels
{
    public class SiteViewModel : INotifyPropertyChanged
    {

        private readonly IMainNavigateService _nav;
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUser;
        private readonly AddEditViewModel _addeditmodel;
        public ObservableCollection<SiteModel> Sites { get; set;  }

        public ICommand AddNewSiteCommand { get; }
        public ICommand DeleteSitesCommand { get; }
        public ICommand EditSiteCommand { get; }

        public SiteViewModel(IMediator mediator, AddEditViewModel viewmodel
            , IMainNavigateService navigator, ICurrentUserService currentUserService)
        {
            _currentUser = currentUserService;
            _mediator = mediator;

            _nav = navigator;
            _addeditmodel = viewmodel;

            AddNewSiteCommand = new RelayCommand(async _ => await AddNewSite());
            DeleteSitesCommand = new RelayCommand(async (siteId) => await DeleteSite(siteId));
            EditSiteCommand = new RelayCommand(async (siteId) => await EditSite(siteId));
        }

        public async Task EditSite(object siteId)
        {
            var site = this.Sites.FirstOrDefault(x => x.SiteId == (int)siteId);
            if (site != null)
            {
                _addeditmodel.ViewData(site);
                _nav.NavigateTo<AddEditSitePage>();
                   
            }
        }
        public async Task DeleteSite(object siteId)
        {


            if (MessageBox.Show("Continue deleting the site?\n All the equipment under it will be available again for other users", "Deleting site", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var site = this.Sites.Where(x => x.SiteId == (int)siteId).FirstOrDefault();

                var res = await _mediator.Send(new DeleteSiteCommand
                {
                    SiteId = (int)siteId
                });

                if (res.IsSuccess)
                {
                    this.Sites.Remove(site);
                    MessageBox.Show(res.Message, "Success deleting the site", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show(res.Message, "Failed to delete the site", MessageBoxButton.OK);

                }
            }

        }
        public async Task GetSites()
        {
            var res = await _mediator.Send(new GetSitesQuery());

            if (res.IsSuccess)
            {
                Sites = new ObservableCollection<SiteModel>(res.Sites);
                OnPropertyChanged("Sites");
            }
        }

        public async Task SaveSite()
        {

        }
        public async Task AddNewSite()
        {
            _nav.NavigateTo<AddEditSitePage>();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
