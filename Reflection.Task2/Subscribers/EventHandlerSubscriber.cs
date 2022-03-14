using System;
using System.Reflection;
using Reflection.Task2.Providers;

namespace Reflection.Task2.Subscribers
{
	/// <summary>
	/// Subscribes a <see cref="MethodInfo" /> to an event of an instance.
	/// </summary>
	public class EventHandlerSubscriber : ISubscriber
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EventHandlerSubscriber" /> class.
		/// </summary>
		/// <param name="eventName">An event name to subscribe for.</param>
		/// <exception cref="ArgumentException"></exception>
		public EventHandlerSubscriber(string eventName)
		{
			if (string.IsNullOrWhiteSpace(eventName))
			{
				throw new ArgumentException("Event name mustn't be empty or contain white spaces.", nameof(eventName));
			}

			EventName = eventName;
		}

		/// <summary>
		/// An event name to subscribe for.
		/// </summary>
		public string EventName { get; }

		/// <summary>
		/// Subscribes an <paramref name="method" /> to the event with the name <see cref="EventName" /> of an
		/// <paramref name="instance" />.
		/// </summary>
		/// <param name="instance">An instance on which to subscribe an <paramref name="method" />.</param>
		/// <param name="method">A method to subscribe.</param>
		public void Subscribe(object instance, MethodInfo method)
		{
			if (instance == null)
			{
				throw new ArgumentNullException(nameof(instance));
			}

			if (method == null)
			{
				throw new ArgumentNullException(nameof(method));
			}

			var instanceType = instance.GetType();
			var eventInfo = EventInfoProvider.GetEventInfo(instanceType, EventName);
			var eventHandlerType = eventInfo.EventHandlerType;
			var delegateInstance = Delegate.CreateDelegate(eventHandlerType, null, method);

			eventInfo.AddMethod.Invoke(instance, new object[] {delegateInstance});
		}
	}
}