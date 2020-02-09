namespace Source.Core.Utils
{
    using Exceptions;

    public class ExceptionThrower
    {

        public static void Throws<T>()
            where T : CoreException, new()
        {
            throw new T();
        }
    }
}