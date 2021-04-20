using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using danceschool.Models;
using danceschool.Context;
using System.Linq;
using danceschool.Api;


namespace danceschool.Handlers.QueryHandlers
{
    public class GetSubscriptionHistoryQuery : IRequest<BaseResponse<IEnumerable<Subscription>>>
    {
        public int Id { get; set; }
        public int PageNumber;
        public int PageSize;

        public class GetSubscriptionHistoryQueryHandler : IRequestHandler<GetSubscriptionHistoryQuery, BaseResponse<IEnumerable<Subscription>>>
        {
            private readonly ApplicationContext _context;

            public GetSubscriptionHistoryQueryHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<BaseResponse<IEnumerable<Subscription>>> Handle(GetSubscriptionHistoryQuery request, CancellationToken cancellationToken)
            {
                var SubscriptionList = await _context.Subscription
                .Where(s => s.StudentID == request.Id)
                .OrderByDescending(s => s.StartDate)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

                return new BaseResponse<IEnumerable<Subscription>>(SubscriptionList);
            }
        }
    }
}
