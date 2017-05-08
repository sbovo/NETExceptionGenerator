using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NETFullConsoleExceptionGenerator
{
    class Program
    {
        private static ConsoleColor ConsoleDefaultForegroundColor = Console.ForegroundColor;

        static void Main(string[] args)
        {
            while (true)
            {
                Thread.Sleep(10000);
                TryAndCatch(typeof(StackOverflowException));
                Thread.Sleep(1000);
                TryAndCatch(typeof(OutOfMemoryException));
            }
        }

        private static void TryAndCatch(Type exceptionType)
        {
            try
            {
                RaiseNewException(exceptionType);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"{ex.GetType().ToString()} was handled by code.");
                Console.ForegroundColor = ConsoleDefaultForegroundColor;
            }
        }

        private static void RaiseNewException(Type exceptionType)
        {
            var message = $"{Assembly.GetExecutingAssembly().GetName().Name} generated an {exceptionType} at {DateTime.Now.ToLongTimeString()}.";

            System.Exception ex = (System.Exception)Activator.CreateInstance(exceptionType);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleDefaultForegroundColor;

            throw ex;
        }
    }
}
