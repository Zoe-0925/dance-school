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
    public class GetBookingQuery : IRequest<BaseResponse<IEnumerable<BookingDTO>>>
    {
        public int PageNumber;
        public int PageSize;
        public class GetBookingQueryHandler : IRequestHandler<GetBookingQuery, BaseResponse<IEnumerable<BookingDTO>>>
        {
            private readonly ApplicationContext _context;

            public GetBookingQueryHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<BaseResponse<IEnumerable<BookingDTO>>> Handle(GetBookingQuery request, CancellationToken cancellationToken = new CancellationToken())
            {
                var bookingList = await _context.Booking
                .OrderByDescending(b => b.BookingDate)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Include(b => b.DanceClass)
                .Include(b => b.Student)
                .Select(b => new BookingDTO
                {
                    ID = b.ID,
                    ClassID = b.ClassID,
                    BookingDate = b.BookingDate,
                    Date = b.DanceClass.StartTime,
                    StudentEmail = b.Student.Email,
                    CourseName = b.DanceClass.CourseName
                })
                .ToListAsync();

                return new BaseResponse<IEnumerable<BookingDTO>>((IEnumerable<BookingDTO>)bookingList);
            }
        }
    }
}
