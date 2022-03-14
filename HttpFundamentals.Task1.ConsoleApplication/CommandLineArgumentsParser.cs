using HttpFundamentals.Task1.WebLoading;

namespace HttpFundamentals.Task1.ConsoleApplication
{
	/// <summary>
	/// Parse methods of command line input arguments into the <see cref="WebLoaderConfiguration"/> class.
	/// </summary>
	public static class CommandLineArgumentsParser
	{
		/// <summary>
		/// Parses <paramref name="arguments"/> into the <see cref="WebLoaderConfiguration"/> class.
		/// </summary>
		/// <param name="arguments">Arguments to parse.</param>
		/// <returns>Initializes configuration.</returns>
		public static WebLoaderConfiguration Parse(string[] arguments)
		{
			return new WebLoaderConfiguration();
		}
	}
}
