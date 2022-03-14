using Serialization.Basic.Models;

namespace Serialization.Basic.Manager
{
	/// <summary>
	/// Provides writing and reading methods of <see cref="Catalog" /> information.
	/// </summary>
	public interface ICatalogManager
	{
		/// <summary>
		/// Writes <paramref name="catalog" /> to source.
		/// </summary>
		/// <param name="catalog">Catalog to save.</param>
		void Write(Catalog catalog);

		/// <summary>
		/// Reads <see cref="Catalog" /> from source.
		/// </summary>
		/// <returns>Read catalog.</returns>
		Catalog Read();
	}
}