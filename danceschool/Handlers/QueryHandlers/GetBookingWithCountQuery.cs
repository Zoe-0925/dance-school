using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using danceschool.Models;
using danceschool.Context;
using System.Linq;
using danceschool.Api;

namespace danceschool.Handlers.QueryHandlers
{
    public class GetBookingWithCountQuery : IRequest<BaseResponse<BookingCountDTO>>
    {
        public int PageNumber;
        public int PageSize;
        public class GetBookingWithCountQueryHandler : IRequestHandler<GetBookingWithCountQuery, BaseResponse<BookingCountDTO>>
        {
            private readonly ApplicationContext _context;

            public GetBookingWithCountQueryHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<BaseResponse<BookingCountDTO>> Handle(GetBookingWithCountQuery request, CancellationToken cancellationToken = new CancellationToken())
            {

                var count = await _context.Booking.Select(c => new
                {
                    ID = c.ID
                }).CountAsync();

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

                return new BaseResponse<BookingCountDTO>(
                    new BookingCountDTO
                    {
                        Count = count,
                        Data = bookingList
                    });
            }
        }
    }
}
