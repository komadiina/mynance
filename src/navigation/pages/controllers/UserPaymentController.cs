using mynance.src.auth;
using mynance.src.models.db;
using mynance.src.models.db.contexts;
using mynance.src.utilities;

namespace mynance.src.navigation.pages.controllers
{
	class UserPaymentController
	{
		public UserPaymentController() { }

		public Boolean CreatePaymentOutgoing(String amountStr, int categoryID, String note)
		{
			if (Double.TryParse(amountStr, out double amount) && amount > 0)
			{
				BudgetContext budgetCtx = new();
				Payment payment = new()
				{
					Amount = amount,
					AssignedBudgetID = categoryID + 1, // 0-index
					BudgetID = budgetCtx.Budgets.Where(b => b.Username == AuthGate.CurrentUser.Username).First().ID,
					DateTime = DateTimeUtils.GetCurrentTimestamp(),
					Outgoing = true,
					Note = note.Trim(),
					Username = AuthGate.CurrentUser.Username
				};

				PaymentsContext paymentCtx = new();
				paymentCtx.Payments.Add(payment);
				paymentCtx.SaveChanges();

				return true;
			}
			else return false;
		}

		// no need for category id, this will just be used for incoming payments statistic
		public Boolean CreatePaymentIncoming(String amountStr, String note)
		{
			if (Double.TryParse(amountStr, out double amount) && amount > 0)
			{
				BudgetContext budgetCtx = new();
				Budget budget = budgetCtx.Budgets.Where(b => b.Username == AuthGate.CurrentUser.Username).First();

				Payment payment = new()
				{
					Amount = amount,
					AssignedBudgetID = 1, // ignored, Outgoing has to be looked at first, idk simplicity
					BudgetID = budget.ID,
					DateTime = DateTimeUtils.GetCurrentTimestamp(),
					Outgoing = false,
					Note = note.Trim(),
					Username = AuthGate.CurrentUser.Username
				};

				PaymentsContext paymentCtx = new();
				paymentCtx.Payments.Add(payment);
				paymentCtx.SaveChanges();

				// increase user's budget
				budget.Amount += amount;
				budgetCtx.Budgets.Update(budget);
				budgetCtx.SaveChanges();

				return true;
			}
			else return false;
		}
	}
}
