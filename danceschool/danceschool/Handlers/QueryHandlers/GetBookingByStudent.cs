using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using danceschool.Context;
using System.Linq;
using danceschool.Api;
using danceschool.Api.ApiErrors;
using danceschool.Models;
using System.Collections.Generic;
using System;

namespace danceschool.Handlers.QueryHandlers
{
    public class GetBookingByStudentQuery : IRequest<BaseResponse<IEnumerable<Booking>>>
    {
        public int Id { get; set; }
        public int PageNumber;
        public int PageSize;

        public class GetBookingByStudentQueryHandler : IRequestHandler<GetBookingByStudentQuery, BaseResponse<IEnumerable<Booking>>>
        {
            private readonly ApplicationContext _context;


            public GetBookingByStudentQueryHandler(ApplicationContext context)
            {
                _context = context;

            }

            public async Task<BaseResponse<IEnumerable<Booking>>> Handle(GetBookingByStudentQuery request, CancellationToken cancellationToken)
            {
                var booking = await _context.Booking
                .Include(b => b.Student)
                .Where(m => m.StudentID == request.Id)
                .Include(b => b.DanceClass)
                .OrderByDescending(b => b.DanceClass.StartTime)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

                if (booking == null)
                    return new BaseResponse<IEnumerable<Booking>>(new NotFoundError("Booking of Student id '" + request.Id + "' is not found."));

                return new BaseResponse<IEnumerable<Booking>>((IEnumerable<Booking>)booking);
            }
        }
    }
}
