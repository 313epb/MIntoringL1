using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reflection.Task2.Providers;

namespace Reflection.Task2.Tests.Providers
{
	[TestClass]
	public class EventInfoProviderTests
	{
		/// <summary>
		/// Scenario Gets <see cref="EventInfo" /> information from the <see cref="EventInfoProvider.GetEventInfo" /> with invalid
		/// parameter
		/// Given Invalid parameter of the extracting type
		/// When Calls the <see cref="EventInfoProvider.GetEventInfo" /> method
		/// Then Throws an exception of the <see cref="ArgumentNullException" /> class
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GetEventInfo_InvalidType()
		{
			//Arrange
			Type type = null;
			const string eventName = "EventName";

			//Act
			var eventInfo = EventInfoProvider.GetEventInfo(type, eventName);

			//Assert is handled by exception
		}

		/// <summary>
		/// Scenario Gets <see cref="EventInfo" /> information from the <see cref="EventInfoProvider.GetEventInfo" /> with invalid
		/// parameter
		/// Given Invalid parameter of the event name
		/// When Calls the <see cref="EventInfoProvider.GetEventInfo" /> method
		/// Then Throws an exception of the <see cref="ArgumentException" /> class
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void GetEventInfo_InvalidEventName()
		{
			//Arrange
			var type = typeof(string);
			string eventName = string.Empty;

			//Act
			var eventInfo = EventInfoProvider.GetEventInfo(type, eventName);

			//Assert is handled by exception
		}

		/// <summary>
		/// Scenario Gets <see cref="EventInfo" /> information from the <see cref="EventInfoProvider.GetEventInfo" /> of unexisting
		/// event
		/// Given Unexisting event name
		/// When Calls the <see cref="EventInfoProvider.GetEventInfo" /> method
		/// Then Throws an exception of the <see cref="EventInfoRetrievingException" />
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(EventInfoRetrievingException))]
		public void GetEventInfo_ThrowEventInfoRetrievingException()
		{
			//Arrange
			var type = typeof(string);
			const string eventName = "EventName";

			//Act
			var eventInfo = EventInfoProvider.GetEventInfo(type, eventName);

			//Assert is handled by exception
		}

		/// <summary>
		/// Scenario Gets <see cref="EventInfo" /> information from the <see cref="EventInfoProvider.GetEventInfo" /> of existing
		/// event
		/// Given Correct type and event name
		/// When Calls the <see cref="EventInfoProvider.GetEventInfo" /> method
		/// Then Returned event info is not null
		/// </summary>
		[TestMethod]
		public void GetEventInfo_ReturnEventInfo()
		{
			//Arrange
			var type = typeof(EventContainingClass);
			const string eventName = nameof(EventContainingClass.Event);

			//Act
			var eventInfo = EventInfoProvider.GetEventInfo(type, eventName);

			//Assert
			Assert.IsTrue(eventInfo != null);
		}
	}
}