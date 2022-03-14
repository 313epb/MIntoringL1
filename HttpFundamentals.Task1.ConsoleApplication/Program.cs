using System;
using HttpFundamentals.Task1.WebLoading;

namespace HttpFundamentals.Task1.ConsoleApplication
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			WebLoaderConfiguration configration;

			if (args != null)
			{
				configration = CommandLineArgumentsParser.Parse(args);
			}

			Console.ReadLine();
		}
	}
}