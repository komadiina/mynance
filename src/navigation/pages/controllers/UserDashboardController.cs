using mynance.src.auth;
using mynance.src.localization;
using mynance.src.models.db;
using mynance.src.models.db.contexts;
using mynance.src.styles;
using mynance.src.utilities;

namespace mynance.src.navigation.pages.controllers
{
	public class UserDashboardController
	{
		public UserDashboardController()
		{
			// fetch user budget
			BudgetContext budgetCtx = new();
			try
			{
				Budget = budgetCtx.Budgets.Where(b =>
						b.Username == AuthGate.CurrentUser.Username &&
						b.CategoryID == 1 &&
						b.ValidUntil > DateTimeUtils.GetCurrentTimestamp()).FirstOrDefault();
			}
			catch (Exception ex)
			{
				Budget = new()
				{
					ValidUntil = DateTimeUtils.GetMonthExpiry(),
					Username = AuthGate.CurrentUser.Username,
					CategoryID = 1,
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
		}
		public Budget Budget { get; private set; }
		public Currency Currency { get; private set; }
		internal IList<ExpenseCategory> Expenses { get; private set; } = new List<ExpenseCategory>();
	}
}
