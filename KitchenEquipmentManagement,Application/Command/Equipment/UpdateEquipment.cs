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
using MediatR;

namespace KitchenEquipmentManagement.ApplicationLayer.Command.Equipment
{
    public class UpdateEquipmentCommand : IRequest<BaseResult>
    {
        public EquipmentModel Equipment { get; set; }

        public class Handler : BaseService, IRequestHandler<UpdateEquipmentCommand, BaseResult>
        {
            public Handler(IMapper mapper, IAppDbContext appDbContext) : base(mapper, appDbContext)
            {

            }

            public async Task<BaseResult> Handle(UpdateEquipmentCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    // find equip
                    var equip = await ApplicationDbContext.Equipments.FirstOrDefaultAsync(x => x.EquipmentId == request.Equipment.EquipmentId, cancellationToken);
                    if (equip == null)
                    {

                        return new BaseResult
                        {
                            Message = "Equipment not found."
                        };
                    }

                    equip.SerialNumber = request.Equipment.SerialNumber;
                    equip.Description = request.Equipment.Description;
                    equip.Condition = request.Equipment.Condition;
                    equip.DateModifed = DateTime.Now;

                    await ApplicationDbContext.SaveChangesAsync(cancellationToken);


                    return new BaseResult
                    {
                        IsSuccess = true,
                        Message = "Success saving result",
                    };
                }
                catch (Exception ex)
                {
                    return new BaseResult
                    {
                        Message = "There is internal error"
                    };

                }
            }
        }
    }
}
