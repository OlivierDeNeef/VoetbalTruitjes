using System;

namespace Domain.Exceptions
{
    public class ClubSetException : Exception
    {
        public ClubSetException()
        {
            
        }
        public ClubSetException(string message): base(message)
        {
            
        }
        
    }
}