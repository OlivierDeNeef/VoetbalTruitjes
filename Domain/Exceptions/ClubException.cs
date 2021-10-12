using System;

namespace Domain.Exceptions
{
    public class ClubException : Exception
    {
        public ClubException(string message): base(message)
        {
            
        }
    }
}