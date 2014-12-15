using System;
using Common.Exceptions;
using Common.Responces;

namespace BusinessService.ExceptionWrapper
{
    public static class ExceptionWrapper
    {
        public static TValue Execute<TValue>(Func<TValue> action) where TValue : BaseResponse
        {
            TValue result;
            try
            {
                result = action();
                result.ResponseStatus = ResponseStatus.Ok;
            }
            catch (TemplateNotFoundException e)
            {
                result = Activator.CreateInstance<TValue>();
                result.ResponseStatus = ResponseStatus.TemplateNotFound;
                result = GetResult(e, result);
            }
            catch (Exception e)
            {
                result = Activator.CreateInstance<TValue>();
                result.ResponseStatus = ResponseStatus.Exception;
                result = GetResult(e, result);
            }
            return result;
        }

        private static TValue GetResult<TValue>(Exception e, TValue result) where TValue : BaseResponse
        {
            string message = e.Message;
            if (e.InnerException != null)
            {
                GetInnerMessages(ref message, e.InnerException);
            }
            result.ErrorMessage = message;
            return result;
        }

        public static void GetInnerMessages(ref string message, Exception e)
        {
            if (e != null)
            {
                message += "\n" + e.Message;
                if (e.InnerException != null)
                {
                    GetInnerMessages(ref message, e.InnerException);
                }
            }
        }
    }
}
