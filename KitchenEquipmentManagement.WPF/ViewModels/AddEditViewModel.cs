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
using KitchenEquipmentManagement.ApplicationLayer.Models;
using KitchenEquipmentManagement.WPF.Helper;
using KitchenEquipmentManagement.WPF.Views;
using MediatR;

namespace KitchenEquipmentManagement.WPF.ViewModels
{
    public class AddEditViewModel : INotifyPropertyChanged
    {
        private readonly IMediator _mediator;
        private readonly IMainNavigateService _nav;

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
        private bool Active
        {
            get { return _selectedstatus == 0; }

        }

        private int _selectedstatus = 0;
        public int SelectedStatus
        {
            get { return _selectedstatus; }
            set { _selectedstatus = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// constructir
        /// </summary>
        /// <param name="mediator">Mediator Service</param>
        /// <param name="navigator">Navigator service</param>
        public AddEditViewModel(IMediator mediator, IMainNavigateService navigator)
        {
            _mediator = mediator;
            _nav = navigator;
            

            BackToListCommand = new RelayCommand(async (_) => await BackToList());
            SaveSitesCommand = new RelayCommand(async _ => await SaveSite());
        }

        public string TitleText => SiteId > 0 ? "Update site" : "Add new site";


        public ICommand BackToListCommand { get; }
        public ICommand SaveSitesCommand { get; }

        public void ViewData(SiteModel data)
        {
            SiteId = data.SiteId;
            SelectedStatus = data.Active ?  0:1;
            Descriptions = data.Description;
        }

        public async Task BackToList()
        {

            this.Descriptions = string.Empty;
            this.SelectedStatus = 0;
            this.SiteId = 0;
            _nav.NavigateTo<SitePage>();
        }

   

        public async Task SaveSite()
        {
            // check if new id 
            if(_siteid == 0)
            {

                var res = await _mediator.Send(new AddNewSiteCommand
                {
                    Site = new ApplicationLayer.Models.SiteModel
                    {
                        Description = _desccription,
                        Active = Active,
                    }
                });

                this.Descriptions = string.Empty;
                this.SelectedStatus = 0;
                this.SiteId = 0;

                if (res.IsSuccess)
                {
                    MessageBox.Show("Success saving new site");
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
                        SiteId = SiteId,
                        Description = _desccription,
                        Active = Active,
                    }
                });


              

                if (res.IsSuccess)
                {
                    MessageBox.Show("Success updating site data");
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
