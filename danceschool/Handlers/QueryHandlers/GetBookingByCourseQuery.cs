using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using danceschool.Context;
using System.Linq;
using danceschool.Api;
using danceschool.Models;
using System.Collections.Generic;

namespace danceschool.Handlers.QueryHandlers
{
    public class GetBookingByCourseQuery : IRequest<BaseResponse<IEnumerable<Booking>>>
    {
        public string Query { get; set; }
        public class GetBookingByCourseQueryHandler : IRequestHandler<GetBookingByCourseQuery, BaseResponse<IEnumerable<Booking>>>
        {
            private readonly ApplicationContext _context;


            public GetBookingByCourseQueryHandler(ApplicationContext context)
            {
                _context = context;

            }

            public async Task<BaseResponse<IEnumerable<Booking>>> Handle(GetBookingByCourseQuery request, CancellationToken cancellationToken)
            {
                var coursesFound = await _context.Course
                .Where(c => c.Name.ToLower().Contains(request.Query.ToLower()))
                .Select(c => c.ID)
                .ToListAsync();

                if (coursesFound == null)
                {
                    var result = new List<Booking>();
                    return new BaseResponse<IEnumerable<Booking>>(result);
                }

                var booking = await _context.Booking
                .Include(m => m.DanceClass)
                .Where(m => coursesFound.Contains(m.DanceClass.CourseID))
                .OrderByDescending(b => b.DanceClass.StartTime)
                .ToListAsync();

                return new BaseResponse<IEnumerable<Booking>>(booking);
            }
        }
    }
}
