using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using danceschool.Models;
using danceschool.Context;
using System;
using System.Linq;
using danceschool.Api;
using System.Collections.Generic;

namespace danceschool.Handlers.QueryHandlers
{
    public class GetClassesByCourseQuery : IRequest<BaseResponse<IEnumerable<DanceClassDTO>>>
    {
        public int Id { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool Upcoming { get; set; }

        public class GetClassesByCourseQueryHandler : IRequestHandler<GetClassesByCourseQuery, BaseResponse<IEnumerable<DanceClassDTO>>>
        {
            private readonly ApplicationContext _context;


            public GetClassesByCourseQueryHandler(ApplicationContext context)
            {
                _context = context;

            }

            public async Task<BaseResponse<IEnumerable<DanceClassDTO>>> Handle(GetClassesByCourseQuery request, CancellationToken cancellationToken)
            {
                if(request.Upcoming==false){
                 var classes = await _context.DanceClass
                .Where(c => c.CourseID == request.Id)
                .Include(c => c.Bookings)
                                .Select(c => new DanceClassDTO
                                {
                                    ID = c.ID,
                                    StartTime = c.StartTime,
                                    EndTime = c.EndTime,
                                    Count = c.Bookings.Count
                                })
                .OrderByDescending(c => c.StartTime)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

                return new BaseResponse<IEnumerable<DanceClassDTO>>((IEnumerable<DanceClassDTO>)classes);
                }
                else{
                      var classes = await _context.DanceClass
                .Where(c => c.CourseID == request.Id && c.StartTime> DateTime.Now)
                .Include(c => c.Bookings)
                                .Select(c => new DanceClassDTO
                                {
                                    ID = c.ID,
                                    StartTime = c.StartTime,
                                    EndTime = c.EndTime,
                                    Count = c.Bookings.Count
                                })
                .OrderByDescending(c => c.StartTime)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

                return new BaseResponse<IEnumerable<DanceClassDTO>>((IEnumerable<DanceClassDTO>)classes);
          
                }
         }
        }
    }
}
