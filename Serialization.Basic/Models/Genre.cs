using System;
using System.Xml.Serialization;

namespace Serialization.Basic.Models
{
	/// <summary>
	/// Genre enumeration.
	/// </summary>
	[Serializable]
	public enum Genre
	{
		/// <summary>
		/// Computer genre.
		/// </summary>
		Computer,

		/// <summary>
		/// Fantasy genre.
		/// </summary>
		Fantasy,

		/// <summary>
		/// Romance genre.
		/// </summary>
		Romance,

		/// <summary>
		/// Horror genre.
		/// </summary>
		Horror,

		/// <summary>
		/// Science fiction.
		/// </summary>
		[XmlEnum(Name = Constants.Entities.Genre.Fields.ScienceFiction)] ScienceFiction
	}
}