using System.IO;
using System.Net.Http.Formatting;
using System.Web.Http;
using Gaona.Assigment.Web.Filters;
using Gaona.Assigment.Web.Middleware;
using Microsoft.Owin;
using Owin;
using Swashbuckle.Application;

[assembly: OwinStartup(typeof(Gaona.Assigment.Web.Startup))]


namespace Gaona.Assigment.Web
{
    /// <summary>
    /// My OWIN Startup class, where all the configuration is defined.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Defining the configuration for the project. This includes
        /// - Adding middleware components
        /// - Defining the routing 
        /// - Setting the media type formatters
        /// 
        /// and the following suggested IMPROVEMENTS
        /// - Global exception filter
        /// - Swagger docs
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {

            app.Use(typeof(PayloadSizeLimitMiddleware), 1048576L); //IMPROVEMENT 1: Set the limit to 1MB
            //Defining the config for web api; no need to create a default route
            //since we are handling everything with attributes
            HttpConfiguration configuration = new HttpConfiguration();

            //allow json only
            configuration.Formatters.Clear();
            configuration.Formatters.Add(new JsonMediaTypeFormatter());

            //enable route attributes
            configuration.MapHttpAttributeRoutes();


            //IMPROVEMENT
            //add global exception filter
            configuration.Filters.Add(new GlobalExceptionFilterAttribute());

            
            //IMPROVEMENT
            //add Swagger docs
            configuration
                .EnableSwagger(docConfig =>
                {

                    docConfig.SingleApiVersion("v1", "Waes Assignment - Differ");
                    docConfig.PrettyPrint();
                    docConfig.GroupActionsBy(description => description.HttpMethod.ToString());

                    if (File.Exists("Gaona.Assigment.Web.xml"))
                    {
                        docConfig.IncludeXmlComments("Gaona.Assigment.Web.xml");

                    }
                    else
                    {
                        docConfig.IncludeXmlComments($@"{System.AppDomain.CurrentDomain.BaseDirectory}\bin\Gaona.Assigment.Web.xml");
                    }
                   

                })
                .EnableSwaggerUi(uiconfig =>
                {
                    uiconfig.DocumentTitle("Waes Assignment - Differ");
                    uiconfig.DocExpansion(DocExpansion.List);
                });

            configuration.EnsureInitialized();
            app.UseWebApi(configuration);
        }
    }
}
