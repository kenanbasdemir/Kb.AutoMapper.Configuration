using AutoMapper;

namespace FunctionalTest.Converters;

public class DateFormatConverter : IValueConverter<DateTime, string>
{
    public string Convert(DateTime sourceMember, ResolutionContext context)
    {
        return sourceMember.ToString("yyyy-MM-dd");
    }
}