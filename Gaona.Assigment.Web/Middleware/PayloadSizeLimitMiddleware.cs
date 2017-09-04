using System.Linq;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace Gaona.Assigment.Web.Middleware
{
    /// <summary>
    /// IMPROVEMENT
    /// Limits the size of the request to not exceed a certain amount of bytes
    /// </summary>
    public class PayloadSizeLimitMiddleware : OwinMiddleware
    {
        /// <summary>
        /// The request limit size in bytes
        /// </summary>
        private long _limit;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next">The next component</param>
        /// <param name="limit">Max length (bytes) of the request to accept</param>
        public PayloadSizeLimitMiddleware(OwinMiddleware next, long limit) : base(next)
        {
            _limit = limit;
        }

        /// <summary>
        /// Method to be invoked on the pipeline.
        /// </summary>
        /// <param name="context">The current owin context</param>
        /// <returns></returns>
        public override Task Invoke(IOwinContext context)
        {
            IOwinRequest owinRequest = context.Request;
            IOwinResponse owinResponse = context.Response;

            if (owinRequest != null)
            {
                string[] values;
                if (owinRequest.Headers.TryGetValue("Content-Length", out values))
                {

                    long receivedSize;
                    long.TryParse(values.FirstOrDefault(), out receivedSize);

                    if (receivedSize > _limit)
                    {
                        string message = $"Payload limit is {_limit}";
                        owinResponse.OnSendingHeaders(state =>
                        {
                            OwinResponse owinResponseState = (OwinResponse)state;
                            owinResponseState.StatusCode = 413;
                            owinResponseState.ReasonPhrase = message;

                        }, owinResponse);

                        return context.Response.WriteAsync(message);//Short-circuit pipeline
                    }
                }
            }

            return Next.Invoke(context);
        }
    }
}
