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
			DateTime today = DateTime.Now;
			DateTime firstDayOfNextMonth = new DateTime(today.Year, today.Month, 1).AddMonths(1);
			return firstDayOfNextMonth.Ticks / TimeSpan.TicksPerMillisecond;
		}

		public static long GetCurrentTimestamp()
		{
			return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
		}
	}
}
