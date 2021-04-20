using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using danceschool.Models;
using danceschool.Context;
using System.Linq;
using danceschool.Api;
using System;

namespace danceschool.Handlers.QueryHandlers
{
    public class GetCourseWithCountQuery : IRequest<BaseResponse<CourseWithCountDTO>>
    {
        public int PageNumber;
        public int PageSize;
        public class GetCourseWithCountQueryHandler : IRequestHandler<GetCourseWithCountQuery, BaseResponse<CourseWithCountDTO>>
        {
            private readonly ApplicationContext _context;

            public GetCourseWithCountQueryHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<BaseResponse<CourseWithCountDTO>> Handle(GetCourseWithCountQuery request, CancellationToken cancellationToken = new CancellationToken())
            {

                var count = await _context.Course.Select(c => new
                {
                    ID = c.ID
                }).CountAsync();

                var course = await _context.Course
                .Include(c => c.DanceClasses)
                 .Select(c => new CourseDTO
                 {
                     ID = c.ID,
                     Name = c.Name,
                     Price = c.Price,
                     BookingLimit = c.BookingLimit,
                     ClassCount = c.DanceClasses.Count
                 })
                .OrderBy(c => c.Name)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

                return new BaseResponse<CourseWithCountDTO>(new CourseWithCountDTO
                {
                    Data = course,
                    Count = count
                });
            }
        }
    }
}

