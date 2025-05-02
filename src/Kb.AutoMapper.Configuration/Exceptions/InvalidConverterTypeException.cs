namespace Kb.AutoMapper.Configuration.Exceptions
{
    public class InvalidConverterTypeException : Exception
    {
        public InvalidConverterTypeException(string type) : base($"{type} is invalid converter type.")
        {
        }
    }
}
