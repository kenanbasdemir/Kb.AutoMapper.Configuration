# Kb.AutoMapper.Configuration

It is an unofficial mapping structure based on dynamic configuration prepared for AutoMapper.

## Usage

```csharp

// Configuration json.
string configJson = "";

var mapperConfiguration = new MapperConfiguration(conf =>
  {
      MappingConfig config = JsonSerializer.Deserialize<MappingConfig>(configJson)!;
      conf.AddProfile(new ConfigurationProfile(config));
  });
  
var mapper = mapperConfiguration.CreateMapper();

var destination = mapper.Map<Destination>(new Source());

```
