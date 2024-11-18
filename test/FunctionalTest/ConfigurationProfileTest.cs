using System.Text.Json;
using AutoMapper;
using FunctionalTest.Models;
using Kb.AutoMapper.Configuration;
using Kb.AutoMapper.Configuration.Profiles;

namespace FunctionalTest;

public class ConfigurationProfileTest
{
    private readonly IMapper _mapper;

    public ConfigurationProfileTest()
    {
        var configPath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, "config.json");
        var configJson = File.ReadAllText(configPath);

        var mapperConfiguration = new MapperConfiguration(conf =>
        {
            MappingConfig config = JsonSerializer.Deserialize<MappingConfig>(configJson)!;
            conf.AddProfile(new ConfigurationProfile(config));
        });

        _mapper = mapperConfiguration.CreateMapper();
    }

    [Fact]
    public void Success()
    {
        var source = new Source()
        {
            FirstName = "Kenan",
            BirthDate = DateTime.Now,
        };

        Destination destination = _mapper.Map<Destination>(source);

        Assert.Equal("KENAN", destination.Name);
        Assert.Equal(DateTime.Now.ToString("yyyy-MM-dd"), destination.DateOfBirth);
    }
}
