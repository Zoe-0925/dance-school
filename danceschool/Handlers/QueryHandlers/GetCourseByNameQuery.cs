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
    public class GetCourseByNameQuery : IRequest<BaseResponse<IEnumerable<CourseDTO>>>
    {
        public string Query;
        public class GetCourseByNameQueryHandler : IRequestHandler<GetCourseByNameQuery, BaseResponse<IEnumerable<CourseDTO>>>
        {
            private readonly ApplicationContext _context;

            public GetCourseByNameQueryHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<BaseResponse<IEnumerable<CourseDTO>>> Handle(GetCourseByNameQuery request, CancellationToken cancellationToken = new CancellationToken())
            {
                var result = await _context.Course
                .Where(c => c.Name.ToLower().Contains(request.Query.ToLower()))
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
                .ToListAsync();

                return new BaseResponse<IEnumerable<CourseDTO>>((IEnumerable<CourseDTO>)result);
            }
        }
    }
}