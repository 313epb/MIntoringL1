namespace Serialization.Basic
{
	internal static class Constants
	{
		/// <summary>
		/// Namespace.
		/// </summary>
		internal const string Namespace = "http://library.by/catalog";

		/// <summary>
		/// </summary>
		internal static class Entities
		{
			internal static class Catalog
			{
				internal const string Name = "catalog";

				internal static class Attributes
				{
					internal const string Date = "date";
				}
			}

			internal static class Book
			{
				internal const string Name = "book";

				internal static class Fields
				{
					internal const string Isbn = "isbn";

					internal const string Author = "author";

					internal const string Title = "title";

					internal const string Genre = "genre";

					internal const string Publisher = "publisher";

					internal const string PublishDate = "publish_date";

					internal const string Description = "description";

					internal const string RegistrationDate = "registration_date";
				}

				internal static class Attributes
				{
					internal const string Id = "id";
				}
			}

			internal static class Genre
			{
				internal static class Fields
				{
					internal const string ScienceFiction = "Science Fiction";
				}
			}
		}
	}
}