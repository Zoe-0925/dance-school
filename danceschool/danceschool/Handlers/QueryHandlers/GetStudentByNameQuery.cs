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
    public class GetStudentByNameQuery : IRequest<BaseResponse<IEnumerable<StudentDTO>>>
    {
        public string Query;

        public class GetStudentByNameQueryHandler : IRequestHandler<GetStudentByNameQuery, BaseResponse<IEnumerable<StudentDTO>>>
        {
            private readonly ApplicationContext _context;

            public GetStudentByNameQueryHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<BaseResponse<IEnumerable<StudentDTO>>> Handle(GetStudentByNameQuery request, CancellationToken cancellationToken)
            {
                var StudentList = await _context.Student
                .Where(s => s.UserName.ToLower().Contains(request.Query.ToLower()))
                .OrderBy(b => b.UserName)
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
