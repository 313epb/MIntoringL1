using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bcl.AttributeValuesExtraction.Tests
{
	[TestClass]
	public class CodeAuthorInfoAttributeTests
	{
		#region GetHashCode

		/// <summary>
		/// Scenario Gets hash code of two equals <see cref="CodeAuthorInfoAttribute" /> instances
		/// Given Two equal <see cref="CodeAuthorInfoAttribute" /> instances
		/// When Calls <see cref="CodeAuthorInfoAttribute.GetHashCode" />
		/// Then Hash codes must be equal
		/// </summary>
		[TestMethod]
		public void GetHashCode_SameHashCodeForEqualsValues()
		{
			//Arrange
			const string name = "Ivan Ivanov";
			const string email = "ivan.ivanov@outlook.com";
			var first = new CodeAuthorInfoAttribute(name, email);
			var second = new CodeAuthorInfoAttribute(name, email);

			//Act
			int firstHashCode = first.GetHashCode();
			int secondHashCode = second.GetHashCode();

			//Assert
			Assert.IsTrue(firstHashCode == secondHashCode);
		}

		#endregion

		#region CompareTo

		/// <summary>
		/// Scenario Compares two objects of the <see cref="CodeAuthorInfoAttribute" /> with same values
		/// Given Two objects of the <see cref="CodeAuthorInfoAttribute" /> with same values
		/// When Calls <see cref="CodeAuthorInfoAttribute.CompareTo" />
		/// Then Comparing must return 0
		/// </summary>
		[TestMethod]
		public void CompareTo_SameValues()
		{
			//Arrange
			const string name = "Ivan Ivanov";
			const string email = "ivan.ivanov@outlook.com";
			var first = new CodeAuthorInfoAttribute(name, email);
			var second = new CodeAuthorInfoAttribute(name, email);

			//Act
			int comparingResult = first.CompareTo(second);

			//Assert
			Assert.IsTrue(comparingResult == 0);
		}

		/// <summary>
		/// Scenario Compares two objects of the <see cref="CodeAuthorInfoAttribute" /> with different values
		/// Given Two objects of the <see cref="CodeAuthorInfoAttribute" /> with different values
		/// When Calls <see cref="CodeAuthorInfoAttribute.CompareTo" />
		/// Then Comparing must return less than 0 because the first object is greater than the second
		/// </summary>
		[TestMethod]
		public void CompareTo_GreaterThan()
		{
			//Arrange
			var first = new CodeAuthorInfoAttribute("Aleksey Alekseev", "aleksey.alekseev@outlook.com");
			var second = new CodeAuthorInfoAttribute("Ivan Ivanov", "ivan.ivanov@outlook.com");

			//Act
			int comparingResult = first.CompareTo(second);

			//Assert
			Assert.IsTrue(comparingResult < 0);
		}

		/// <summary>
		/// Scenario Compares two objects of the <see cref="CodeAuthorInfoAttribute" /> with different values
		/// Given Two objects of the <see cref="CodeAuthorInfoAttribute" /> with different values
		/// When Calls <see cref="CodeAuthorInfoAttribute.CompareTo" />
		/// Then Comparing must return greater than 0 because the first object is less than the second
		/// </summary>
		[TestMethod]
		public void CompareTo_LessThan()
		{
			//Arrange
			var first = new CodeAuthorInfoAttribute("Ivan Ivanov", "ivan.ivanov@outlook.com");
			var second = new CodeAuthorInfoAttribute("Aleksey Alekseev", "aleksey.alekseev@outlook.com");

			//Act
			int comparingResult = first.CompareTo(second);

			//Assert
			Assert.IsTrue(comparingResult > 0);
		}

		#endregion

		#region Equals

		/// <summary>
		/// Scenario Equals two objects of the <see cref="CodeAuthorInfoAttribute" /> class with same values but with different
		/// case
		/// Given Same values with different case
		/// When Calls <see cref="CodeAuthorInfoAttribute.Equals(Bcl.AttributeValuesExtraction.CodeAuthorInfoAttribute)" />
		/// Then The objects must be equal
		/// </summary>
		[TestMethod]
		public void Equals_DifferentCase()
		{
			//Arrange
			var first = new CodeAuthorInfoAttribute("Ivan Ivanov", "Ivan.Ivanov@outlook.com");
			var second = new CodeAuthorInfoAttribute("ivan ivanov", "ivan.ivanov@outlook.com");

			//Act
			bool isEqual = first.Equals(second);

			//Assert
			Assert.IsTrue(isEqual);
		}

		/// <summary>
		/// Scenario Equals two objects of the <see cref="CodeAuthorInfoAttribute" /> class with same values but with different
		/// number of white spaces
		/// Given Same values with different number of white spaces
		/// When Calls <see cref="CodeAuthorInfoAttribute.Equals(Bcl.AttributeValuesExtraction.CodeAuthorInfoAttribute)" />
		/// Then The objects must be equal
		/// </summary>
		[TestMethod]
		public void Equals_WhiteSpaces()
		{
			//Arrange
			var first = new CodeAuthorInfoAttribute("Ivan   Ivanov  ", "Ivan.Ivanov@outlook.com   ");
			var second = new CodeAuthorInfoAttribute("Ivan Ivanov", "Ivan.Ivanov@outlook.com");

			//Act
			bool isEqual = first.Equals(second);

			//Assert
			Assert.IsTrue(isEqual);
		}

		/// <summary>
		/// Scenario Equals two objects of the <see cref="CodeAuthorInfoAttribute" /> class with different values
		/// Given Different values
		/// When Calls <see cref="CodeAuthorInfoAttribute.Equals(Bcl.AttributeValuesExtraction.CodeAuthorInfoAttribute)" />
		/// Then The objects must not be equal
		/// </summary>
		[TestMethod]
		public void Equals_DifferentValues()
		{
			//Arrange
			var first = new CodeAuthorInfoAttribute("Ivan", "Ivan@outlook.com");
			var second = new CodeAuthorInfoAttribute("Aleksey", "Aleksey@outlook.com");

			//Act
			bool isEqual = first.Equals(second);

			//Assert
			Assert.IsFalse(isEqual);
		}

		#endregion

		#region CodeAuthorInfoAttribute

		/// <summary>
		/// Scenario Creates a new instance of the <see cref="CodeAuthorInfoAttribute" /> class with valid values
		/// Given Valid values for the constructor
		/// When Calls the constructor of the <see cref="CodeAuthorInfoAttribute" /> class
		/// Then Creates a new not null instance
		/// </summary>
		[TestMethod]
		public void CodeAuthorInfoAttribute_ValidValues()
		{
			//Arrange
			const string name = "Ivan Ivanov";
			const string email = "ivan.ivanov@outlook.com";

			//Act
			var codeAuthorInfoAttribute = new CodeAuthorInfoAttribute(name, email);

			//Assert
			Assert.IsNotNull(codeAuthorInfoAttribute);
		}

		/// <summary>
		/// Scenario Creates a new instance of the <see cref="CodeAuthorInfoAttribute" /> class with invalid values
		/// Given Invalid values for the constructor
		/// When Calls the constructor of the <see cref="CodeAuthorInfoAttribute" /> class
		/// Then Throws an <see cref="ArgumentException" /> exception
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void CodeAuthorInfoAttribute_InvalidValues()
		{
			//Arrange
			string name = string.Empty;
			string email = string.Empty;

			//Act
			var codeAuthorInfoAttribute = new CodeAuthorInfoAttribute(name, email);

			//Assert is handled by an exception
		}

		#endregion
	}
}