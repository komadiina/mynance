namespace mynance.src.utilities
{
	public static class DateTimeUtils
	{
		public static String GetTodaysDate() => DateTime.Now.ToString("dd/MM/yyyy");

		/// <summary>
		/// Get the timestamp (long) of the next month's start time.
		/// </summary>
		/// <returns>Timestamp in millis</returns>
		public static long GetMonthExpiry()
		{
			DateTime now = DateTime.Now;
			DateTime firstDayOfNextMonth = new DateTime(now.Year, now.Month, 1).AddMonths(1);
			return ((DateTimeOffset)firstDayOfNextMonth).ToUnixTimeSeconds();
		}

		public static long GetCurrentTimestamp()
		{
			return DateTimeOffset.Now.ToUnixTimeSeconds();
		}
	}
}
