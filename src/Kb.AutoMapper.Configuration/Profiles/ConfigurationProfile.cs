using AutoMapper;
using Kb.AutoMapper.Configuration.Exceptions;

namespace Kb.AutoMapper.Configuration.Profiles;

public class ConfigurationProfile : Profile
{
    public ConfigurationProfile(MappingConfig config)
        : base(!string.IsNullOrWhiteSpace(config.ProfileName)
              ? config.ProfileName 
              : nameof(ConfigurationProfile))
    {
        DefineMappingDefaults(config);
        CreateMemberMappings(config.Members);
    }

    private void DefineMappingDefaults(MappingConfig config)
    {
        AllowNullCollections = config.AllowNullCollections;
        AllowNullDestinationValues = config.AllowNullDestinationValues;

        if (config.DisableConstructorMapping)
            DisableConstructorMapping();

        foreach (var ignoreMember in config.GlobalIgnores)
        {
            AddGlobalIgnore(ignoreMember);
        }
    }

    private void CreateMemberMappings(IEnumerable<MappingMember> mappingMembers)
    {
        foreach (var member in mappingMembers)
        {
            var sourceType = Type.GetType(member.SourceType);
            var destinationType = Type.GetType(member.DestinationType);

            if (sourceType is null || destinationType is null)
                throw new InvalidMappingTypeException(sourceType ?? destinationType!);

            CreateMap(sourceType, destinationType, member.Mappings);
        }
    }

    private void CreateMap(Type sourceType, Type destinationType, IEnumerable<Mapping> mappings)
    {
        var mapExpression = CreateMap(sourceType, destinationType);

        foreach (var mapping in mappings)
        {
            var sourceProperty = GetPropertyByNameIgnoreCase(sourceType, mapping.Source);
            var destinationProperty = GetPropertyByNameIgnoreCase(destinationType, mapping.Destination);

            var converterType = Type.GetType(mapping.Converter)
                ?? throw new InvalidConverterTypeException(mapping.Converter);

            mapExpression.ForMember(destinationProperty, opt =>
            {
                if (IsValueConverter(converterType))
                    opt.ConvertUsing(converterType, sourceProperty);

                else if (IsMemberValueResolver(converterType))
                    opt.MapFrom(valueResolverType: converterType, sourceMemberName: sourceProperty);

                else
                    opt.MapFrom(sourceProperty);
            });
        }
    }

    private static string GetPropertyByNameIgnoreCase(Type type, string propertyName)
        => type.GetProperties()
                    .First(p => string.Equals(p.Name, propertyName, StringComparison.OrdinalIgnoreCase))!
                    .Name;

    private static bool IsValueConverter(Type? type)
        => type?.GetInterface(typeof(IValueConverter<,>).Name) is not null;

    private static bool IsMemberValueResolver(Type? type)
        => type?.GetInterface(typeof(IMemberValueResolver<,,,>).Name) is not null;
}
