using System;
using System.Collections.Generic;
using AdvancedCSharp.Types.Task1.Processing;

namespace AdvancedCSharp.Types.Task1
{
	/// <summary>
	/// Represents source data.
	/// </summary>
	public class Source
	{
		private readonly IProcessor<InfoData> _processor;

		/// <summary>
		/// Initializes a new instance of the <see cref="Source" /> class.
		/// </summary>
		/// <param name="processor">Implementation of the <see cref="IProcessor{T}" />.</param>
		public Source(IProcessor<InfoData> processor)
		{
			if (processor == null)
			{
				throw new ArgumentNullException(nameof(processor));
			}

			_processor = processor;
		}

		//		Warning CA1822  The 'this' parameter(or 'Me' in Visual Basic) of 'Source.CheckAndProceed(List<InfoData>)' is never used.Mark the member as static (or Shared in Visual Basic) or use 'this'/'Me' in the method body or at least one property accessor, if appropriate.AdvancedCSharp.Types.Task1
		/// <summary>
		/// Executes check of the source <paramref name="data" /> and then passes source <paramref name="data" /> to
		/// <see cref="Destination{T}" />.
		/// </summary>
		/// <param name="data">Source data to check and proceed.</param>
		public void CheckAndProceed(IEnumerable<InfoData> data)
		{
			var destination = new Destination<InfoData>(data, _processor);
			destination.ProceedData();
		}
	}
}