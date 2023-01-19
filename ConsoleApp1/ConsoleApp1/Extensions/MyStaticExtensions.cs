namespace ExtensionMethods
{
    public static class MyStaticExtensions
    {
        public static int Decrease(this int myNumber)
        {
            return myNumber--;
        }
    }
}