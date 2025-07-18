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

namespace KitchenEquipmentManagement.ApplicationLayer.Queries.Login
{
    public class GetSitesQuery : IRequest<GetSitesResult>
    {
        public class Handler : BaseService, IRequestHandler<GetSitesQuery, GetSitesResult>
        {

            public Handler(IAppDbContext context, IMapper mapper) : base(mapper, context)
            {
            }

            public async Task<GetSitesResult> Handle(GetSitesQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var sites = await ApplicationDbContext.Sites.ToArrayAsync();

                    var sitesmodel = Mapper.Map<SiteModel[]>(sites);

                    return new GetSitesResult
                    {
                        IsSuccess = true,
                        Sites = sitesmodel,
                    };

                }
                catch (Exception ex)
                {
                    return null;

                }


            }
        }
    }
}
