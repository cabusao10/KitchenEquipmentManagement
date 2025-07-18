using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KitchenEquipmentManagement.ApplicationLayer.DTOs;
using KitchenEquipmentManagement.ApplicationLayer.Interfaces;
using MediatR;

namespace KitchenEquipmentManagement.ApplicationLayer.Queries.Login
{
    public class LoginQuery : IRequest<LoginResult>
    {
        public string Username { get; set; }
        public string Password { get; set; }


        public class Handler : IRequestHandler<LoginQuery, LoginResult>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<LoginResult> Handle(LoginQuery request, CancellationToken cancellationToken)
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.UserName == request.Username && u.Password == request.Password);

                if (user == null)
                {
                    return new LoginResult
                    {
                        Success = false,
                        ErrorMessage = "Invalid username or password"
                    };
                }

                if(user.Status == Domain.Entities.UserEntity.EnumUserStatus.Created)
                {
                    return new LoginResult
                    {
                        Success = false,
                        ErrorMessage = "Your account needed to be verified"
                    };
                }
                return new LoginResult
                {
                    Success = true,
                    User = user
                };
            }
        }
    }
}
