using mynance.src.models.db;
using mynance.src.models.db.contexts;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace mynance.src.navigation.pages
{
	internal class LandingAdministratorViewModel : INotifyPropertyChanged
	{
		public ObservableCollection<User> Users { get; set; }
		public ICommand ToggleActivateUserCommand { get; private set; }

		public LandingAdministratorViewModel()
		{
			UserContext ctx = new();
			Users = new();

			foreach (var user in ctx.Users.AsQueryable())
			{
				Users.Add(user);
			}

			ToggleActivateUserCommand = new mynance.src.mvvm.RelayCommand(ToggleActivateUser);
		}

		// TODO: fix, not being called?
		private void ToggleActivateUser(object parameter)
		{
			User user = parameter as User;

			foreach (var item in Users)
			{
				if (item.Username.Equals(item.Username))
				{
					item.IsActive = !item.IsActive;
					UserContext ctx = new();
					var targetUser = ctx.Users.Where(u => u.Username == item.Username).FirstOrDefault();
					targetUser.IsActive = !item.IsActive;
					ctx.Users.Update(targetUser);
					ctx.SaveChanges();
				}
			}

			Trace.WriteLine("Deactivated: " + user.ToString());
		}

		public event PropertyChangedEventHandler? PropertyChanged;
		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
