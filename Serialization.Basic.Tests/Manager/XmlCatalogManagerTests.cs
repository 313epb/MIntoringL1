using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serialization.Basic.Manager;
using Serialization.Basic.Models;

namespace Serialization.Basic.Tests.Manager
{
	[TestClass]
	public class XmlCatalogManagerTests
	{
		/// <summary>
		/// Scenario Creates a new instance of the <see cref="XmlCatalogManager" /> class with invalid path
		/// Given Invalid path value
		/// When Creates a new instance of the <see cref="XmlCatalogManager" /> class
		/// Then Throws an exception of the <see cref="ArgumentException" /> class
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void XmlCatalogManager_InvalidPath()
		{
			//Arrange
			string path = string.Empty;

			//Act
			var xmlCataloManager = new XmlCatalogManager(path);

			//Assert is handled by exception
		}

		/// <summary>
		/// Scenario Creates a new instance of the <see cref="XmlCatalogManager" /> class with valid path
		/// Given Valid value for the valid parameter
		/// When Creates a new instance of the <see cref="XmlCatalogManager" /> class
		/// Then Created instance is not null
		/// </summary>
		[TestMethod]
		public void XmlCatalogManger_ValidPath()
		{
			//Arrange
			const string path = "books.xml";

			//Act
			var xmlCatalogManager = new XmlCatalogManager(path);

			//Assert
			Assert.IsNotNull(xmlCatalogManager);
		}

		/// <summary>
		/// Scenario Reads <see cref="Catalog" /> information from the books.xml (included to the proejct) file
		/// Given XML file with <see cref="Catalog" /> information path
		/// When Reads <see cref="Catalog" /> information with the <see cref="ICatalogManager.Read" />
		/// Then Read <see cref="Catalog" /> information is not null
		/// </summary>
		[TestMethod]
		public void Read_FromBooksFile()
		{
			//Arrange
			var xmlCatalogManager = new XmlCatalogManager("books.xml");

			//Act
			var catalog = xmlCatalogManager.Read();

			//Assert
			Assert.IsNotNull(catalog);
		}

		/// <summary>
		/// Scenario Writes read <see cref="Catalog" /> information from the books.xml to the another XML file
		/// Given Path to the books.xml
		/// When Writes <see cref="Catalog" /> information with the <see cref="ICatalogManager.Write" />
		/// Then Written <see cref="Catalog" /> information is not null and the XML file is not empty
		/// </summary>
		[TestMethod]
		public void Write_ToXmlFile()
		{
			//Arrange
			var xmlCatalogManager = new XmlCatalogManager("books.xml");
			var catalog = xmlCatalogManager.Read();

			//Act
			xmlCatalogManager = new XmlCatalogManager("anotherBooks.xml");
			xmlCatalogManager.Write(catalog);

			//Assert
			var anotherCatalog = xmlCatalogManager.Read();
			Assert.IsNotNull(anotherCatalog);
		}
	}
}