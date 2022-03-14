using System;

namespace ExceptionHandling.CodeContracts.CodeAuthorInfo.Exceptions
{
	/// <summary>
	/// Base exception that occurs during retrieving <see cref="CodeAuthorInfoAttribute" /> inforamtion.
	/// </summary>
	[Serializable]
	public class CodeAuthorInfoException : Exception
	{
		/// <summary>
		/// Initializes a new instanse of the <see cref="CodeAuthorInfoException" /> class.
		/// </summary>
		public CodeAuthorInfoException()
		{
		}

		/// <summary>
		/// Initializes a new instanse of the <see cref="CodeAuthorInfoException" /> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public CodeAuthorInfoException(string message) : base(message)
		{
		}

		/// <summary>
		/// Initializes a new instanse of the <see cref="CodeAuthorInfoException" /> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="innerException">Inner exception.</param>
		public CodeAuthorInfoException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}