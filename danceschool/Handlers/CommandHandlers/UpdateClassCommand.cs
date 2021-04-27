using danceschool.Api;
using danceschool.Api.ApiErrors;
using danceschool.Context;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace danceschool.Handlers.CommandHandlers
{
    public class UpdateClassCommand : IRequest<BaseResponse<int>>
    {

        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public class UpdateClassCommandHandler : IRequestHandler<UpdateClassCommand, BaseResponse<int>>
        {
            private readonly ApplicationContext _context;
            public UpdateClassCommandHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<BaseResponse<int>> Handle(UpdateClassCommand request, CancellationToken cancellationToken)
            {
                var DanceClass = _context.DanceClass.Where(a => a.ID == request.Id).FirstOrDefault();
                if (DanceClass == null)
                {
                    return new BaseResponse<int>(new NotFoundError("DanceClass of id '" + request.Id + "' is not found."));
                }
                else
                {
                    DanceClass.StartTime = request.StartTime;
                    DanceClass.EndTime = request.EndTime < request.StartTime ? request.StartTime : request.EndTime;
                    int flag = await _context.SaveChangesAsync();
                    return new BaseResponse<int>(flag);
                }
            }
        }
    }
}

