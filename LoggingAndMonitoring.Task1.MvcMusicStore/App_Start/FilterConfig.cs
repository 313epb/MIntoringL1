using System.Web.Mvc;
using NLog;

namespace MvcMusicStore
{
	public class FilterConfig
	{
		private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			Logger.Debug("Start to register global MCV filters.");

			filters.Add(new HandleErrorAttribute());

			Logger.Info("Global MVC filters registered.");
		}
	}
}