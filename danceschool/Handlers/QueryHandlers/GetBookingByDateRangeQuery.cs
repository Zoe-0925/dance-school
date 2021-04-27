using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using danceschool.Context;
using System.Linq;
using danceschool.Api;
using danceschool.Models;
using System.Collections.Generic;
using System;

namespace danceschool.Handlers.QueryHandlers
{
    public class GetBookingByDateRangeQuery : IRequest<BaseResponse<IEnumerable<Booking>>>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int PageNumber;
        public int PageSize;

        public class GetBookingByDateRangeQueryHandler : IRequestHandler<GetBookingByDateRangeQuery, BaseResponse<IEnumerable<Booking>>>
        {
            private readonly ApplicationContext _context;


            public GetBookingByDateRangeQueryHandler(ApplicationContext context)
            {
                _context = context;

            }

            public async Task<BaseResponse<IEnumerable<Booking>>> Handle(GetBookingByDateRangeQuery request, CancellationToken cancellationToken)
            {
                var booking = await _context.Booking
                .Include(b => b.DanceClass)
                .Where(b => b.DanceClass.StartTime >= request.StartDate && b.DanceClass.EndTime >= request.EndDate)
                .Include(b => b.Student)
                .OrderByDescending(b => b.DanceClass.StartTime)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

                return new BaseResponse<IEnumerable<Booking>>((IEnumerable<Booking>)booking);
            }
        }
    }
}
