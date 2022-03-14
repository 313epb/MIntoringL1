using System;
using Reflection.Task2.Lib;

namespace Reflection.Task2.ConsoleApplication
{
    internal class Program
    {
        private static void Main()
        {
            var type = typeof(SomeClass);
            var someClassInstance = Activator.CreateInstance(type);
            var someClass = someClassInstance as SomeClass;
            var eventInfo = type.GetEvent(nameof(SomeClass.Handler));
            var delegateType = eventInfo.EventHandlerType;
            var methodToExecute = type.GetMethod(nameof(SomeClass.WriteFromMethod));
            var delegateInstance = Delegate.CreateDelegate(delegateType, methodToExecute);

            var addMethod = eventInfo.GetAddMethod();
            object[] handlers = {delegateInstance};

            addMethod.Invoke(someClass, handlers);
            
            someClass?.Show();

            Console.ReadLine();
        }
    }
}