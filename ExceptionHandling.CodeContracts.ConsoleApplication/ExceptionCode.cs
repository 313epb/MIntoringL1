using ExceptionHandling.CodeContracts.CodeAuthorInfo.Exceptions;

namespace ExceptionHandling.CodeContracts.ConsoleApplication
{
	/// <summary>
	/// Exception codes.
	/// </summary>
	public enum ExceptionCode
	{
		/// <summary>
		/// Exception code for the <see cref="CodeAuthorInfoException" /> class.
		/// </summary>
		CodeAuthorInfo = 100,

		/// <summary>
		/// Exception code for the <see cref="ExtractingCodeAuthorInfoException" /> class.
		/// </summary>
		ExtractingCodeAuthorInfo = 101,

		/// <summary>
		/// Exception code for the <see cref="MemberRetrievingException" /> class.
		/// </summary>
		MemberRetrieving = 102,

		/// <summary>
		/// Exception code for the <see cref="LoadCodeAuthorInfoException" /> class.
		/// </summary>
		LoadCodeAuthorInfo = 103
	}
}