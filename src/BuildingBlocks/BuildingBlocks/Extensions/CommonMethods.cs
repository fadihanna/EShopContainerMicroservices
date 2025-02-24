namespace BuildingBlocks.Extensions
{
    public static class CommonMethods
    {
        public static DateTime UTCtoDateTime(long unixTimeStamp)
        {
            var datetimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            datetimeVal = datetimeVal.AddSeconds(unixTimeStamp).ToLocalTime();

            return datetimeVal;
        }
        public static string GetRandomString()
        {
            Random random = new Random();
            string chars = "ABCDEFGHIJKLMNOPRSTUVYZWX0123456789";
            return new string(Enumerable.Repeat(chars, 35).Select(n => n[new Random().Next(n.Length)]).ToArray());
        }
    }
}
