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

namespace KitchenEquipmentManagement.ApplicationLayer.Command.Equipment
{
    public class AddEquipementCommand : IRequest<BaseResult>
    {
        public string SerialNumber { get; set; }
        public string Description { get; set; }
        public string Condition { get; set; }


        public class Handler : BaseService, IRequestHandler<AddEquipementCommand, BaseResult>
        {
            public Handler(IMapper mapper, IAppDbContext appdb) : base(mapper, appdb)
            {

            }
            public async Task<BaseResult> Handle(AddEquipementCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    //check existing serial
                    if (ApplicationDbContext.Equipments.Any(x => x.SerialNumber == request.SerialNumber))
                    {
                        return new BaseResult
                        {
                            Message = "There is an existing equipment with that Serial Number"
                        };
                    }

                    var newequip = ApplicationDbContext.Equipments.Add(new EquipmentEntity());
                    newequip.SerialNumber = request.SerialNumber;
                    newequip.Description = request.Description;
                    newequip.Condition = request.Condition;
                    newequip.DateCreated = DateTime.Now;


                    await ApplicationDbContext.SaveChangesAsync(cancellationToken);

                    return new BaseResult
                    {
                         IsSuccess = true,
                         Message= "Succes adding new equipment"
                    };

                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }

    }
}
