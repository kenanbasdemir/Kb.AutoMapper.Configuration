namespace Kb.AutoMapper.Configuration.Exceptions
{
    public class InvalidMappingTypeException : Exception
    {
        public InvalidMappingTypeException(Type type) : base($"{type.FullName} is invalid mapping type.")
        {
        }
    }
}
