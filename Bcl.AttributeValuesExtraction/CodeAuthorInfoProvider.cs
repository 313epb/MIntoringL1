using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Bcl.AttributeValuesExtraction
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
			if (assembly == null)
			{
				throw new ArgumentNullException(nameof(assembly));
			}

			_infoElements = new List<CodeAuthorInfoAttribute>(ExtractInfo(assembly));
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodeAuthorInfoAttribute" /> class.
		/// </summary>
		/// <param name="path">
		/// Path of an assembly to extract information about classes and methods with the
		/// <see cref="CodeAuthorInfoAttribute" /> attribute.
		/// </param>
		public CodeAuthorInfoProvider(string path) : this(Assembly.Load(path))
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
		protected static IEnumerable<CodeAuthorInfoAttribute> ExtractInfo(Assembly assembly)
		{
			var infoElements = new List<CodeAuthorInfoAttribute>();
			var membersInfo = GetMembersInfo(assembly);

			foreach (var memberInfo in membersInfo)
			{
				infoElements.AddRange(
					Attribute.GetCustomAttributes(memberInfo, typeof(CodeAuthorInfoAttribute))
						.OfType<CodeAuthorInfoAttribute>());
			}

			return infoElements;
		}

		/// <summary>
		/// Gets necessary members info from the <paramref name="assembly" /> to extract information.
		/// </summary>
		/// <param name="assembly">An assembly to get members from.</param>
		/// <returns>Enumerator of the members info.</returns>
		protected static IEnumerable<MemberInfo> GetMembersInfo(Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException(nameof(assembly));
			}

			var types = assembly.GetTypes();
			var memberInfos = new List<MemberInfo>();

			memberInfos.AddRange(
				types.Where(type => type.IsClass && Attribute.IsDefined(type, typeof(CodeAuthorInfoAttribute))));

			foreach (var type in types)
			{
				memberInfos.AddRange(
					type.GetMethods()
						.Where(methodInfo => Attribute.IsDefined(methodInfo, typeof(CodeAuthorInfoAttribute))));
			}

			return memberInfos;
		}
	}
}