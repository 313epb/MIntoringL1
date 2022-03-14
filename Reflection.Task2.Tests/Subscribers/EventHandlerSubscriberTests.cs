using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reflection.Task2.Subscribers;

namespace Reflection.Task2.Tests.Subscribers
{
	[TestClass]
	public class EventHandlerSubscriberTests
	{
		/// <summary>
		/// Scenario Creates a new instance of the <see cref="EventHandlerSubscriber" /> class with invalid event name
		/// Given Invalid event name
		/// When Calls the constructor of the <see cref="EventHandlerSubscriber" />
		/// Then Throws an exception of the <see cref="ArgumentException" /> class
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void EventHandlerSubscriber_InvalidEventName()
		{
			//Arrange
			string eventName = string.Empty;

			//Act
			var eventHandlerSubscriber = new EventHandlerSubscriber(eventName);

			//Assert is handled by exception
		}

		/// <summary>
		/// Scenario Creates a new instance of the <see cref="EventHandlerSubscriber" /> class with valid event name
		/// Given Valid event name
		/// When Calls the constructor of the <see cref="EventHandlerSubscriber" />
		/// Then Created instance of the <see cref="EventHandlerSubscriber" /> class is not null
		/// </summary>
		[TestMethod]
		public void EventHandlerSubscriber_ValidEventName()
		{
			//Arrange
			const string eventName = "Closed";

			//Act
			var eventHandlerSubscriber = new EventHandlerSubscriber(eventName);

			//Assert
			Assert.IsTrue(eventHandlerSubscriber != null);
		}

		/// <summary>
		/// Scenario Subscribes with the <see cref="EventHandlerSubscriber.Subscribe" /> to the null instance
		/// Given Null instance
		/// When Calls <see cref="EventHandlerSubscriber.Subscribe" />
		/// Then Throws an exception of the <see cref="ArgumentNullException" /> class
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Subscribe_InvalidInstance()
		{
			//Arrange
			object instance = null;
			MethodInfo method = null;
			const string eventName = "Closed";
			var eventHandlerSubscriber = new EventHandlerSubscriber(eventName);

			//Act
			eventHandlerSubscriber.Subscribe(instance, method);

			//Assert is handled by exception
		}

		/// <summary>
		/// Scenario Subscribes with the <see cref="EventHandlerSubscriber.Subscribe" /> to the instance with null method
		/// Given Null method
		/// When Calls <see cref="EventHandlerSubscriber.Subscribe" />
		/// Then Throws an exception of the <see cref="ArgumentNullException" /> class
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Subscribe_InvalidMethod()
		{
			//Arrange
			var instance = new object();
			MethodInfo method = null;
			const string eventName = "Closed";
			var eventHandlerSubscriber = new EventHandlerSubscriber(eventName);

			//Act
			eventHandlerSubscriber.Subscribe(instance, method);

			//Assert is handled by exception
		}

		/// <summary>
		/// Scenario Adds event handler to an empty list of event handlers of an class event
		/// Given An instance with empty list event handlers event
		/// When Calls <see cref="EventHandlerSubscriber.Subscribe" />
		/// Then Event handlers count must be greater than default value by one
		/// </summary>
		[TestMethod]
		public void Subscribe_AddEventHandlerToEvent()
		{
			//Arrange
			var instance = new EventContainingClass();
			int before = instance.GetEventHandlerCount();
			const string eventName = nameof(EventContainingClass.Event);
			var method = typeof(EventContainingClass).GetMethod(nameof(EventContainingClass.MethodHandler));
			var eventHandlerSubscriber = new EventHandlerSubscriber(eventName);

			//Act
			eventHandlerSubscriber.Subscribe(instance, method);
			int after = instance.GetEventHandlerCount();

			//Assert
			Assert.IsTrue(before < after && after - before == 1);
		}
	}
}