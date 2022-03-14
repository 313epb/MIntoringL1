using System;

namespace HttpFundamentals.Task1.WebLoading
{
	internal static class PathExtensions
	{
		internal const char WebSeparatorChar = '/';

		internal static string TrimStartWebSeparator(this string url)
		{
			if (url == null)
			{
				throw new ArgumentNullException(nameof(url));
			}

			return url.TrimStart(WebSeparatorChar);
		}

		internal static string ReplaceIllegalChars(this string url)
		{
			if (url == null)
			{
				throw new ArgumentNullException(nameof(url));
			}

			return url.Replace("?", "@").Replace("*", string.Empty);
		}
	}
}