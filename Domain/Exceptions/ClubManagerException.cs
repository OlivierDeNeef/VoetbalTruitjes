using System;

namespace Domain.Exceptions
{
    [Serializable]
    public class ClubManagerException : Exception
    {
    

        public ClubManagerException()
        {
        }

        public ClubManagerException(string message) : base(message)
        {
        }

        public ClubManagerException(string message, Exception inner) : base(message, inner)
        {
        }


    }
}