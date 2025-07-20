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
using KitchenEquipmentManagement.Domain.Entities;
using MediatR;

namespace KitchenEquipmentManagement.ApplicationLayer.Command.Sites
{
    public class AssignEquipmentCommand : IRequest<BaseResult>
    {
        public int SiteId { get; set; }
        public AssignSiteEquipmentModel[] Selected { get; set; }

        public class Handler : BaseService, IRequestHandler<AssignEquipmentCommand, BaseResult>
        {
            public Handler(IMapper mapper, IAppDbContext appDb) : base(mapper, appDb)
            {

            }

            public async Task<BaseResult> Handle(AssignEquipmentCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    var equipToRegister = request.Selected.Select(x => new RegisteredEquipmentEntity
                    {
                        DateCreated = DateTime.Now,
                        EquipmentId = x.EquipmentId,
                        SiteId = request.SiteId,
                    }).ToArray();

                    ApplicationDbContext.RegisteredEquipments.AddRange(equipToRegister);
                    await ApplicationDbContext.SaveChangesAsync();

                    return new BaseResult
                    {
                        IsSuccess = true,
                        Message = "Success assigning equipment"
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
