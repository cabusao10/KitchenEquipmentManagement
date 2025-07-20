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

namespace KitchenEquipmentManagement.ApplicationLayer.Command.Equipment
{
    public class UnuseEquipmentCommand : IRequest<BaseResult>
    {
        public int SiteId { get; set; }
        public int EquipmentId { get; set; }

        public class Handler : BaseService, IRequestHandler<UnuseEquipmentCommand, BaseResult>
        {

            public Handler(IMapper mapper, IAppDbContext appDbContext) : base(mapper, appDbContext) { }

            public async Task<BaseResult> Handle(UnuseEquipmentCommand request, CancellationToken cancellationToken)
            {
                try
                {

                    // Find site
                    var site = ApplicationDbContext.Sites.Where(x => x.SiteId == request.SiteId).FirstOrDefault();
                    if (site == null)
                    {

                        return new BaseResult
                        {
                            Message = "Site Not found!"
                        };
                    }

                    // find equipment
                    var equipment = ApplicationDbContext.Equipments.Where(x => x.EquipmentId == request.EquipmentId).FirstOrDefault();
                    if (equipment == null)
                    {

                        return new BaseResult
                        {
                            Message = "Equipment Not found!"
                        };
                    }

                    equipment.UserId = null;

                    var registered = ApplicationDbContext.RegisteredEquipments.Where(x => x.SiteId == request.SiteId).ToArray();
                    if (registered.Length == 0)
                    {



                    }

                    // check if site is still used by someone
                    if (registered.Any(x => x.Equipment.UserId.HasValue))
                    {
                        // some still using it
                    }
                    else
                    {
                        site.UserId = null;
                    }

                    await ApplicationDbContext.SaveChangesAsync();


                    return new BaseResult
                    {
                        IsSuccess = true,
                        Message = "Success removing "
                    };

                }
                catch (Exception ex)
                {
                    return new BaseResult
                    {
                        Message = "There is an internal error",
                    };

                }
            }
        }
    }
}
