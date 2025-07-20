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
    public class EquipmentAssignmentCommand : IRequest<BaseResult>
    {
        public int UserId { get; set; }
        public int EquipmentId { get; set; }
        public int SiteId { get;set; }

        public class Handler : BaseService, IRequestHandler<EquipmentAssignmentCommand, BaseResult>
        {
            public Handler(IMapper mapper, IAppDbContext appDbContext) : base(mapper, appDbContext)
            {

            }

            public async Task<BaseResult> Handle(EquipmentAssignmentCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var equipment = ApplicationDbContext.Equipments.Where(x => x.EquipmentId == request.EquipmentId).FirstOrDefault();
                    if (equipment == null)
                    {

                        return new BaseResult
                        {
                            Message = "Equipment not found"
                        };
                    }

                    var site = ApplicationDbContext.Sites.Where(x => x.SiteId == request.SiteId).FirstOrDefault();
                    if(site == null)
                    {
                        return new BaseResult
                        {
                            Message = "Site not found"
                        };
                    }

                    var user = ApplicationDbContext.Users.Where(x => x.UserId == request.UserId).FirstOrDefault();

                    if (user == null)
                    {
                        return new BaseResult
                        {
                            Message = "User not found"
                        };
                    }

                   

                    equipment.User = user;
                    equipment.UserId = request.UserId;

                    site.UserId = request.UserId;
                    site.User = user;
                    
                    await ApplicationDbContext.SaveChangesAsync(cancellationToken);

                    return new BaseResult
                    {
                        IsSuccess = true,
                        Message = "Success assigning equipment"
                    };
                }
                catch (Exception)
                {

                    return new BaseResult
                    {
                        Message = "Failed to assign equipment"
                    };
                }
            }
        }
    }
}
