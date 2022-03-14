using System;
using AdvancedCSharp.Types.Task2;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdvancedCSharp.Types.Task2Tests
{
	[TestClass]
	public class ProductTests
	{
		#region GetHashCode

		/// <summary>
		/// Scenario Checks hash code equality for equals products
		/// Given Two products with valid equals values
		/// When Gets hash code of the products
		/// Then Hash codes of the products must be equals
		/// </summary>
		[TestMethod]
		public void GetHashCode_SameHashCodeForEqualsProducts()
		{
			//Arrange
			const string name = "Bycicle";
			const decimal price = 10;
			const int quantity = 10;

			var firstProduct = new Product(name, price, quantity);
			var secondProduct = new Product(name, price, quantity);

			//Act
			int firstHashCode = firstProduct.GetHashCode();
			int secondHashCode = secondProduct.GetHashCode();

			//Assert
			Assert.AreEqual(firstHashCode, secondHashCode);
		}

		#endregion

		#region Product

		/// <summary>
		/// Scenario Creates a product with name contains white spaces
		/// Given String contains white spaces for name
		/// And valid value for price
		/// And valid value for
		/// When Creates a product by the constructor
		/// Then Throws exception
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Product_EmptyWhiteSpacesName_Throw()
		{
			//Arrange
			string name = string.Empty + "    ";
			const decimal price = default(decimal);
			const int quantity = default(int);

			//Act
			var product = new Product(name, price, quantity);

			//Assert is handled by exception
		}

		/// <summary>
		/// Scenario Creates a product with negative price
		/// Given Valid value for name
		/// And negative value for price
		/// And valid value for quantity
		/// When Creates a product by the constructor
		/// Then Throws exception
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Product_NegativePrice_Throw()
		{
			//Arrange
			const string name = "Bycicle";
			const decimal price = -5;
			const int quantity = 10;

			//Act
			var product = new Product(name, price, quantity);

			//Assert is handled by the exception
		}

		/// <summary>
		/// Scenario Creates a product with negative quantity
		/// Given Valid value for name
		/// And valud value for price
		/// And negative value for quantity
		/// When Creates a product by the constructor
		/// Then Throws exception
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Product_NegativeQuantity_Throw()
		{
			//Arranage
			const string name = "Bycicle";
			const decimal price = 10;
			const int quantity = -10;

			//Act
			var product = new Product(name, price, quantity);

			//Assert is handled by the exception
		}

		/// <summary>
		/// Scenario Creates a product with valid values
		/// Given Valid values
		/// When Creates a product by the constructor
		/// Then Created product is not default value of <see cref="Product" />
		/// </summary>
		[TestMethod]
		public void Product_ValidValues()
		{
			//Arrange
			const string name = "Bycicle";
			const decimal price = 10;
			const int quantity = 5;

			//Act
			var product = new Product(name, price, quantity);

			//Assert
			Assert.IsTrue(product != default(Product));
		}

		#endregion

		#region Equals

		/// <summary>
		/// Scenario Checks equality of two products with same correct values
		/// Given Two products with same values
		/// When Calls equals check
		/// Then Products should be equal
		/// </summary>
		[TestMethod]
		public void Equals_SameValues()
		{
			//Arrange
			const string name = "Bycicle";
			const decimal price = 10;
			const int quantity = 10;

			var firstProduct = new Product(name, price, quantity);
			var secondProduct = new Product(name, price, quantity);

			//Act
			bool isEqual = firstProduct.Equals(secondProduct);

			//Assert
			Assert.IsTrue(isEqual);
		}

		/// <summary>
		/// Scenario Checks equality of two products with different correct values
		/// Given Two products with different values
		/// When Calls equals check
		/// Then Products should not be equal
		/// </summary>
		[TestMethod]
		public void Equals_DifferentValues()
		{
			//Arrange
			var firstProduct = new Product("Bycicle", 100, 100);
			var secondProduct = new Product("Ladder", 58, 20);

			//Act
			bool isEqual = firstProduct.Equals(secondProduct);

			//Assert
			Assert.IsFalse(isEqual);
		}

		/// <summary>
		/// Scenario Checks equality a product with a product packed into an object with same values
		/// Given One product and one product packed into an object with same values
		/// When Call equals check
		/// Then Products must be equal
		/// </summary>
		[TestMethod]
		public void Equals_WithObject()
		{
			//Arrange
			var product = new Product("Bycicle", 10, 10);
			var objectProduct = (object) product;

			//Act
			bool isEqual = product.Equals(objectProduct);

			//Assert
			Assert.IsTrue(isEqual);
		}

		/// <summary>
		/// Scenario Checks equality a product with null
		/// Given A product with correct values and null
		/// When Calls equals check
		/// Then Products should not be equal
		/// </summary>
		[TestMethod]
		public void Equals_WithNullObject()
		{
			//Arrange
			var product = new Product("Bycicle", 10, 100);

			//Act
			bool isEqual = product.Equals(null);

			//Assert
			Assert.IsFalse(isEqual);
		}

		/// <summary>
		/// Scenario Checks equality with a non product value
		/// Given A product with correct values and a object
		/// When Calls equals check
		/// Then Product should not be equal
		/// </summary>
		[TestMethod]
		public void Equals_WithNonProductObject()
		{
			//Arrange
			var product = new Product("Bycicle", 10, 100);
			var fakeProduct = new object();

			//Act
			bool isEqual = product.Equals(fakeProduct);

			//Assert   
			Assert.IsFalse(isEqual);
		}

		#endregion
	}
}