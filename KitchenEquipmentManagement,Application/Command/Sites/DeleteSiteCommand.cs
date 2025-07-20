using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using KitchenEquipmentManagement.ApplicationLayer.DTOs;
using KitchenEquipmentManagement.ApplicationLayer.Interfaces;
using MediatR;

namespace KitchenEquipmentManagement.ApplicationLayer.Command.Sites
{
    public class DeleteSiteCommand : IRequest<BaseResult>
    {
        public int SiteId { get; set; }

        public class Handler : BaseService, IRequestHandler<DeleteSiteCommand, BaseResult>
        {
            public Handler(IMapper mapper, IAppDbContext context) : base(mapper, context) { }
            public async Task<BaseResult> Handle(DeleteSiteCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var site = ApplicationDbContext.Sites.Where(x => x.SiteId == request.SiteId).FirstOrDefault();
                    if (site == null)
                    {
                        return new BaseResult
                        {
                            Message = "Site not found!"
                        };
                    }

                    var registered = ApplicationDbContext.RegisteredEquipments.Where(x => x.SiteId == site.SiteId).ToArray();
                    if (registered.Any())
                    {
                        for(int x = 0; x < registered.Length; x++)
                        {
                            registered[x].Equipment.UserId = null;
                        }
                        ApplicationDbContext.RegisteredEquipments.RemoveRange(registered);
                    }

                    ApplicationDbContext.Sites.Remove(site);
                    await ApplicationDbContext.SaveChangesAsync();

                    return new BaseResult
                    {
                        IsSuccess = true,
                        Message = "Success deleting the site"
                    };

                }
                catch (Exception)
                {

                    return new BaseResult
                    {
                        Message = "There are internal error",
                    };
                }
            }
        }
    }
}
