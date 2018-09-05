namespace Domain.Extensions
{
    public static class EnumExtensions
    {
        internal static Status Next(this Status currentStatus)
        {
            return currentStatus + 1;
        }
    }
}
