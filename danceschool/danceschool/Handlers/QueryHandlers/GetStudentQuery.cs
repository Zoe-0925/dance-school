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
    public class GetStudentQuery : IRequest<BaseResponse<IEnumerable<StudentDTO>>>
    {
        public int PageNumber;
        public int PageSize;
        public class GetStudentQueryHandler : IRequestHandler<GetStudentQuery, BaseResponse<IEnumerable<StudentDTO>>>
        {
            private readonly ApplicationContext _context;

            public GetStudentQueryHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<BaseResponse<IEnumerable<StudentDTO>>> Handle(GetStudentQuery request, CancellationToken cancellationToken)
            {
                var StudentList = await _context.Student
                .OrderBy(b => b.UserName)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Include(s => s.Subscription)
                .Select(s => new StudentDTO
                {
                    ID = s.ID,
                    UserName = s.UserName,
                    Email = s.Email,
                    Membership = ""
                }).ToListAsync();

                return new BaseResponse<IEnumerable<StudentDTO>>((IEnumerable<StudentDTO>)StudentList);
            }
        }
    }
}
