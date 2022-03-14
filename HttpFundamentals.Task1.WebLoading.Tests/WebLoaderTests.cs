using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HttpFundamentals.Task1.WebLoading.Tests
{
	[TestClass]
	public class WebLoaderTests
	{
		[TestMethod]
		public void LoadTest()
		{
			var webLoader = new WebLoader(new WebLoaderConfiguration
			{
				Url = "http://www.lingvo.ru/",
				DestinationPath = "www.lingvo.ru"
			});

			webLoader.Load();
		}
	}
}