using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using KitchenEquipmentManagement.ApplicationLayer.DTOs;
using KitchenEquipmentManagement.ApplicationLayer.Interfaces;
using KitchenEquipmentManagement.ApplicationLayer.Models;
using KitchenEquipmentManagement.Domain.Entities;
using MediatR;

namespace KitchenEquipmentManagement.ApplicationLayer.Command.Sites
{
    public class UpdateSiteCommand : IRequest<BaseResult>
    {
        public SiteModel Site { get; set; }


        public class Handler : BaseService, IRequestHandler<UpdateSiteCommand, BaseResult>
        {
            public Handler(IMapper mapper, IAppDbContext context) : base(mapper, context)
            {

            }
            public async Task<BaseResult> Handle(UpdateSiteCommand request, CancellationToken cancellationToken)
            {

                // find userdata
                if (!ApplicationDbContext.Sites.Any(x => x.SiteId == request.Site.SiteId))
                {
                    return new BaseResult
                    {
                        IsSuccess = false,
                        Message = "Site not found"
                    };
                }


                var site = ApplicationDbContext.Sites.FirstOrDefault(x => x.SiteId == request.Site.SiteId);

                site.Description = request.Site.Description;
                site.Active = request.Site.Active;
                site.DateModifed = DateTime.Now;


                await ApplicationDbContext.SaveChangesAsync();

                return new BaseResult
                {
                    IsSuccess = true,
                    Message = "Success saving user data!"
                };

            }
        }
    }
}
