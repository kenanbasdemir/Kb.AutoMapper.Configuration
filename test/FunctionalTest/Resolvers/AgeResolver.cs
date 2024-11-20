using AutoMapper;
using FunctionalTest.Models;

namespace FunctionalTest.Resolvers;

public class AgeResolver : IMemberValueResolver<Source, Destination, DateTime, int>
{
    public int Resolve(Source source, Destination destination, DateTime sourceMember, int destMember, ResolutionContext context)
    {
        return DateTime.Now.Year - sourceMember.Year;
    }
}
