using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using mynance.src.localization;
using mynance.src.navigation.pages.controllers;
using System.ComponentModel;
using System.Diagnostics;

namespace mynance.src.navigation.pages
{
	public class LandingUserViewModel : INotifyPropertyChanged
	{
		public ISeries[] Series { get; set; } = [];
		public ISeries[] BudgetSeries { get; set; } = [
			new PieSeries<double>(300.0, 400.0, 999.9)
		];

		public string SideNavbar_Dashboard
		{
			get { return LocalizationProvider.GetLocalizedValue<string>("SideNavbar_Dashboard"); }
		}
		public string ProfilePage_BalanceStatus
		{
			get { return LocalizationProvider.GetLocalizedValue<string>("ProfilePage_BalanceStatus"); }
		}
		public string Dashboard_CurrentBudget
		{
			get { return LocalizationProvider.GetLocalizedValue<string>("Dashboard_CurrentBudget"); }
		}
		public string Dashboard_Spendings
		{
			get { return LocalizationProvider.GetLocalizedValue<string>("Dashboard_Spendings"); }
		}
		public string Dashboard_EditBudget
		{
			get { return LocalizationProvider.GetLocalizedValue<string>("Dashboard_EditBudget"); }
		}

		private string dashboard_BalanceAmount;
		public string Dashboard_BalanceAmount
		{
			set { dashboard_BalanceAmount = value; }
			get { return LocalizationProvider.GetLocalizedValue<string>("Dashboard_BalanceAmount"); }
		}

		private String budgetValue;
		public String BudgetValue
		{
			get { return budgetValue; }
			set
			{
				try
				{
					if (Double.TryParse(value, out double amount) == false)
					{
						Trace.WriteLine("Could not parse as double: " + value);
					}
					else
					{
						budgetValue = value;
						Controller.Budget.Amount = amount;
					}
				}
				catch (Exception)
				{
					budgetValue = "ERR";
				}
			}
		}

		public static UserDashboardController Controller = new();

		public LandingUserViewModel()
		{
			Dashboard_BalanceAmount = String.Format(
				" {0:.00} {1}",
				Controller.Budget.Amount,
				Controller.Currency.AlphabeticCode
			);

			BudgetValue = Controller.Budget.Amount.ToString();

			LocaleHandler.Instance.LocaleChanged += (sender, args) =>
			{
				OnPropertyChanged(nameof(SideNavbar_Dashboard));
				OnPropertyChanged(nameof(ProfilePage_BalanceStatus));
				OnPropertyChanged(nameof(Dashboard_CurrentBudget));
				OnPropertyChanged(nameof(Dashboard_Spendings));
				OnPropertyChanged(nameof(Dashboard_EditBudget));
				OnPropertyChanged(nameof(Dashboard_BalanceAmount));
			};
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
			Trace.WriteLine("Updated: " + propertyName);
		}
	}
}
