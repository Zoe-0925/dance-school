using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using danceschool.Context;
using System.Linq;
using danceschool.Api;
using danceschool.Models;

namespace danceschool.Handlers.QueryHandlers
{
    public class GetBookingCountByDateNumberQuery : IRequest<BaseResponse<IEnumerable<CountByDateNumber>>>
    {
        public class GetBookingCountByDateNumberQueryHandler : IRequestHandler<GetBookingCountByDateNumberQuery, BaseResponse<IEnumerable<CountByDateNumber>>>
        {
            private readonly ApplicationContext _context;

            public GetBookingCountByDateNumberQueryHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<BaseResponse<IEnumerable<CountByDateNumber>>> Handle(GetBookingCountByDateNumberQuery request, CancellationToken cancellationToken = new CancellationToken())
            {
                var result = await _context.Booking
                               .Include(b => b.DanceClass)
                               .Select(b => new
                               {
                                   ID = b.ID,
                                   Date = b.DanceClass.StartTime
                               })
                               .Where(x => x.Date >= DateTime.Today.AddMonths(-1))
                   .GroupBy(x => x.Date.Day)
                      .Select(g => new CountByDateNumber
                      {
                          Date = g.Key,
                          Count = g.Count()
                      })
                    .OrderBy(g => g.Date)
                      .ToListAsync();

                return new BaseResponse<IEnumerable<CountByDateNumber>>((IEnumerable<CountByDateNumber>)result);
            }
        }
    }
}
