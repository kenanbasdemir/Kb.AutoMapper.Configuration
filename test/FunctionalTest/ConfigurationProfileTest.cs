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
    public void SuccessNewInstance()
    {
        var birthDate = new DateTime(2000, 1, 1);

        var source = new Source()
        {
            FirstName = "Kenan",
            BirthDate = birthDate,
        };

        Destination destination = _mapper.Map<Destination>(source);

        Assert.Equal("KENAN", destination.Name);
        Assert.Equal(birthDate.ToString("yyyy-MM-dd"), destination.DateOfBirth);
        Assert.Equal(DateTime.Now.Year - birthDate.Year, destination.Age);
    }

    [Fact]
    public void SuccessAlreadyInstance()
    {
        var birthDate = new DateTime(2000, 1, 1);

        var source = new Source()
        {
            Id = 1,
            FirstName = "Kenan",
            BirthDate = birthDate,
        };

        Destination destination = new Destination()
        {
            Id = 2
        };

        _mapper.Map(source, destination);

        Assert.Equal(2, destination.Id);
        Assert.Equal("KENAN", destination.Name);
        Assert.Equal(birthDate.ToString("yyyy-MM-dd"), destination.DateOfBirth);
        Assert.Equal(DateTime.Now.Year - birthDate.Year, destination.Age);
    }
}
