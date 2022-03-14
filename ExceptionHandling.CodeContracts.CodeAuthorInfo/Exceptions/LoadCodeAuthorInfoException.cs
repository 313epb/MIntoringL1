using System;

namespace ExceptionHandling.CodeContracts.CodeAuthorInfo.Exceptions
{
	/// <summary>
	/// Occurs when loading of <see cref="CodeAuthorInfoAttribute" /> information from an assembly is failed.
	/// </summary>
	public class LoadCodeAuthorInfoException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LoadCodeAuthorInfoException" /> class.
		/// </summary>
		/// <param name="message">A message of the error.</param>
		/// <param name="assemblyPath">An assembly path.</param>
		public LoadCodeAuthorInfoException(string message, string assemblyPath) : this(message, assemblyPath, null)
		{
			AssemblyPath = assemblyPath;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LoadCodeAuthorInfoException" /> class.
		/// </summary>
		/// <param name="message">A message of the error.</param>
		/// <param name="assemblyPath"></param>
		/// <param name="innerException"></param>
		public LoadCodeAuthorInfoException(string message, string assemblyPath, Exception innerException)
			: base(message, innerException)
		{
			AssemblyPath = assemblyPath;
		}

		/// <summary>
		/// Assembly path with <see cref="CodeAuthorInfoAttribute" /> information.
		/// </summary>
		public string AssemblyPath { get; private set; }
	}
}