using System;

namespace Caching.Task1.FibonacciGenerating
{
	/// <summary>
	/// Provides methods to generate Fibbonacci sequences and numbers.
	/// </summary>
	public static class FibonacciGenerator
	{
		/// <summary>
		/// Generates a Fibbonacci sequence of the <paramref name="length" /> size.
		/// </summary>
		/// <param name="length">The length of the generated sequence.</param>
		/// <returns>Generated sequence.</returns>
		public static int[] Sequence(int length)
		{
			if (length < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(length), length,
					"The length of the Fibbonacci sequence must be greater than zero.");
			}

			var sequence = new int[length];

			for (var i = 0; i < sequence.Length; i++)
			{
				sequence[i] = Number(i);
			}

			return sequence;
		}

		/// <summary>
		/// Generates a Fibbonacci number on the <paramref name="position" /> position in the Fibbonacci sequence.
		/// </summary>
		/// <param name="position">The position of the generated number.</param>
		/// <returns>Generated number.</returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public static int Number(int position)
		{
			if (position < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(position), position,
					"The Fibbonacci position must be greater than zero.");
			}

			var number = 0;
			var nextNumber = 1;

			for (var i = 0; i < position; i++)
			{
				int previousNumber = number;
				number = nextNumber;
				nextNumber = previousNumber + nextNumber;
			}

			return number;
		}
	}
}