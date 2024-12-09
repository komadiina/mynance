using mynance.src.auth;
using mynance.src.localization;
using mynance.src.models.db;
using mynance.src.models.db.contexts;
using mynance.src.styles;
using mynance.src.utilities;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace mynance.src.navigation.pages.controllers
{
	public class UserDashboardController
	{
		private Budget budget;
		//public Budget Budget
		//{
		//	get
		//	{
		//		BudgetContext budgetCtx = new();
		//		return budgetCtx.Budgets.Where(b => b.Username == AuthGate.CurrentUser.Username).FirstOrDefault();
		//	}
		//	set { budget = value; }
		//}
		public Budget Budget { get; set; }

		public Currency Currency { get; private set; }
		internal IList<ExpenseCategory> Expenses { get; private set; } = [];
		public IList<AssignedBudget> AssignedBudgets { get; set; } = [];

		public ObservableCollection<double> CategoryBills = new(), SpendingBills = new([0.0]),
			CategoryLoans = new(), SpendingLoans = new([0.0]),
			CategorySubscriptions = new(), SpendingSubscriptions = new([0.0]),
			CategoryGroceries = new(), SpendingGroceries = new([0.0]),
			CategoryOther = new(), SpendingOther = new([0.0]);
		public static int SelectedCategoryID = 1;

		public UserDashboardController()
		{
			// fetch user budget
			BudgetContext budgetCtx = new();
			try
			{
				Budget = budgetCtx.Budgets.Where(b =>
						b.Username == AuthGate.CurrentUser.Username &&
						b.ValidUntil > DateTimeUtils.GetCurrentTimestamp()).FirstOrDefault();
			}
			catch (Exception ex)
			{
				Budget = new()
				{
					ValidUntil = DateTimeUtils.GetMonthExpiry(),
					Username = AuthGate.CurrentUser.Username,
					Amount = 0.0
				};
			}

			UserPreferenceContext userPreferenceCtx = new();
			UserPreference userPreference = userPreferenceCtx.UserPreferences
				.Where(pref => pref.Username == AuthGate.CurrentUser.Username).FirstOrDefault();

			if (userPreference != null)
			{
				CurrencyContext currencyCtx = new();
				Currency = currencyCtx.Currencies.Where(c => c.NumericCode == userPreference.CurrencyID).FirstOrDefault();

				if (Currency == null)
				{
					Currency = currencyCtx.Currencies.FirstOrDefault();
					userPreference.CurrencyID = Currency.NumericCode;

					userPreferenceCtx.SaveChanges();
				}
			}
			else
			{
				// no preference set, load default
				userPreference = new()
				{
					LocaleName = LocaleHandler.Instance.CurrentLocale,
					UseDarkTheme = ThemeHandler.IsDarkMode,
					Username = AuthGate.CurrentUser.Username,
					CurrencyID = 977 // BAM
				};
			}

			InitializeAssignedBudgets();
			AnalyzeSpendings();
		}

		public void AnalyzeSpendings()
		{
			PaymentsContext ctx = new();
			Dictionary<int, double> totalSpent = [];
			foreach (var payment in ctx.Payments.AsQueryable())
			{
				if (payment.Username == AuthGate.CurrentUser.Username && payment.Outgoing)
				{
					switch (payment.AssignedBudgetID)
					{
						case 1: // Bills
							SpendingBills[0] += payment.Amount;
							break;
						case 2: // Loans
							SpendingLoans[0] += payment.Amount;
							break;
						case 3: // Subscriptions
							SpendingSubscriptions[0] += payment.Amount;
							break;
						case 4: // Groceries
							SpendingGroceries[0] += payment.Amount;
							break;
						case 5: // Other
							SpendingOther[0] += payment.Amount;
							break;
						default: break;
					}
				}
			}

			// aj samo da zavrsim sunce ti poljubim
			LandingUserViewModel.BillsTotal = SpendingBills[0];
			LandingUserViewModel.LoansTotal = SpendingLoans[0];
			LandingUserViewModel.SubscriptionsTotal = SpendingSubscriptions[0];
			LandingUserViewModel.GroceriesTotal = SpendingGroceries[0];
			LandingUserViewModel.OtherTotal = SpendingOther[0];
		}

		public Budget GetBudget()
		{
			BudgetContext budgetCtx = new();
			try
			{
				Budget = budgetCtx.Budgets.Where(b =>
						b.Username == AuthGate.CurrentUser.Username &&
						b.ValidUntil > DateTimeUtils.GetCurrentTimestamp()).FirstOrDefault();
			}
			catch (Exception ex)
			{
				Budget = new()
				{
					ValidUntil = DateTimeUtils.GetMonthExpiry(),
					Username = AuthGate.CurrentUser.Username,
					Amount = 0.0
				};
			}

			return Budget;
		}

		public void InitializeAssignedBudgets()
		{
			BudgetContext budgetCtx = new();
			AssignedBudgetContext assignedBudgetCtx = new();
			List<AssignedBudget> assignedBudgets = [];

			Budget currentMonth = budgetCtx.Budgets.Where(b => b.Username == AuthGate.CurrentUser.Username).FirstOrDefault();
			if (currentMonth != null)
			{
				assignedBudgets = assignedBudgetCtx.AssignedBudgets
					.Where(ab => ab.BudgetID == currentMonth.ID).ToList();

				if (assignedBudgets.Count() == 0)
				{
					assignedBudgets = GenerateEmptyAssignedBudgets(currentMonth.ID);
					assignedBudgets.ForEach(ab => assignedBudgetCtx.AssignedBudgets.Add(ab));
					assignedBudgetCtx.SaveChanges();
				}
			}
			else
			{
				budgetCtx.Add(GenerateEmptyBudget());
				budgetCtx.SaveChanges();

				assignedBudgetCtx.Add(GenerateEmptyAssignedBudgets(budgetCtx.Budgets.Last().ID));
				assignedBudgetCtx.SaveChanges();
			}

			AssignedBudgets = assignedBudgets;

			foreach (var item in AssignedBudgets)
			{
				Trace.WriteLine(item.CategoryID + ":" + item.BudgetID + ":" + item.Amount);
				switch (item.CategoryID)
				{
					case 1: CategoryBills.Add(item.Amount); break;
					case 2: CategoryGroceries.Add(item.Amount); break;
					case 3: CategorySubscriptions.Add(item.Amount); break;
					case 4: CategoryLoans.Add(item.Amount); break;
					case 5: CategoryOther.Add(item.Amount); break;
					default: break;
				}
			}
		}

		public void UpdateBudget(int categoryID, double amount)
		{
			switch (categoryID)
			{
				case 1: CategoryBills[0] = amount; break;
				case 2: CategoryGroceries[0] = amount; break;
				case 3: CategorySubscriptions[0] = amount; break;
				case 4: CategoryLoans[0] = amount; break;
				case 5: CategoryOther[0] = amount; break;
				default: break;
			}
		}

		public static Budget GenerateEmptyBudget()
		{
			return new Budget
			{
				ValidUntil = DateTimeUtils.GetMonthExpiry(),
				Username = AuthGate.CurrentUser.Username,
				Amount = 0.0
			};
		}

		public static List<AssignedBudget> GenerateEmptyAssignedBudgets(int currentMonthBudgetID)
		{
			return [
				new AssignedBudget { BudgetID = currentMonthBudgetID, Amount = 0.00, CategoryID = 1 },
				new AssignedBudget { BudgetID = currentMonthBudgetID, Amount = 0.00, CategoryID = 2 },
				new AssignedBudget { BudgetID = currentMonthBudgetID, Amount = 0.00, CategoryID = 3 },
				new AssignedBudget { BudgetID = currentMonthBudgetID, Amount = 0.00, CategoryID = 4 },
				new AssignedBudget { BudgetID = currentMonthBudgetID, Amount = 0.00, CategoryID = 5 },
			];
		}

		// calculate usages of each budget (to be displayed in a bar chart on ui)
		private void CalculateBudgetUsages()
		{
			// TODO
		}
	}
}
