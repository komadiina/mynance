namespace mynance.src.models
{
	public class Expense
	{
		public string Name { get; set; }
		public String Description { get; set; }
		public Double Amount { get; set; }
		public DateTime From { get; set; }
		public DateTime To { get; set; }

		public Expense(List<int> idList)
		{

		}
	}
}
