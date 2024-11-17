namespace Kb.AutoMapper.Configuration;

public class MappingConfig
{
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