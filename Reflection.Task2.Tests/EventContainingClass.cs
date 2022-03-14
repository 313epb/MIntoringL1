using System;

namespace Reflection.Task2.Tests
{
	public class EventContainingClass
	{
		public event EventHandler Event;

		public static void MethodHandler(object sender, EventArgs eventArgs)
		{
		}

		protected virtual void OnEvent()
		{
			Event?.Invoke(this, EventArgs.Empty);
		}

		public int GetEventHandlerCount()
		{
			return Event?.GetInvocationList().Length ?? 0;
		}
	}
}