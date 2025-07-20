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
    public class GetAvailableEquipmentQuery : IRequest<GetAvailableEquipmentResult>
    {

        public int Page { get; set; }
        public int Count { get; set; } = 100;
        public class Handler : BaseService, IRequestHandler<GetAvailableEquipmentQuery, GetAvailableEquipmentResult>
        {

            public Handler(IMapper mapper, IAppDbContext context) : base(mapper, context)
            {

            }
            public async Task<GetAvailableEquipmentResult> Handle(GetAvailableEquipmentQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var registeredEquipmentIds = await ApplicationDbContext.RegisteredEquipments
                                          .Select(r => r.EquipmentId)
                                          .ToListAsync();

                    var equipments = await ApplicationDbContext.Equipments
                        .Where(x=> x.Condition == "Working")
                        .Where(e => !registeredEquipmentIds.Contains(e.EquipmentId))
                        .ToArrayAsync();

                    var equipmodel = Mapper.Map<AssignSiteEquipmentModel[]>(equipments);

                    return new GetAvailableEquipmentResult
                    {
                        IsSuccess = true,
                        Equipments = equipmodel,
                    };

                }
                catch (Exception ex)
                {
                    return new GetAvailableEquipmentResult
                    {
                        IsSuccess = false,
                        Message = "There is internal error"
                    };

                }


            }
        }
    }
}
