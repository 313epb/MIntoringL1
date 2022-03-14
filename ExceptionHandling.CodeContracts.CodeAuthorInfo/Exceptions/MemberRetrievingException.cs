using System;

namespace ExceptionHandling.CodeContracts.CodeAuthorInfo.Exceptions
{
	/// <summary>
	/// Error occurs during retrieving members marked <see cref="CodeAuthorInfoAttribute" /> from the <see cref="SourceType" />
	/// .
	/// </summary>
	[Serializable]
	public class MemberRetrievingException : CodeAuthorInfoException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MemberRetrievingException" /> class.
		/// </summary>
		/// <param name="message">The message of the error.</param>
		/// <param name="sourceType">SourceType to retrieve <see cref="CodeAuthorInfoAttribute" /> from.</param>
		public MemberRetrievingException(string message, Type sourceType) : this(message, sourceType, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MemberRetrievingException" /> class.
		/// </summary>
		/// <param name="message">The message of the error.</param>
		/// <param name="sourceType">SourceType to retrieve <see cref="CodeAuthorInfoAttribute" /> from.</param>
		/// <param name="innerException">Inner exception.</param>
		/// <exception cref="ArgumentNullException" />
		public MemberRetrievingException(string message, Type sourceType, Exception innerException)
			: base(message, innerException)
		{
			if (sourceType == null)
			{
				throw new ArgumentNullException(nameof(sourceType));
			}

			SourceType = sourceType;
		}

		/// <summary>
		/// SourceType to retrieve <see cref="CodeAuthorInfoAttribute" /> from.
		/// </summary>
		public Type SourceType { get; }
	}
}