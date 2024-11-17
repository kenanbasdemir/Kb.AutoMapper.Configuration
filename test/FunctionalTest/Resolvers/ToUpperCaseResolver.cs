using AutoMapper;
using FunctionalTest.Models;

namespace FunctionalTest.Resolvers;

public class ToUpperCaseResolver : IMemberValueResolver<Source, Destination, string, string>
{
    public string Resolve(Source source, Destination destination, string sourceMember, string destMember, ResolutionContext context)
    {
        if (source.BirthDate.Year == 2024)
            return sourceMember.ToUpper();

        return sourceMember;
    }
}