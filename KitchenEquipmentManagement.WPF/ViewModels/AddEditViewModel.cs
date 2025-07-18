using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using KitchenEquipmentManagement.ApplicationLayer.Command.Sites;
using KitchenEquipmentManagement.ApplicationLayer.Command.Users;
using KitchenEquipmentManagement.WPF.Helper;
using KitchenEquipmentManagement.WPF.Views;
using MediatR;

namespace KitchenEquipmentManagement.WPF.ViewModels
{
    public class AddEditViewModel : INotifyPropertyChanged
    {
        private readonly IMediator _mediator;
        private readonly INavigateService _nav;

        private string _desccription;
        private bool _status;
        private int _siteid;


        /// <summary>
        /// Id of site
        /// </summary>
        public int SiteId
        {
            get { return _siteid; }
            set { _siteid = value; OnPropertyChanged(); }
        }
        /// <summary>
        /// Descriptions of site
        /// </summary>
        public string Descriptions
        {
            get { return _desccription; }
            set { _desccription = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Status of site
        /// </summary>
        public bool Status
        {
            get { return _status; }
            set { _status = value; OnPropertyChanged(); }

        }

        /// <summary>
        /// constructir
        /// </summary>
        /// <param name="mediator">Mediator Service</param>
        /// <param name="navigator">Navigator service</param>
        public AddEditViewModel(IMediator mediator, INavigateService navigator)
        {
            _mediator = mediator;
            _nav = navigator;
            BackToListCommand = new RelayCommand(async (_) => await BackToList());
            SaveSitesCommand = new RelayCommand(async _ => await SaveSite());
        }


        public ICommand BackToListCommand { get; }
        public ICommand SaveSitesCommand { get; }


        public async Task BackToList()
        {
            _nav.NavigateTo<SitePage>();
        }

        public async Task SaveSite()
        {
            // check if new id 
            if(_siteid > 0)
            {

                var res = await _mediator.Send(new AddNewSiteCommand
                {
                    Site = new ApplicationLayer.Models.SiteModel
                    {
                        Description = _desccription,
                        Active = _status,
                    }
                });

                if (res.IsSuccess)
                {
                    MessageBox.Show("Success savingn new site");
                }
                else
                {
                    MessageBox.Show(res.Message, "Failed to add new sites");
                }
            }
            else
            {

                var res = await _mediator.Send(new UpdateSiteCommand
                {
                    Site = new ApplicationLayer.Models.SiteModel
                    {
                        Description = _desccription,
                        Active = _status,
                    }
                });

                if (res.IsSuccess)
                {
                    MessageBox.Show("Success savingn new site");
                }
                else
                {
                    MessageBox.Show(res.Message, "Failed to add new sites");
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
