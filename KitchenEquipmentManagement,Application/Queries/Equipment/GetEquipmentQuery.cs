using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    public class GetEquipmentQuery : IRequest<GetEquipmentResult>
    {

        public int Page { get; set; }
        public int Count { get; set; } = 100;
        public class Handler : BaseService, IRequestHandler<GetEquipmentQuery, GetEquipmentResult>
        {

            public Handler(IMapper mapper, IAppDbContext context) : base(mapper, context)
            {

            }
            public async Task<GetEquipmentResult> Handle(GetEquipmentQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = from e in ApplicationDbContext.Equipments
                                join r in ApplicationDbContext.RegisteredEquipments
                                    on e.EquipmentId equals r.EquipmentId into regEquipGroup
                                from r in regEquipGroup.DefaultIfEmpty() // LEFT JOIN
                                join s in ApplicationDbContext.Sites
                                    on r.SiteId equals s.SiteId into siteGroup
                                from s in siteGroup.DefaultIfEmpty()
                                select new EquipmentModel
                                {
                                    EquipmentId = e.EquipmentId,
                                    Condition = e.Condition,
                                    Description = e.Description,
                                    SerialNumber = e.SerialNumber,
                                    User = new UserModel
                                    {
                                         FirstName = e.User.FirstName
                                    },
                                    UserId = e.UserId,

                                    SiteId = s.SiteId,
                                    SiteDescription = s.Description
                                };

                    var equipmodel = query.ToArray();

                    return new GetEquipmentResult
                    {
                        IsSuccess = true,
                        Equipments = equipmodel,
                    };

                }
                catch (Exception ex)
                {
                    return new GetEquipmentResult
                    {
                        IsSuccess = false,
                        Message = "There is internal error"
                    };

                }


            }
        }
    }
}
