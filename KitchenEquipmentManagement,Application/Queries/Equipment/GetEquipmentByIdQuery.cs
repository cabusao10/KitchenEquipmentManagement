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
using KitchenEquipmentManagement.ApplicationLayer.Queries.Login;
using MediatR;

namespace KitchenEquipmentManagement.ApplicationLayer.Queries.Equipment
{
    public class GetEquipmentByIdQuery :IRequest<GetEquipmentByIdResult>
    {
        public int EquipId { get; set; }

        public class Handler : BaseService, IRequestHandler<GetEquipmentByIdQuery, GetEquipmentByIdResult>
        {

            public Handler(IMapper mapper,IAppDbContext context):base (mapper,context) 
            {
                
            }
            public async Task<GetEquipmentByIdResult> Handle(GetEquipmentByIdQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var equipments = await ApplicationDbContext.Equipments.Where(x=> x.EquipmentId == request.EquipId).FirstOrDefaultAsync();
                    if(equipments == null)
                    {
                        return new GetEquipmentByIdResult
                        {
                            Message = "Equipment not found"
                        };
                    }

                    var equipmodel = Mapper.Map<EquipmentModel>(equipments);

                    return new GetEquipmentByIdResult
                    {
                        IsSuccess = true,
                        Equipment = equipmodel,
                    };

                }
                catch (Exception ex)
                {
                    return new GetEquipmentByIdResult
                    {
                        IsSuccess = false,
                        Message= "There is internal error"
                    };

                }


            }
        }
    }
}
