using System.Web.Http;
using WebActivatorEx;
using mcare.API;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace mcare.API
{
	public class SwaggerConfig
	{
		public static void Register()
		{
			var thisAssembly = typeof(SwaggerConfig).Assembly;

			GlobalConfiguration.Configuration
                .EnableSwagger(c => { c.SingleApiVersion("v1", "mcare.API");
                c.IncludeXmlComments(string.Format(@"{0}\bin\mcare.API.XML",           
                           System.AppDomain.CurrentDomain.BaseDirectory)); })
				.EnableSwaggerUi(c => { });
		}
	}
}
