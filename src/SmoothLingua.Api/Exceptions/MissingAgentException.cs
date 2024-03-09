namespace SmoothLingua.Api.Exceptions
{
    public class MissingAgentException : Exception
    {
        public MissingAgentException(int id)
            : base($"Missing agent {id}")
        { }
    }
}
