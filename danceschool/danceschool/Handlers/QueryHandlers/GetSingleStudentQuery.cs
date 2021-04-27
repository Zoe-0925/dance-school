using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using danceschool.Context;
using System.Linq;
using danceschool.Api;
using danceschool.Api.ApiErrors;
using danceschool.Models;

namespace danceschool.Handlers.QueryHandlers
{
    public class GetSingleStudentQuery : IRequest<BaseResponse<StudentDTO>>
    {
        public string Email { get; set; }

        public class GetSingleStudentQueryHandler : IRequestHandler<GetSingleStudentQuery, BaseResponse<StudentDTO>>
        {
            private readonly ApplicationContext _context;


            public GetSingleStudentQueryHandler(ApplicationContext context)
            {
                _context = context;

            }

            public async Task<BaseResponse<StudentDTO>> Handle(GetSingleStudentQuery request, CancellationToken cancellationToken)
            {
                var student = await _context.Student
                .Where(m => m.Email == request.Email)
                .Select(s => new StudentDTO
                {
                    ID = s.ID,
                    UserName = s.UserName,
                    Email = s.Email,
                    Membership = ""
                })
                .FirstOrDefaultAsync();
                if (student == null)
                    return new BaseResponse<StudentDTO>(new NotFoundError("This account is not found."));

                return new BaseResponse<StudentDTO>(student);
            }
        }
    }
}
