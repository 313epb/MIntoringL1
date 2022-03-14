using System.Web.Mvc;
using System.Web.Routing;
using NLog;

namespace MvcMusicStore
{
	public class RouteConfig
	{
		private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

		public static void RegisterRoutes(RouteCollection routes)
		{
			Logger.Debug("Start to register MVC routings.");

			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.MapRoute(
				"Default",
				"{controller}/{action}/{id}",
				new {controller = "Home", action = "Index", id = UrlParameter.Optional}
				);

			Logger.Info("MVC routes has registered.");
		}
	}
}