using System;
using System.Xml.Serialization;

namespace Serialization.Basic.Models
{
	/// <summary>
	/// A book catalog.
	/// </summary>
	[Serializable]
	[XmlType(AnonymousType = true, Namespace = Constants.Namespace)]
	[XmlRoot(Namespace = Constants.Namespace, ElementName = Constants.Entities.Catalog.Name, IsNullable = false)]
	public class Catalog
	{
		/// <summary>
		/// Books in a catalog.
		/// </summary>
		[XmlElement(ElementName = Constants.Entities.Book.Name)]
		public Book[] Books { get; set; }

		/// <summary>
		/// Date of a catalog.
		/// </summary>
		[XmlAttribute(DataType = "date", AttributeName = Constants.Entities.Catalog.Attributes.Date)]
		public DateTime Date { get; set; }
	}
}