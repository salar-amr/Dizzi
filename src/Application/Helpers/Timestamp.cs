namespace Application.Helpers;

public sealed class Timestamp
{
    public static Timestamp FromDateTime(DateTime dateTime)
    {
        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        TimeSpan diff = dateTime.ToUniversalTime() - origin;
        var secounds = Convert.ToInt64(diff.TotalSeconds);
        return new Timestamp() { Seconds = secounds };
    }

    public long Seconds { get; set; }

    public long Nanos { get; set; }
}
