using System.Reflection;

namespace Reflection.Task2.Subscribers
{
	/// <summary>
	/// Subscribes a <see cref="MethodInfo" /> to an instance of the <see cref="object" />.
	/// </summary>
	public interface ISubscriber
	{
		/// <summary>
		/// Subscribes an <paramref name="method" /> to an <paramref name="instance" />.
		/// </summary>
		/// <param name="instance">An instance on which to subscribe an <paramref name="method" />.</param>
		/// <param name="method">A method to subscribe.</param>
		void Subscribe(object instance, MethodInfo method);
	}
}