using mynance.src.auth;
using mynance.src.models.db;
using mynance.src.models.db.contexts;
using mynance.src.navigation.pages.controllers;
using System.Diagnostics;

namespace mynance.src.models
{
	class UserState
	{
		public static UserState Instance = new();
		public Budget Budget { get; set; }
		public List<AssignedBudget> AssignedBudgets { get; set; } = new();

		private UserState()
		{
			BudgetContext ctx = new();
			Budget = ctx.Budgets.Where(b => b.Username == AuthGate.CurrentUser.Username).First();
			Budget ??= UserDashboardController.GenerateEmptyBudget();

			try
			{
				AssignedBudgetContext abCtx = new();
				AssignedBudgets = abCtx.AssignedBudgets.Where(ab => ab.BudgetID == Budget.ID).ToList();
				if (AssignedBudgets.Count() == 0)
				{
					UserDashboardController.GenerateEmptyAssignedBudgets(Budget.ID).ForEach(ab => AssignedBudgets.Add(ab));
				}
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
			}
		}

		// should be called with each property's {set} method but idc cba
		public void Persist()
		{
			if (AuthGate.Role == 1) { return; }
			else
			{
				if (Budget != null)
				{
					Budget.Username = AuthGate.CurrentUser.Username;
					Trace.WriteLine(Budget);
					BudgetContext budgetCtx = new();
					budgetCtx.Update(Budget);
					budgetCtx.SaveChanges();
				}

				if (AssignedBudgets != null)
				{
					AssignedBudgetContext assignedBudgetCtx = new();

					foreach (var assignedBudget in AssignedBudgets)
					{
						assignedBudgetCtx.Update(assignedBudget);
					}

					assignedBudgetCtx.SaveChanges(true);
				}
			}
		}
	}
}
