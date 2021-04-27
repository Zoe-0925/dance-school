using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using danceschool.Context;
using danceschool.Api;
using danceschool.Models;
using System.Collections.Generic;

namespace danceschool.Handlers.QueryHandlers
{
    public class GetInstructorQuery : IRequest<BaseResponse<IEnumerable<Instructor>>>
    {
        public class GetInstructorQueryHandler : IRequestHandler<GetInstructorQuery, BaseResponse<IEnumerable<Instructor>>>
        {
            private readonly ApplicationContext _context;

            public GetInstructorQueryHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<BaseResponse<IEnumerable<Instructor>>> Handle(GetInstructorQuery request, CancellationToken cancellationToken = new CancellationToken())
            {
                var InstructorList = await _context.Instructor.ToListAsync();
                return new BaseResponse<IEnumerable<Instructor>>((IEnumerable<Instructor>)InstructorList);
            }
        }
    }
}
