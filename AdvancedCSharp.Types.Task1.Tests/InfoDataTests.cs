using System;
using AdvancedCSharp.Types.Task1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdvancedCSharp.Types.Task1Tests
{
	[TestClass]
	public class InfoDataTests
	{
		/// <summary>
		/// Scenario Creates <see cref="InfoData" /> instance with invalid <see cref="InfoData.FirstName" />
		/// Given Invalid value for <see cref="InfoData.FirstName" /> and valid value for <see cref="InfoData.LastName" />
		/// When Calls the constructor to create <see cref="InfoData" /> instance
		/// Then Throws exception about not valid value
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void InfoData_InvalidFirstName_Throw()
		{
			//Arrange
			string firstName = string.Empty + "     ";
			const string lastName = "Ivanov";

			//Act
			var infoData = new InfoData(firstName, lastName);

			//Assert is handled by exception
		}

		/// <summary>
		/// Scenario Creates <see cref="InfoData" /> instance with valid values
		/// Given Valid values
		/// When Calls the constructor to create <see cref="InfoData" /> instance
		/// Then Creates an instance of <see cref="InfoData" /> is not equals to default
		/// </summary>
		[TestMethod]
		public void InfoData_ValidValues()
		{
			//Arrange
			const string firstName = "Ivan";
			const string lastName = "Ivanov";

			//Act
			var infoData = new InfoData(firstName, lastName);

			//Assert
			Assert.IsTrue(infoData != default(InfoData));
		}

		/// <summary>
		/// Scenario Equals two instance of <see cref="InfoData" /> with same values
		/// Given Two instance of <see cref="InfoData" /> with same values
		/// When Calls <see cref="InfoData.Equals(object)" />
		/// Then Instances must be equal
		/// </summary>
		[TestMethod]
		public void Equals_SameValues()
		{
			//Arrange
			const string firstName = "Ivan";
			const string lastName = "Ivanov";
			var firstInfoData = new InfoData(firstName, lastName);
			var secondInfoData = new InfoData(firstName, lastName);

			//Act
			bool isEqual = firstInfoData.Equals(secondInfoData);

			//Assert
			Assert.IsTrue(isEqual);
		}

		/// <summary>
		/// Scenario Equals two instance of <see cref="InfoData" /> with different values
		/// Given Two instance of <see cref="InfoData" /> with different values
		/// When Calls <see cref="InfoData.Equals(object)" />
		/// Then Instances must not be equal
		/// </summary>
		[TestMethod]
		public void Equals_DifferentValues()
		{
			//Arrange
			var firstInfoData = new InfoData("Ivan", "Ivanov");
			var secondInfoData = new InfoData("Aleksey", "Alekseev");

			//Act
			bool isEqual = firstInfoData.Equals(secondInfoData);

			//Assert
			Assert.IsFalse(isEqual);
		}

		/// <summary>
		/// Scenario Gets hash code of two equals <see cref="InfoData" /> instances
		/// Given Two equal <see cref="InfoData" /> instances
		/// When Calls <see cref="InfoData.GetHashCode" />
		/// Then Hash codes must be equal
		/// </summary>
		[TestMethod]
		public void GetHashCode_SameHashCodeForEqualsInfoData()
		{
			//Arrange
			const string firstName = "Ivan";
			const string lastName = "Ivanov";
			var firstInfoData = new InfoData(firstName, lastName);
			var secondInfoData = new InfoData(firstName, lastName);

			//Act
			int firstHashCode = firstInfoData.GetHashCode();
			int secondHashCode = secondInfoData.GetHashCode();

			//Assert
			Assert.IsTrue(firstHashCode == secondHashCode);
		}
	}
}