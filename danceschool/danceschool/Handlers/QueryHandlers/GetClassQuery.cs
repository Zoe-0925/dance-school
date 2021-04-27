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
    public class GetClassQuery : IRequest<BaseResponse<IEnumerable<DanceClass>>>
    {
        public class GetClassQueryHandler : IRequestHandler<GetClassQuery, BaseResponse<IEnumerable<DanceClass>>>
        {
            private readonly ApplicationContext _context;

            public GetClassQueryHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<BaseResponse<IEnumerable<DanceClass>>> Handle(GetClassQuery request, CancellationToken cancellationToken = new CancellationToken())
            {
                var classList = await _context.DanceClass.ToListAsync();
                return new BaseResponse<IEnumerable<DanceClass>>((IEnumerable<DanceClass>)classList);
            }
        }
    }
}
