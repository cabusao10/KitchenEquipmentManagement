using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KitchenEquipmentManagement.ApplicationLayer.DTOs;
using KitchenEquipmentManagement.ApplicationLayer.Interfaces;
using KitchenEquipmentManagement.ApplicationLayer.Queries.Login;
using MediatR;

namespace KitchenEquipmentManagement.ApplicationLayer.Command.Registration
{
    public class RegisterCommand : IRequest<RegisterResult>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public class Handler : IRequestHandler<RegisterCommand, RegisterResult>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<RegisterResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    // check if email already existing;
                    if (await _context.Users.AnyAsync(x => x.EmailAddress == request.Email))
                    {
                        return new RegisterResult
                        {
                            Success = false,
                            ErrorMessage = "Email address taken."
                        };
                    }

                    //check if username already existing
                    if (await _context.Users.AnyAsync(x => x.UserName == request.Username))
                    {
                        return new RegisterResult
                        {
                            Success = false,
                            ErrorMessage = "Username is already taken."
                        };

                    }

                    var user = new KitchenEquipmentManagement.Domain.Entities.UserEntity
                    {
                        UserName = request.Username,
                        Password = request.Password,
                        UserType = KitchenEquipmentManagement.Domain.Entities.EnumUserType.Admin,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        Status = KitchenEquipmentManagement.Domain.Entities.UserEntity.EnumUserStatus.Created,
                        EmailAddress = request.Email,

                    };

                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();

                    if (user == null)
                    {
                        return new RegisterResult
                        {
                            Success = false,
                            ErrorMessage = "Invalid username or password"
                        };
                    }

                    return new RegisterResult
                    {
                        Success = true,
                        User = user
                    };
                }
                catch (Exception ex)
                {
                    return new RegisterResult
                    {
                        Success = false,
                        ErrorMessage = "There are error creating new account"
                    };

                }
            }
        }
    }
}
