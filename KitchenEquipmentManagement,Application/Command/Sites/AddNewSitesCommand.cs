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
using KitchenEquipmentManagement.Domain.Entities;
using MediatR;

namespace KitchenEquipmentManagement.ApplicationLayer.Command.Sites
{
    public class AddNewSiteCommand : IRequest<BaseResult>
    {
        public SiteModel Site { get; set; }


        public class Handler : BaseService, IRequestHandler<AddNewSiteCommand, BaseResult>
        {
            public Handler(IMapper mapper, IAppDbContext context) : base(mapper, context)
            {

            }
            public async Task<BaseResult> Handle(AddNewSiteCommand request, CancellationToken cancellationToken)
            {

                try
                {

                    if (ApplicationDbContext.Sites.Any(x => x.Description == request.Site.Description))
                    {
                        return new BaseResult
                        {
                            IsSuccess = false,
                            Message = "There is an existing site with that descriptions"
                        };
                    }


                    var newsite = Mapper.Map<SiteEntity>(request.Site);
                    newsite.DateCreated = DateTime.Now;
                    newsite.UserId = null;

                    ApplicationDbContext.Sites.Add(newsite);


                    await ApplicationDbContext.SaveChangesAsync();

                    return new BaseResult
                    {
                        IsSuccess = true,
                        Message = "Success saving user data!"
                    };
                }
                catch (Exception ex)
                {

                    return new BaseResult
                    {
                        IsSuccess = false,
                        Message = "There are internal error"
                    };
                }

            }
        }
    }
}
