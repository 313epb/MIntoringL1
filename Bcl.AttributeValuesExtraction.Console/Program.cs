using System.Collections.Generic;
using System.Linq;

namespace Bcl.AttributeValuesExtraction.Console
{
	internal class Program
	{
		private static void Main()
		{
			var provider = new CodeAuthorInfoProvider("Bcl.AttributeValuesExtraction.Data");
			var infoElements = provider.InfoElements.ToList();
			var distingInfoElements = infoElements.DistinctInfo().ToList();

			System.Console.WriteLine(ConsoleResource.CodeAuthorInfo);
			WriteInfo(infoElements);
			System.Console.WriteLine();

			System.Console.WriteLine(ConsoleResource.DistinctCodeAuthorInfo);
			WriteInfo(distingInfoElements);
			System.Console.WriteLine();

			distingInfoElements.Sort();

			System.Console.WriteLine(ConsoleResource.SortedDistinctCodeAuthorInfo);
			WriteInfo(distingInfoElements);
			System.Console.WriteLine();

			System.Console.ReadLine();
		}

		private static void WriteInfo(IEnumerable<CodeAuthorInfoAttribute> elements)
		{
			if (elements == null)
			{
				return;
			}

			foreach (var element in elements)
			{
				System.Console.WriteLine(element);
			}
		}
	}
}