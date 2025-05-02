namespace Kb.AutoMapper.Configuration;

public class MappingConfig
{
    public string ProfileName { get; set; }
    public bool? AllowNullCollections { get; set; }
    public bool? AllowNullDestinationValues { get; set; }
    public bool DisableConstructorMapping { get; set; }
    public IEnumerable<string> GlobalIgnores { get; set; } = Enumerable.Empty<string>();
    public IEnumerable<MappingMember> Members { get; set; } = Enumerable.Empty<MappingMember>();
}

public class MappingMember
{
    public string SourceType { get; set; }
    public string DestinationType { get; set; }
    public IEnumerable<Mapping> Mappings { get; set; } = Enumerable.Empty<Mapping>();
}

public class Mapping
{
    public string Source { get; set; }
    public string Destination { get; set; }
    public string Converter { get; set; }
}