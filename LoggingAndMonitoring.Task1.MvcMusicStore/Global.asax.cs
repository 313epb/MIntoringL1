using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NLog;

namespace MvcMusicStore
{
	public class MvcApplication : HttpApplication
	{
		private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

		protected void Application_Start()
		{
			Logger.Debug("Application is about to perform registration in {0}", nameof(Application_Start));

			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			PerformanceCounterConfig.ClearCounters();

			Logger.Info("Application started.");
		}
	}
}