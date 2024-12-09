using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Extensions;
using mynance.src.localization;
using mynance.src.models;
using mynance.src.navigation.pages.controllers;
using System.ComponentModel;
using System.Diagnostics;

namespace mynance.src.navigation.pages
{
	public class LandingUserViewModel : INotifyPropertyChanged
	{
		public ISeries[] Series { get; set; } = [];
		public ISeries[] BudgetSeries { get; set; } = [
			new PieSeries<double>{ Values = [0.0] }, // bills
			new PieSeries<double>{ Values = [0.0] }, // groceries
			new PieSeries<double>{ Values = [0.0] }, // subscriptions
			new PieSeries<double>{ Values = [0.0] }, // loans
			new PieSeries<double>{ Values = [0.0] }, // other
		];

		private double _billsBudget, _groceriesBudget, _subscriptionsBudget, _loansBudget, _otherBudget;

		public double BillsBudget { get { return _billsBudget; } set { _billsBudget = value; OnPropertyChanged(nameof(BillsBudget)); } }
		public double GroceriesBudget { get { return _groceriesBudget; } set { _groceriesBudget = value; OnPropertyChanged(nameof(GroceriesBudget)); } }
		public double SubscriptionsBudget { get { return _subscriptionsBudget; } set { _subscriptionsBudget = value; OnPropertyChanged(nameof(SubscriptionsBudget)); } }
		public double LoansBudget { get { return _loansBudget; } set { _loansBudget = value; OnPropertyChanged(nameof(LoansBudget)); } }
		public double OtherBudget { get { return _otherBudget; } set { _otherBudget = value; OnPropertyChanged(nameof(OtherBudget)); } }
		public static double BillsTotal, GroceriesTotal, SubscriptionsTotal, LoansTotal, OtherTotal;

		public IEnumerable<ISeries> BillsGauge { get; set; }
		public IEnumerable<ISeries> GroceriesGauge { get; set; }
		public IEnumerable<ISeries> SubscriptionsGauge { get; set; }
		public IEnumerable<ISeries> LoansGauge { get; set; }
		public IEnumerable<ISeries> OtherGauge { get; set; }

		public string CategoryBills
		{
			get { return LocalizationProvider.GetLocalizedValue<string>("CategoryBills"); }
		}
		public string CategoryLoans
		{
			get { return LocalizationProvider.GetLocalizedValue<string>("CategoryLoans"); }
		}
		public string CategorySubscriptions
		{
			get { return LocalizationProvider.GetLocalizedValue<string>("CategorySubscriptions"); }
		}
		public string CategoryOther
		{
			get { return LocalizationProvider.GetLocalizedValue<string>("CategoryOther"); }
		}
		public string CategoryGroceries
		{
			get { return LocalizationProvider.GetLocalizedValue<string>("CategoryGroceries"); }
		}
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

