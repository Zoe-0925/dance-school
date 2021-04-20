using System;
using System.Collections.Generic;
using danceschool.Api.ApiErrors;
using danceschool.Models;

namespace danceschool.Api
{
    public class BaseResponse<T>
    {
        public ApiError Error;
        public bool Success;
        public T Data;
        public bool IsCached;

        public BaseResponse(ApiError error)
        {
            this.Data = default(T);
            this.Success = false;
            this.Error = error;
            this.IsCached = false;
        }

        public BaseResponse(T data)
        {
            this.Data = data;
            this.Success = true;
            this.Error = null;
            this.IsCached = false;
        }

        public BaseResponse(T data, bool isCached)
        {
            this.Data = data;
            this.Success = true;
            this.Error = null;
            this.IsCached = isCached;
        }


        public static explicit operator BaseResponse<T>(BaseResponse<IEnumerable<Booking>> v)
        {
            throw new NotImplementedException();
        }
    }
}