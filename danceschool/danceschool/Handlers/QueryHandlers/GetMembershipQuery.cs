using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using danceschool.Context;
using danceschool.Api;
using danceschool.Models;
using System.Collections.Generic;

namespace danceschool.Handlers.QueryHandlers
{
    public class GetMembershipQuery : IRequest<BaseResponse<IEnumerable<Membership>>>
    {
        public class GetMembershipQueryHandler : IRequestHandler<GetMembershipQuery, BaseResponse<IEnumerable<Membership>>>
        {
            private readonly ApplicationContext _context;

            public GetMembershipQueryHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<BaseResponse<IEnumerable<Membership>>> Handle(GetMembershipQuery request, CancellationToken cancellationToken)
            {
                var Membership = await _context.Membership.ToListAsync();
                return new BaseResponse<IEnumerable<Membership>>((IEnumerable<Membership>)Membership);
            }
        }
    }
}
