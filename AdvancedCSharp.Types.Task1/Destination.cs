using System;
using System.Collections.Generic;
using AdvancedCSharp.Types.Task1.Processing;

namespace AdvancedCSharp.Types.Task1
{
	/// <summary>
	/// Represents destination point for the <see cref="Source" /> class.
	/// </summary>
	/// <typeparam name="T">Type of an object to be processed.</typeparam>
	public class Destination<T>
	{
		private readonly IProcessor<T> _processor;

		/// <summary>
		/// Source data to process.
		/// </summary>
		private readonly IEnumerable<T> _source;

		/// <summary>
		/// Initializes a new instance of the <see cref="Destination{T}" /> class.
		/// </summary>
		/// <param name="source">Source data to process.</param>
		/// <param name="processor"></param>
		/// <exception cref="ArgumentNullException">Throws when input parameters are null.</exception>
		public Destination(IEnumerable<T> source, IProcessor<T> processor)
		{
			if (source == null)
			{
				throw new ArgumentNullException(nameof(source));
			}

			if (processor == null)
			{
				throw new ArgumentNullException(nameof(processor));
			}

			_source = new List<T>(source);
			_processor = processor;
		}

		internal void ProceedData()
		{
			_processor.Process(_source);
		}
	}
}