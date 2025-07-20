using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using KitchenEquipmentManagement.ApplicationLayer.DTOs;
using KitchenEquipmentManagement.ApplicationLayer.Interfaces;
using KitchenEquipmentManagement.ApplicationLayer.Models;
using MediatR;

namespace KitchenEquipmentManagement.ApplicationLayer.Queries.Sites
{
    public class GetSiteEquipmentQuery:IRequest<GetSiteEquipmentResult>
    {
        public int SiteId { get; set; }
        public class Handler : BaseService, IRequestHandler<GetSiteEquipmentQuery, GetSiteEquipmentResult>
        {
            public Handler(IMapper mapper,IAppDbContext dbContext):base(mapper,dbContext){}

            public async Task<GetSiteEquipmentResult> Handle(GetSiteEquipmentQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var registeredequip = ApplicationDbContext.RegisteredEquipments.Where(x=> x.SiteId ==  request.SiteId).ToList();
                    if (registeredequip.Any()) {

                        var equipments = Mapper.Map<EquipmentModel[]>(registeredequip.Where(x=> x.SiteId == request.SiteId).Select(x => x.Equipment)).ToArray();

                        return new GetSiteEquipmentResult
                        {
                            IsSuccess = true,
                            Equipments = equipments
                        };

                    }


                    return new GetSiteEquipmentResult
                    {
                        IsSuccess = true,
                        Equipments = new EquipmentModel[0]
                    };
                }
                catch (Exception ex)
                {
                    return new GetSiteEquipmentResult { Message = "There is an internal error." };
                }
            }
        }
    }
}
