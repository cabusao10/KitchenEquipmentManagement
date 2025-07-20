using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using KitchenEquipmentManagement.ApplicationLayer.DTOs;
using KitchenEquipmentManagement.ApplicationLayer.Interfaces;
using KitchenEquipmentManagement.Domain.Entities;
using MediatR;

namespace KitchenEquipmentManagement.ApplicationLayer.Command.Sites
{
    public class RemoveEquipmentCommand:IRequest<BaseResult>
    {
        public int SiteId { get; set; }
        public int EquipId { get; set; }
        

        public class Handler : BaseService, IRequestHandler<RemoveEquipmentCommand, BaseResult>
        {
            public Handler(IMapper mapper, IAppDbContext app):base(mapper, app) { }

            public async Task<BaseResult> Handle(RemoveEquipmentCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    var site = ApplicationDbContext.Sites.Where(x => x.SiteId == request.SiteId).FirstOrDefault();

                    if (site == null) {

                        return new BaseResult
                        {
                            Message = "Site not found",
                        };
                    }

                    var equipment = ApplicationDbContext.Equipments.Where(x=> x.EquipmentId == request.EquipId).FirstOrDefault();

                    if (equipment == null) {
                        return new BaseResult
                        {
                            Message = "Equipment not found"
                        };
                    }
                    var registeredentity = ApplicationDbContext.RegisteredEquipments.Where(x=> x.EquipmentId == request.EquipId).FirstOrDefault();
                    ApplicationDbContext.RegisteredEquipments.Remove(registeredentity);

                    equipment.User = null;
                    equipment.UserId = null;

                    await ApplicationDbContext.SaveChangesAsync(cancellationToken);
                    return new BaseResult
                    {
                        IsSuccess = true,
                        Message = "Success removing equipment"
                    };
                }
                catch (Exception ex)
                {
                    return new BaseResult
                    {
                        Message = "There is an internal error"
                    };
                }
            }
        }
    }
}
