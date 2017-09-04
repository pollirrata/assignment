using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Gaona.Assigment.Web.Filters
{
    /// <summary>
    /// IMPROVEMENT
    /// Global filter for catching and handling exceptions in the API
    /// 
    /// IMPROVEMENT
    /// Send an email whenever an exception is thrown
    /// </summary>
    public class GlobalExceptionFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// The async exception handler
        /// </summary>
        /// <param name="actionExecutedContext">action context</param>
        /// <param name="cancellationToken">token to cancell the async operations</param>
        /// <returns></returns>
        public override async Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            Exception exception = actionExecutedContext.Exception;
            HttpRequestMessage request = actionExecutedContext.Request;

            string body = $"URI: {request.Method} {request.RequestUri} \nException: {exception.Message} \nStackTrace:\n{exception.StackTrace}";

            //print message in the console
            Console.WriteLine("{0}\n\n{1}", exception, body);

            //Generate a pretty response
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            responseMessage.StatusCode = HttpStatusCode.InternalServerError;
            responseMessage.Content = new StringContent("Oops it seems we screwed up ┏༼ ◉ ╭╮ ◉༽┓", Encoding.UTF8, "text/plain");
            actionExecutedContext.Response = responseMessage;

            //send email with the error info
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            SendGridClient client = new SendGridClient(ConfigurationManager.AppSettings.Get("sgapikey"));
            EmailAddress from = new EmailAddress("errors@diff.com");
            EmailAddress to = new EmailAddress(ConfigurationManager.AppSettings.Get("errormail"));
            SendGridMessage msg = MailHelper.CreateSingleEmail(from, to, exception.Message, body, $"<marquee>{body.Replace("\n", "<br />")}</marquee>");
            await client.SendEmailAsync(msg, cancellationToken);
        }
    }
}
