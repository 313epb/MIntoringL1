using System;
using System.Reflection;

namespace Reflection.Task2.Providers
{
	/// <summary>
	/// Provides <see cref="EventInfo" /> information.
	/// </summary>
	public static class EventInfoProvider
	{
		/// <summary>
		/// Retrieves <see cref="EventInfo" /> information from an <paramref name="type" /> by specified
		/// <paramref name="eventName" />.
		/// </summary>
		/// <param name="type">An type to retrieve <see cref="EventInfo" /> information from.</param>
		/// <param name="eventName">An event name to retrieve information from.</param>
		/// <returns><see cref="EventInfo" /> information by specified <paramref name="eventName" />.</returns>
		/// <exception cref="ArgumentNullException" />
		/// <exception cref="ArgumentException" />
		/// <exception cref="EventInfoRetrievingException" />
		public static EventInfo GetEventInfo(Type type, string eventName)
		{
			if (type == null)
			{
				throw new ArgumentNullException(nameof(type));
			}

			if (string.IsNullOrWhiteSpace(eventName))
			{
				throw new ArgumentException("Event name mustn't be empty or contain white spaces.", nameof(eventName));
			}

			EventInfo eventInfo;

			try
			{
				eventInfo = type.GetEvent(eventName);
			}
			catch (Exception exception)
			{
				throw new EventInfoRetrievingException(
					"Error while retrieving event information from the specified type.", eventName, type,
					exception);
			}

			if (eventInfo == null)
			{
				throw new EventInfoRetrievingException("Retrieved event information is null.", eventName, type);
			}

			return eventInfo;
		}
	}
}