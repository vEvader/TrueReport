using System;
using BusinessServiceInterface;
using Common.Responces;
using Container;

namespace TrueReport.Helpers
{
    public static class ServiceProxy
    {
        public static TResult Call<TResult>(Func<ITrService, TResult> call) where TResult : BaseResponse
        {
            TResult result;
            var client = TrContainer.Instance.Resolve<ITrService>();
            result = call(client);
            return result;
        }
    }
}

