using System;

namespace Domain.Exceptions
{
    public class TruitjeException : Exception
    {
        public TruitjeException(string message): base(message)
        {
            
        }
    }
}