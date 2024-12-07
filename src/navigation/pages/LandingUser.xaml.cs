using System.Windows.Controls;
using System.Windows.Input;

namespace mynance.src.navigation.pages
{
	/// <summary>
	/// Interaction logic for LandingUser.xaml
	/// </summary>
	public partial class LandingUser : Page
	{
		public LandingUser()
		{
			InitializeComponent();

			lblBalanceAmount.Content = String.Format(
				" {0:.00} {1}",
				LandingUserViewModel.Controller.Budget.Amount,
				LandingUserViewModel.Controller.Currency.AlphabeticCode
			);

			DataContext = new LandingUserViewModel();
		}

		private void tbEditBudget_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				((LandingUserViewModel)this.DataContext).BudgetValue = tbEditBudget.Text;
			}
		}
	}
}
