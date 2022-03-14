using System;
using System.Globalization;
using System.Reflection;

namespace ExceptionHandling.CodeContracts.CodeAuthorInfo.Exceptions
{
	/// <summary>
	/// Exception that occurs during extracting <see cref="CodeAuthorInfoAttribute" /> information from the
	/// <see cref="MemberInfo" />.
	/// </summary>
	[Serializable]
	public class ExtractingCodeAuthorInfoException : CodeAuthorInfoException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ExtractingCodeAuthorInfoException" /> class.
		/// </summary>
		/// <param name="message">The message of the error.</param>
		/// <param name="memberInfo">Member info with the error extracting of.</param>
		public ExtractingCodeAuthorInfoException(string message, MemberInfo memberInfo)
			: this(message, memberInfo, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ExtractingCodeAuthorInfoException" /> class.
		/// </summary>
		/// <param name="message">The message of the error.</param>
		/// <param name="memberInfo">Member info with the error extracting of.</param>
		/// <param name="innerException">Inner exception.</param>
		/// <exception cref="ArgumentNullException" />
		public ExtractingCodeAuthorInfoException(string message, MemberInfo memberInfo, Exception innerException)
			: base(message, innerException)
		{
			if (memberInfo == null)
			{
				throw new ArgumentNullException(nameof(memberInfo));
			}

			MemberInfo = memberInfo;
		}

		/// <summary>
		/// Member info with the error extracting of.
		/// </summary>
		public MemberInfo MemberInfo { get; }

		/// <summary>Creates and returns a string representation of the current exception.</summary>
		/// <returns>A string representation of the current exception.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///     <IPermission
		///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
		///         version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "Error during extracting code author info from member {0}.",
				MemberInfo.Name);
		}
	}
}