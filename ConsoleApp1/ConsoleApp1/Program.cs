using ExtensionMethods;

namespace ConsoleApp1
{
    /// <summary>
    /// this is my program
    /// </summary>
    internal partial class Program
    {
        public static void Main()
        {
            dynamic x;
            x = 100;
            Console.WriteLine(x);
        }

        public static class MyStatic
        {
            public static int Increase(int myNumber)
            {
                return myNumber++;
            }
        };
    }
}