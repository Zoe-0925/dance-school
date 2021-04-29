using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using danceschool.Models;
using danceschool.Context;
using System.Linq;
using System;
using danceschool.Api;

namespace danceschool.Handlers.QueryHandlers
{
    public class GetActiveSubscriptionQuery : IRequest<BaseResponse<IEnumerable<Subscription>>>
    {
        public int Id { get; set; }

        public class GetActiveSubscriptionQueryHandler : IRequestHandler<GetActiveSubscriptionQuery, BaseResponse<IEnumerable<Subscription>>>
        {
            private readonly ApplicationContext _context;

            public GetActiveSubscriptionQueryHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<BaseResponse<IEnumerable<Subscription>>> Handle(GetActiveSubscriptionQuery request, CancellationToken cancellationToken)
            {
                var SubscriptionList = await _context.Subscription.Where(s => s.StudentID == request.Id && s.NextBillingDate > DateTime.Now).Include(b => b.Membership).ToListAsync();
                return new BaseResponse<IEnumerable<Subscription>>((IEnumerable<Subscription>)SubscriptionList);
            }
        }
    }
}
