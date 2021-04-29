using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using danceschool.Models;
using danceschool.Context;
using System.Linq;
using danceschool.Api;
using System.Collections.Generic;

namespace danceschool.Handlers.QueryHandlers
{
    public class GetSubscriptionQuery : IRequest<BaseResponse<IEnumerable<SubscriptionDTO>>>
    {
        public int PageNumber;
        public int PageSize;
        public class GetSubscriptionQueryHandler : IRequestHandler<GetSubscriptionQuery, BaseResponse<IEnumerable<SubscriptionDTO>>>
        {
            public int PageNumber;
            public int PageSize;
            private readonly ApplicationContext _context;

            public GetSubscriptionQueryHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<BaseResponse<IEnumerable<SubscriptionDTO>>> Handle(GetSubscriptionQuery request, CancellationToken cancellationToken)
            {
                var SubscriptionList = await _context.Subscription
                .Include(s => s.Student)
                .OrderByDescending(s => s.StartDate)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(s => new SubscriptionDTO
                {
                    ID = s.ID,
                    StartDate = s.StartDate,
                    NextBillingDate = s.NextBillingDate,
                    StudentName = s.Student.UserName,
                    MembershipName = s.MembershipName
                })
                .ToListAsync();

                return new BaseResponse<IEnumerable<SubscriptionDTO>>((IEnumerable<SubscriptionDTO>)SubscriptionList);
            }
        }
    }
}
