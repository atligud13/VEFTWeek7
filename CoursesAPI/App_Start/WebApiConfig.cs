using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CoursesAPI
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.MessageHandlers.Add(new LanguageMessageHandler());

            // Add the Authorize attribute to all api calls
            // Use [AllowAnonymous] if it should be open
            config.Filters.Add(new AuthorizeAttribute());

            config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
		}
	}
}
