using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace Reflection.Task2.Providers
{
	/// <summary>
	/// The exception that is thrown when there are errors while retrieving <see cref="EventInfo" /> or retrieved
	/// <see cref="EventInfo" /> is <code>null</code>.
	/// </summary>
	[Serializable]
	public class EventInfoRetrievingException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EventInfoRetrievingException" /> class.
		/// </summary>
		public EventInfoRetrievingException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="EventInfoRetrievingException" /> class.
		/// </summary>
		/// <param name="message">An error message.</param>
		public EventInfoRetrievingException(string message) : base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="EventInfoRetrievingException" /> class.
		/// </summary>
		/// <param name="message">An error message.</param>
		/// <param name="innerException">Inner exception of an error.</param>
		public EventInfoRetrievingException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="EventInfoRetrievingException" /> class.
		/// </summary>
		/// <param name="message">An error message.</param>
		/// <param name="eventName">The event name.</param>
		/// <param name="instanceType">The type of the instance.</param>
		public EventInfoRetrievingException(string message, string eventName, Type instanceType)
			: this(message, eventName, instanceType, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="EventInfoRetrievingException" /> class.
		/// </summary>
		/// <param name="message">An error message.</param>
		/// <param name="eventName">The event name.</param>
		/// <param name="instanceType">The type of the instance.</param>
		/// <param name="innerException">Inner exception of an error.</param>
		public EventInfoRetrievingException(string message, string eventName, Type instanceType,
			Exception innerException) : base(message, innerException)
		{
			EventName = eventName;
			InstanceType = instanceType;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="EventInfoRetrievingException" /> class.
		/// </summary>
		/// <param name="info">Serialization information.</param>
		/// <param name="context">Context of the serialized stream.</param>
		protected EventInfoRetrievingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>
		/// Event name.
		/// </summary>
		public string EventName { get; }

		/// <summary>
		/// Instance type retrieving <see cref="EventInfo" /> from.
		/// </summary>
		public Type InstanceType { get; }
	}
}