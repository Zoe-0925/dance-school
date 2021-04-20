using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using danceschool.Context;
using danceschool.Api;
using danceschool.Models;
using System.Collections.Generic;
using System.Linq;

namespace danceschool.Handlers.QueryHandlers
{
    public class GetInstructorByNameQuery : IRequest<BaseResponse<IEnumerable<Instructor>>>
    {
        public string Query;
        public class GetInstructorByNameQueryHandler : IRequestHandler<GetInstructorByNameQuery, BaseResponse<IEnumerable<Instructor>>>
        {
            private readonly ApplicationContext _context;

            public GetInstructorByNameQueryHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<BaseResponse<IEnumerable<Instructor>>> Handle(GetInstructorByNameQuery request, CancellationToken cancellationToken = new CancellationToken())
            {
                var InstructorList = await _context.Instructor
                .Where(i => i.FirstName.ToLower().Contains(request.Query.ToLower()) || i.LastName.ToLower().Contains(request.Query.ToLower()))
                .ToListAsync();
                return new BaseResponse<IEnumerable<Instructor>>((IEnumerable<Instructor>)InstructorList);
            }
        }
    }
}
