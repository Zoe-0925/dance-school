using System.Net;

namespace danceschool.Api.ApiErrors
{
    public class BadRequestError : ApiError
    {

        public BadRequestError()
            : base(400, HttpStatusCode.BadRequest.ToString())
        {
        }


        public BadRequestError(string message)
            : base(400, HttpStatusCode.BadRequest.ToString(), message)
        {
        }
    }
}
