using DNTPersianUtils.Core;

namespace Application.Helpers;

public class DateTimeToPersianDateTimeConverter : ITypeConverter<DateTime, string>
{
    public string Convert(DateTime source, string destination, ResolutionContext context)
    {
        return source.ToPersianDateTextify();
    }
}
