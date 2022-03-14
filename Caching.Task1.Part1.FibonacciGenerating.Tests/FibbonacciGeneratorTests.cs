using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Caching.Task1.FibonacciGenerating.Tests
{
	[TestClass]
	public class FibbonacciGeneratorTests
	{
		/// <summary>
		/// Scenario Validates the length input parameter of the <see cref="FibonacciGenerator.Sequence" />
		/// Given Invalid length value
		/// When Generates Fibbonacci sequence
		/// Then Throws an exception of the <see cref="ArgumentOutOfRangeException" /> class
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void Sequence_InvalidLength()
		{
			//Arrange
			const int length = 0;

			//Act
			var fibbonacciSequence = FibonacciGenerator.Sequence(length);

			//Assert is handled by exception
		}

		/// <summary>
		/// Scenario Validates the Fibbonacci position to generate number on the position
		/// Given Invalid Fibbonacci position
		/// When Generates Fibbonacci number on the position
		/// Then Throws an exception of the <see cref="ArgumentOutOfRangeException" /> class
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void Number_InvalidPosition()
		{
			//Arrange
			const int position = -1;

			//Act
			int number = FibonacciGenerator.Number(position);

			//Assert is handled by exception
		}

		/// <summary>
		/// Scenario Generates the first number of Fibbonacci sequence
		/// Given The first position of the Fibbonaci sequence
		/// When Generates Fibbonacci number on the first position
		/// Then Return correct number
		/// </summary>
		[TestMethod]
		public void Number_FirstPosition()
		{
			//Arrange
			const int position = 0;

			//Act
			int number = FibonacciGenerator.Number(position);

			//Assert
			Assert.AreEqual(0, number);
		}

		/// <summary>
		/// Scenario Generates a number of Fibbonacci sequence on the second position
		/// Given The position of the second number
		/// When Generates Fibbonacci number by the <see cref="FibonacciGenerator.Number" /> on the second position
		/// Then Returns correct number
		/// </summary>
		[TestMethod]
		public void Number_SecondPosition()
		{
			//Arrange
			const int position = 1;

			//Act
			int secondNumber = FibonacciGenerator.Number(position);

			//Assert
			Assert.AreEqual(1, secondNumber);
		}

		/// <summary>
		/// Scenario Generates an number of Fibbonacci sequence on the arbitrary position
		/// Given A number of Fibbonacci sequence on the position
		/// When Generates the number by the <see cref="FibonacciGenerator.Number" /> on the arbitrary position
		/// Then Returns correct the arbitrary number
		/// </summary>
		[TestMethod]
		public void Number_ArbitraryPosition()
		{
			//Arrange
			const int position = 7;

			//Act
			int number = FibonacciGenerator.Number(position);

			//Assert
			Assert.AreEqual(13, number);
		}

		/// <summary>
		/// Scenario Generates a Fibbonacci sequence of the arbitrary size
		/// Given The size of the sequence
		/// When Generates the sequence
		/// Then The generated sequence must be correct
		/// </summary>
		[TestMethod]
		public void Sequence_WithArbitraryItemCount()
		{
			//Arrange
			const int length = 8;

			//Act
			var sequence = FibonacciGenerator.Sequence(length);

			//Assert
			Assert.IsTrue(new[] {0, 1, 1, 2, 3, 5, 8, 13}.SequenceEqual(sequence));
		}
	}
}