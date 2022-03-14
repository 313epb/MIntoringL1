using System;
using System.Xml.Serialization;

namespace Serialization.Basic.Models
{
	/// <summary>
	/// A catalog book.
	/// </summary>
	[Serializable]
	[XmlType(Namespace = Constants.Namespace, AnonymousType = true)]
	public class Book
	{
		/// <summary>
		/// Isbn.
		/// </summary>
		[XmlElement(ElementName = Constants.Entities.Book.Fields.Isbn)]
		public string Isbn { get; set; }

		/// <summary>
		/// Author.
		/// </summary>
		[XmlElement(ElementName = Constants.Entities.Book.Fields.Author)]
		public string Author { get; set; }

		/// <summary>
		/// Title.
		/// </summary>
		[XmlElement(ElementName = Constants.Entities.Book.Fields.Title)]
		public string Title { get; set; }

		/// <summary>
		/// Genre.
		/// </summary>
		[XmlElement(ElementName = Constants.Entities.Book.Fields.Genre)]
		public Genre Genre { get; set; }

		/// <summary>
		/// Publisher.
		/// </summary>
		[XmlElement(ElementName = Constants.Entities.Book.Fields.Publisher)]
		public string Publisher { get; set; }

		/// <summary>
		/// Publish date.
		/// </summary>
		[XmlElement(ElementName = Constants.Entities.Book.Fields.PublishDate, DataType = "date")]
		public DateTime PublishDate { get; set; }

		/// <summary>
		/// Registration date.
		/// </summary>
		[XmlElement(ElementName = Constants.Entities.Book.Fields.RegistrationDate, DataType = "date")]
		public DateTime RegistrationDate { get; set; }

		/// <summary>
		/// Description.
		/// </summary>
		[XmlElement(ElementName = Constants.Entities.Book.Fields.Description)]
		public string Description { get; set; }

		/// <summary>
		/// Identifier.
		/// </summary>
		[XmlAttribute(AttributeName = Constants.Entities.Book.Attributes.Id)]
		public string Id { get; set; }
	}
}