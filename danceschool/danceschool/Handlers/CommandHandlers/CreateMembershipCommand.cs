﻿using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using danceschool.Models;
using danceschool.Context;

namespace danceschool.Handlers.CommandHandlers
{
    public class CreateMembershipCommand : IRequest<int>
    {

        public string Name { get; set; }
        public string Duration { get; set; }
        public decimal Price { get; set; }

        public class CreateMembershipCommandHandler : IRequestHandler<CreateMembershipCommand, int>
        {
            private readonly ApplicationContext _context;

            public CreateMembershipCommandHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(CreateMembershipCommand request, CancellationToken cancellationToken)
            {
                var membership = new Membership();
                membership.Name = request.Name;
                membership.Duration = request.Duration;
                membership.Price = request.Price;

                _context.Membership.Add(membership);
                int flag = await _context.SaveChangesAsync();
                int id = membership.ID;
                return id;
            }
        }
    }

}
