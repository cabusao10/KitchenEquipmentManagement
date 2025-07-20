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
    public class DeleteEquipmentCommand:IRequest<BaseResult>
    {
        public int EquipId { get; set; }

        public class Handler : BaseService, IRequestHandler<DeleteEquipmentCommand, BaseResult>
        {
            public Handler(IMapper mapper, IAppDbContext appdbcontext):base(mapper,appdbcontext)
            {
                
            }

            public async Task<BaseResult> Handle(DeleteEquipmentCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var equipment = ApplicationDbContext.Equipments.Where(x=> x.EquipmentId == request.EquipId).FirstOrDefault();
                    if (equipment == null) {

                        return new BaseResult { Message = "Equipment not found" };
                    }
                    ApplicationDbContext.Equipments.Remove(equipment);
                    await ApplicationDbContext.SaveChangesAsync(cancellationToken);

                    return new BaseResult { Message ="Success removing equipment",IsSuccess = true };
                }
                catch (Exception)
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
