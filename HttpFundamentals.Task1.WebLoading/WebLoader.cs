using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using HtmlAgilityPack;

namespace HttpFundamentals.Task1.WebLoading
{
	/// <summary>
	/// Allows to load resources recursively to the file system.
	/// </summary>
	public class WebLoader
	{
		private readonly Uri _baseUri;

		private readonly WebLoaderConfiguration _configuration;

		private readonly string _path;

		/// <summary>
		/// Initializes a new instance of the <see cref="WebLoader" /> class.
		/// </summary>
		/// <param name="configuration">Configuration.</param>
		/// <exception cref="ArgumentException"></exception>
		public WebLoader(WebLoaderConfiguration configuration)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException(nameof(configuration));
			}

			_configuration = configuration;
			_baseUri = new Uri(configuration.Url);
			_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configuration.DestinationPath);
		}

		/// <summary>
		/// Returns base path.
		/// </summary>
		protected string BasePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _path);


		/// <summary>
		/// Starts to load resources recursively.
		/// </summary>
		public void Load()
		{
			if (Directory.Exists(_path))
			{
				Directory.Delete(_path, true);
			}

			using (var httpClient = new HttpClient())
			{
				LoadPage(_baseUri, httpClient, 0);
			}
		}

		/// <summary>
		/// Operates a page loading.
		/// </summary>
		/// <param name="uri">Uniform resource identifier of the processed resource.</param>
		/// <param name="httpClient">An instance of the <see cref="HttpClient" /> class to make HTTP requests.</param>
		/// <param name="currentDepth">Current depth in the resources tree.</param>
		/// <exception cref="ArgumentNullException"></exception>
		protected void LoadPage(Uri uri, HttpClient httpClient, int currentDepth)
		{
			if (uri == null)
			{
				throw new ArgumentNullException(nameof(uri));
			}

			if (httpClient == null)
			{
				throw new ArgumentNullException(nameof(httpClient));
			}

			var response = httpClient.GetAsync(uri).Result;
			string content = response.Content.ReadAsStringAsync().Result;
			var htmlDocument = new HtmlDocument();

			htmlDocument.LoadHtml(content);
			SavePage(htmlDocument, uri);
			SaveAssets(htmlDocument, httpClient, uri);

			if (currentDepth >= _configuration.Depth)
			{
				return;
			}

			var referencedPages =
				htmlDocument.DocumentNode.SelectNodes("//a[@href]").Select(n => n.Attributes["href"].Value).ToList();

			foreach (string referencedPage in referencedPages)
			{
				LoadPage(new Uri(uri, referencedPage), httpClient, currentDepth + 1);
			}
		}

		/// <summary>
		/// Saves a loaded page to the file system as a HTML document.
		/// </summary>
		/// <param name="htmlDocument">HTML document to save.</param>
		/// <param name="uri">Uniform resource identifier of the processed page.</param>
		/// <exception cref="ArgumentNullException"></exception>
		protected virtual void SavePage(HtmlDocument htmlDocument, Uri uri)
		{
			if (htmlDocument == null)
			{
				throw new ArgumentNullException(nameof(htmlDocument));
			}

			if (uri == null)
			{
				throw new ArgumentNullException(nameof(uri));
			}

			string fileName = GetFileName(uri);
			string directory = Path.Combine(BasePath, _path, GetSubdirectoryPath(uri));

			Directory.CreateDirectory(directory);
			htmlDocument.Save(Path.Combine(directory,
				"index.html" + (string.IsNullOrWhiteSpace(fileName) ? string.Empty : "@" + fileName)));
		}

		/// <summary>
		/// Saves assets (e.g. images, CSS, etc.) of the <paramref name="htmlDocument" /> to the file system.
		/// </summary>
		/// <param name="htmlDocument">HTML document assets to be saved. </param>
		/// <param name="httpClient">An instance of the <see cref="HttpClient" /> class to make HTTP requests.</param>
		/// <param name="baseUri">Uniform resource identifier of the processed page.</param>
		protected virtual void SaveAssets(HtmlDocument htmlDocument, HttpClient httpClient, Uri baseUri)
		{
			if (htmlDocument == null)
			{
				throw new ArgumentNullException(nameof(htmlDocument));
			}

			if (httpClient == null)
			{
				throw new ArgumentNullException(nameof(httpClient));
			}

			if (baseUri == null)
			{
				throw new ArgumentNullException(nameof(baseUri));
			}

			var assets = new List<string>();
			var images = htmlDocument.DocumentNode.SelectNodes("//img[@src]");
			var links = htmlDocument.DocumentNode.SelectNodes("//link[@href]");

			if (images != null)
			{
				assets.AddRange(images.Where(
					n =>
						n.Attributes["src"].Value != null &&
						n.Attributes["src"].Value.StartsWith(PathExtensions.WebSeparatorChar.ToString(), StringComparison.CurrentCulture))
					.Select(n => n.Attributes["src"].Value)
					.ToList());
			}

			if (links != null)
			{
				assets.AddRange(links
					.Where(
						n =>
							n.Attributes["href"].Value != null &&
							n.Attributes["href"].Value.StartsWith(PathExtensions.WebSeparatorChar.ToString(), StringComparison.CurrentCulture))
					.Select(n => n.Attributes["href"].Value)
					.ToList());
			}

			foreach (string asset in assets)
			{
				var uri = new Uri(baseUri, asset);
				string directory = Path.Combine(BasePath, GetSubdirectoryPath(uri));
				string path = Path.Combine(directory, GetFileName(uri));

				var response = httpClient.GetAsync(uri).Result;
				var content = response.Content.ReadAsStreamAsync().Result;

				if (!Directory.Exists(directory))
				{
					Directory.CreateDirectory(directory);
				}

				using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
				{
					content.CopyTo(fileStream);
				}
			}
		}

		private static string GetFileName(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException(nameof(uri));
			}

			return
				uri.LocalPath.Substring(uri.PathAndQuery.LastIndexOf(PathExtensions.WebSeparatorChar))
					.ReplaceIllegalChars()
					.TrimStartWebSeparator();
		}

		private static string GetSubdirectoryPath(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException(nameof(uri));
			}

			return
				uri.LocalPath.Substring(0, uri.PathAndQuery.LastIndexOf(PathExtensions.WebSeparatorChar))
					.ReplaceIllegalChars()
					.TrimStartWebSeparator()
					.Replace(PathExtensions.WebSeparatorChar, Path.DirectorySeparatorChar);
		}
	}
}