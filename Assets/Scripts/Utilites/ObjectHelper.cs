namespace Utilites
{
    public static class ObjectHelper
    {
        public static T Cast<T>(this object objectToCast)  where T : class
        {
            return (T)objectToCast;
        }
    }
}