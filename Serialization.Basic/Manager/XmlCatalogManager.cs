using System;
using System.IO;
using System.Xml.Serialization;
using Serialization.Basic.Models;

namespace Serialization.Basic.Manager
{
	/// <summary>
	/// Writes and reads <see cref="Catalog" /> information to the XML file.
	/// </summary>
	public class XmlCatalogManager : ICatalogManager
	{
		private readonly string _path;

		/// <summary>
		/// Initializes a new instance of the <see cref="XmlCatalogManager" /> class.
		/// </summary>
		/// <param name="path">Path to the XML file.</param>
		public XmlCatalogManager(string path)
		{
			if (string.IsNullOrWhiteSpace(path))
			{
				throw new ArgumentException("The specified path must not be null or contain white spaces.", nameof(path));
			}

			_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
		}

		/// <summary>
		/// Writes <paramref name="catalog" /> to source.
		/// </summary>
		/// <param name="catalog">Catalog to save.</param>
		public void Write(Catalog catalog)
		{
			if (catalog == null)
			{
				throw new ArgumentNullException(nameof(catalog));
			}

			using (var fileStream = new FileStream(_path, FileMode.Create))
			{
				var xmlSerializer = new XmlSerializer(typeof(Catalog));

				xmlSerializer.Serialize(fileStream, catalog);
			}
		}

		/// <summary>
		/// Reads <see cref="Catalog" /> from source.
		/// </summary>
		/// <returns>Read catalog.</returns>
		public Catalog Read()
		{
			Catalog catalog;

			using (var fileStream = new FileStream(_path, FileMode.Open))
			{
				var xmlSerializer = new XmlSerializer(typeof(Catalog));

				catalog = (Catalog) xmlSerializer.Deserialize(fileStream);
			}

			return catalog;
		}
	}
}