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
    public class GetCourseQuery : IRequest<BaseResponse<IEnumerable<CourseDTO>>>
    {
        public int PageNumber;
        public int PageSize;
        public class GetCourseQueryHandler : IRequestHandler<GetCourseQuery, BaseResponse<IEnumerable<CourseDTO>>>
        {
            private readonly ApplicationContext _context;

            public GetCourseQueryHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<BaseResponse<IEnumerable<CourseDTO>>> Handle(GetCourseQuery request, CancellationToken cancellationToken = new CancellationToken())
            {
                var result = await _context.Course
                .Include(c => c.DanceClasses)
                 .Select(c => new CourseDTO
                 {
                     ID = c.ID,
                     Name = c.Name,
                     Price = c.Price,
                     BookingLimit = c.BookingLimit,
                     ClassCount = c.DanceClasses.Count,
                     InstructorID = c.InstructorID
                 })
                .OrderBy(c => c.Name)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

                return new BaseResponse<IEnumerable<CourseDTO>>((IEnumerable<CourseDTO>)result);
            }
        }
    }
}