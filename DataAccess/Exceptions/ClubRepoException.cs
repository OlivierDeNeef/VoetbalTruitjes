using System;

[Serializable]
public class ClubRepoException : Exception
{
    //
    // For guidelines regarding the creation of new exception types, see
    //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
    // and
    //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
    //

    public ClubRepoException()
    {
    }

    public ClubRepoException(string message) : base(message)
    {
    }

    public ClubRepoException(string message, Exception inner) : base(message, inner)
    {
    }


}