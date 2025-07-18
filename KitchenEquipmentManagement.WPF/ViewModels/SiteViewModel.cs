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
        public ObservableCollection<SiteModel> Sites { get; set; }

        public string Descri
        public ICommand AddNewSiteCommand { get; }

        public SiteViewModel(IMediator mediator, IMainNavigateService navigator, ICurrentUserService currentUserService)
        {
            _currentUser = currentUserService;
            _mediator = mediator;

            _nav = navigator;

            AddNewSiteCommand = new RelayCommand(async _ => await AddNewSite());
        }
        public async Task GetSites()
        {
            var res = await _mediator.Send(new GetSitesQuery());
            
            if (res.IsSuccess)
            {
                Sites = new ObservableCollection<SiteModel>(res.Sites);
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
