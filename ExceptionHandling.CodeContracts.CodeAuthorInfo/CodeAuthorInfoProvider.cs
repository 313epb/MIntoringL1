using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using ExceptionHandling.CodeContracts.CodeAuthorInfo.Exceptions;

namespace ExceptionHandling.CodeContracts.CodeAuthorInfo

{
	/// <summary>
	/// Provides information about classes and methods with the <see cref="CodeAuthorInfoAttribute" /> attribute from an
	/// assembly.
	/// </summary>
	public class CodeAuthorInfoProvider
	{
		private readonly ICollection<CodeAuthorInfoAttribute> _infoElements;

		/// <summary>
		/// Initializes a new instance of the <see cref="CodeAuthorInfoAttribute" /> class.
		/// </summary>
		/// <param name="assembly">
		/// Assembly to extract information about classes and methods with the
		/// <see cref="CodeAuthorInfoAttribute" /> attribute.
		/// </param>
		/// <exception cref="ArgumentNullException" />
		public CodeAuthorInfoProvider(Assembly assembly)
		{
			Contract.Requires<ArgumentNullException>(assembly != null, "assembly != null");

			_infoElements = new List<CodeAuthorInfoAttribute>(ExtractInfo(assembly));
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodeAuthorInfoAttribute" /> class.
		/// </summary>
		/// <param name="path">
		/// Path of an assembly to extract information about classes and methods with the
		/// <see cref="CodeAuthorInfoAttribute" /> attribute.
		/// </param>
		public CodeAuthorInfoProvider(string path) : this(LoadAssembly(path))
		{
		}

		/// <summary>
		/// Enumerator of the information about classes and methods with the <see cref="CodeAuthorInfoAttribute" /> attribute.
		/// </summary>
		public IEnumerable<CodeAuthorInfoAttribute> InfoElements => _infoElements;

		/// <summary>
		/// Extracts from the <paramref name="assembly" /> <see cref="CodeAuthorInfoAttribute" /> information.
		/// </summary>
		/// <param name="assembly">An assembly to extract information from.</param>
		/// <returns>
		/// Enumerator of the <see cref="CodeAuthorInfoAttribute" /> information about all classes and methods from the
		/// <paramref name="assembly" />.
		/// </returns>
		/// <exception cref="AggregateException" />
		protected static IEnumerable<CodeAuthorInfoAttribute> ExtractInfo(Assembly assembly)
		{
			var infoElements = new List<CodeAuthorInfoAttribute>();
			var membersInfo = GetMembersInfo(assembly);
			var exceptions = new List<Exception>();

			foreach (var memberInfo in membersInfo)
			{
				try
				{
					infoElements.AddRange(
						Attribute.GetCustomAttributes(memberInfo, typeof(CodeAuthorInfoAttribute))
							.OfType<CodeAuthorInfoAttribute>());
				}
				catch (Exception exception)
				{
					exceptions.Add(
						new ExtractingCodeAuthorInfoException("There is an error during extracting code author info.",
							memberInfo, exception));
				}
			}

			if (exceptions.Any())
			{
				throw new AggregateException("There are multiple errors during extracting code author info.", exceptions);
			}

			return infoElements;
		}

		/// <summary>
		/// Gets necessary members info from the <paramref name="assembly" /> to extract information.
		/// </summary>
		/// <param name="assembly">An assembly to get members from.</param>
		/// <returns>Enumerator of the members info.</returns>
		/// <exception cref="AggregateException" />
		protected static IEnumerable<MemberInfo> GetMembersInfo(Assembly assembly)
		{
			Contract.Requires<ArgumentNullException>(assembly != null, "assembly != null");

			var types = assembly.GetTypes();
			var exceptions = new List<Exception>();
			var memberInfos = new List<MemberInfo>();

			memberInfos.AddRange(
				types.Where(type => type.IsClass && Attribute.IsDefined(type, typeof(CodeAuthorInfoAttribute))));

			foreach (var type in types)
			{
				try
				{
					memberInfos.AddRange(
						type.GetMethods()
							.Where(methodInfo => Attribute.IsDefined(methodInfo, typeof(CodeAuthorInfoAttribute))));
				}
				catch (Exception exception)
				{
					exceptions.Add(new MemberRetrievingException("There is an error during getting member.", type,
						exception));
				}
			}

			if (exceptions.Any())
			{
				throw new AggregateException("There are multiple errors during getting members from the assembly.",
					exceptions);
			}

			return memberInfos;
		}

		private static Assembly LoadAssembly(string path)
		{
			Assembly assembly;

			try
			{
				assembly = Assembly.LoadFile(path);
			}
			catch (Exception exception)
			{
				throw new LoadCodeAuthorInfoException("Loading of code author information is failed.", path, exception);
			}

			return assembly;
		}
	}
}