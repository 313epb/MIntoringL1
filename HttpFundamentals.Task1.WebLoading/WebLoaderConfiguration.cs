using System;
using System.Collections.Generic;

namespace HttpFundamentals.Task1.WebLoading
{
	/// <summary>
	/// Configuration options for the <see cref="WebLoader" /> class.
	/// </summary>
	public class WebLoaderConfiguration
	{
		private int _depth;
		private string _destinationPath;
		private string _url;

		/// <summary>
		/// Unified resource locator of the required web resource.
		/// </summary>
		/// <exception cref="ArgumentException"></exception>
		public string Url
		{
			get { return _url; }
			set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					throw new ArgumentException("The specified URL must not be empty or contain white spaces.", nameof(value));
				}

				_url = value;
			}
		}

		/// <summary>
		/// Destination path to save web resource.
		/// </summary>
		/// <exception cref="ArgumentException"></exception>
		public string DestinationPath
		{
			get { return _destinationPath; }
			set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					throw new ArgumentException("The specified path must not be empty or contain white spaces", nameof(value));
				}

				_destinationPath = value;
			}
		}

		/// <summary>
		/// Depth of the loaded resource.
		/// </summary>
		public int Depth
		{
			get { return _depth; }
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException(nameof(value), "Depth must be greater than zero.");
				}

				_depth = value;
			}
		}

		/// <summary>
		/// Collection of the restricted extensions.
		/// </summary>
		public ISet<string> RestrictedExtensions { get; set; }

		/// <summary>
		/// Determines whether trace information enabled or not.
		/// </summary>
		public bool IsTraceEnabled { get; set; }
	}
}