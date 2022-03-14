using System;
using System.Collections.Generic;
using ExceptionHandling.CodeContracts.CodeAuthorInfo.Exceptions;

namespace ExceptionHandling.CodeContracts.ConsoleApplication
{
	/// <summary>
	/// Returns an exception code by a given type of a exception.
	/// </summary>
	public static class ExceptionCodesResolver
	{
		private static readonly IDictionary<Type, int> ExceptionCodesDictionary = new Dictionary<Type, int>
		{
			{typeof(CodeAuthorInfoException), (int) ExceptionCode.CodeAuthorInfo},
			{typeof(MemberRetrievingException), (int) ExceptionCode.MemberRetrieving},
			{typeof(ExtractingCodeAuthorInfoException), (int) ExceptionCode.ExtractingCodeAuthorInfo},
			{typeof(LoadCodeAuthorInfoException), (int) ExceptionCode.LoadCodeAuthorInfo}
		};

		/// <summary>
		/// Returns exception code by the given <paramref name="exceptionType" />.
		/// </summary>
		/// <param name="exceptionType">Type of a exception.</param>
		/// <returns>Exception code.</returns>
		public static int GetExceptionCode(Type exceptionType)
		{
			if (ExceptionCodesDictionary.ContainsKey(exceptionType))
			{
				return ExceptionCodesDictionary[exceptionType];
			}

			return -1;
		}
	}
}