		public string Dashboard_EditButton
		{
			get { return LocalizationProvider.GetLocalizedValue<string>("Dashboard_EditButton"); }
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
						throw new Exception();
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

		private string editBudgetTextBox;
		public string EditBudgetTextBox
		{
			get { return editBudgetTextBox; }
			set { editBudgetTextBox = value; OnPropertyChanged(nameof(EditBudgetTextBox)); }
		}

		private String balanceStatusDisplay;
		public String BalanceStatusDisplay
		{
			get { return balanceStatusDisplay; }
			set { balanceStatusDisplay = value; OnPropertyChanged(nameof(BalanceStatusDisplay)); }
		}

		public static UserDashboardController Controller;

		public LandingUserViewModel()
		{
			Controller = new();
			BudgetValue = Controller.Budget.Amount.ToString();
			BalanceStatusDisplay = String.Format("{0:.00} {1}", Controller.Budget.Amount, Controller.Currency.AlphabeticCode);
			InitBudgets();

			BillsGauge = GaugeGenerator.BuildSolidGauge(
				new GaugeItem(
					BillsTotal,
					series =>
					{
						series.MaxRadialColumnWidth = 7;
						series.DataLabelsSize = 20;
					}
				));
			GroceriesGauge = GaugeGenerator.BuildSolidGauge(
				new GaugeItem(
					GroceriesTotal,
					series =>
					{
						series.MaxRadialColumnWidth = 7;
						series.DataLabelsSize = 20;
					}
				));
			SubscriptionsGauge = GaugeGenerator.BuildSolidGauge(
				new GaugeItem(
					SubscriptionsTotal,
					series =>
					{
						series.MaxRadialColumnWidth = 7;
						series.DataLabelsSize = 20;
					}
				));
			LoansGauge = GaugeGenerator.BuildSolidGauge(
				new GaugeItem(
					LoansTotal,
					series =>
					{
						series.MaxRadialColumnWidth = 7;
						series.DataLabelsSize = 20;
					}
				));
			OtherGauge = GaugeGenerator.BuildSolidGauge(
				new GaugeItem(
					OtherTotal,
					series =>
					{
						series.MaxRadialColumnWidth = 7;
						series.DataLabelsSize = 20;
					}
				));

			LocaleHandler.Instance.LocaleChanged += (sender, args) =>
			{
				OnPropertyChanged(nameof(SideNavbar_Dashboard));
				OnPropertyChanged(nameof(ProfilePage_BalanceStatus));
				OnPropertyChanged(nameof(Dashboard_CurrentBudget));
				OnPropertyChanged(nameof(Dashboard_Spendings));
				OnPropertyChanged(nameof(Dashboard_EditBudget));
				OnPropertyChanged(nameof(CategoryBills));
				OnPropertyChanged(nameof(CategoryGroceries));
				OnPropertyChanged(nameof(CategorySubscriptions));
				OnPropertyChanged(nameof(CategoryLoans));
				OnPropertyChanged(nameof(CategoryOther));
				OnPropertyChanged(nameof(Dashboard_EditButton));
			};
		}

		public Boolean TryEditBudget(string value, int selectedCategoryID)
		{
			Trace.WriteLine("Selected: " + selectedCategoryID);

			if (selectedCategoryID == 0)
			{
				Trace.WriteLine("Updating monthly budget: " + Double.Parse(value));
				Controller.Budget.Amount = Double.Parse(value);
				BalanceStatusDisplay = String.Format("{0:.00} {1}", Controller.Budget.Amount, Controller.Currency.AlphabeticCode);
				UserState.Instance.Budget = Controller.Budget;
				UserState.Instance.Persist();

				return true;
			}

			// Create a temporary budget for this category with the BudgetValue amount, and see if there is any limit-exceeding
			double sum = 0.0;
			foreach (var budget in Controller.AssignedBudgets)
			{
				if (budget.CategoryID == selectedCategoryID) { sum += Double.Parse(value); }
				else sum += budget.Amount;
			}

			if (sum <= Controller.Budget.Amount)
			{
				foreach (var ab in Controller.AssignedBudgets)
					if (ab.CategoryID == selectedCategoryID)
					{
						ab.Amount = Double.Parse(value);
						Trace.WriteLine("New amount: " + ab.Amount + ":" + ab.CategoryID);
						switch (ab.CategoryID)
						{
							case 1: BillsBudget = ab.Amount; break;
							case 2: GroceriesBudget = ab.Amount; break;
							case 3: SubscriptionsBudget = ab.Amount; break;
							case 4: LoansBudget = ab.Amount; break;
							case 5: OtherBudget = ab.Amount; break;
							default: break;
						}
						break;
					}

				UserState.Instance.AssignedBudgets = Controller.AssignedBudgets.ToList();
				Controller.AssignedBudgets.ToList().ForEach(budget => Controller.UpdateBudget(budget.CategoryID, budget.Amount));
				UserState.Instance.Persist();

				return true;
			}

			return false;
		}
		private void InitBudgets()
		{
			// joj fuj
			BudgetSeries[0].Values = Controller.CategoryBills;
			BillsBudget = Controller.CategoryBills[0];
			OnPropertyChanged(nameof(BillsBudget));
			Trace.WriteLine(BillsBudget);
			BudgetSeries[0].Name = CategoryBills;

			BudgetSeries[1].Values = Controller.CategoryGroceries;
			GroceriesBudget = Controller.CategoryGroceries[0];
			OnPropertyChanged(nameof(GroceriesBudget));
			Trace.WriteLine(GroceriesBudget);
			BudgetSeries[1].Name = CategoryGroceries;

			BudgetSeries[2].Values = Controller.CategorySubscriptions;
			SubscriptionsBudget = Controller.CategorySubscriptions[0];
			OnPropertyChanged(nameof(SubscriptionsBudget));
			Trace.WriteLine(SubscriptionsBudget);
			BudgetSeries[2].Name = CategorySubscriptions;

			BudgetSeries[3].Values = Controller.CategoryLoans;
			LoansBudget = Controller.CategoryLoans[0];
			OnPropertyChanged(nameof(LoansBudget));
			Trace.WriteLine(LoansBudget);
			BudgetSeries[3].Name = CategoryLoans;

			BudgetSeries[4].Values = Controller.CategoryOther;
			OtherBudget = Controller.CategoryOther[0];
			OnPropertyChanged(nameof(OtherBudget));
			Trace.WriteLine(OtherBudget);
			BudgetSeries[4].Name = CategoryOther;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string propertyName)
		{
			Trace.WriteLine("Updated: " + propertyName);
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
