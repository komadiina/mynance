using Microsoft.EntityFrameworkCore;
using mynance.src.exceptions;
using mynance.src.exceptions.auth;
using mynance.src.localization;
using mynance.src.models;
using mynance.src.models.db;
using mynance.src.models.db.contexts;
using mynance.src.styles;
using System.Diagnostics;
using System.Text.RegularExpressions;
using BC = BCrypt.Net.BCrypt;
namespace mynance.src.auth
{
	public partial class AuthGate
	{
		private readonly Regex usernameRegex = new("^[0-9a-zA-Z_]+$");
		public static User CurrentUser { get; private set; }
		public static int Role { get; private set; }

		public AuthGate() { }

		// TODO: implement proper return value and handling
		public void LoginUser(String username, String password)
		{
			if (username == null || password == null || username.Trim().Length == 0 || password.Trim().Length == 0)
				throw new EmptyFieldException(LocalizedMessages.Instance.GetMessage("emptyField"));
			//throw new EmptyFieldException("Username field cannot be empty.");

			if (usernameRegex.IsMatch(username) == false)
				throw new InvalidUsernameException(LocalizedMessages.Instance.GetMessage("invalidUsername"));
			//throw new InvalidUsernameException("Invalid username.");

			UserContext ctx = new();
			User user = ctx.Users.Where(x => x.Username == username && x.IsActive == true).FirstOrDefault()
				?? throw new NoUserExistsException(LocalizedMessages.Instance.GetMessage("noUserExists"));

			if (BC.EnhancedVerify(password, user.Password) == false)
				throw new InvalidPasswordException(LocalizedMessages.Instance.GetMessage("invalidCredentials"));
			//throw new InvalidPasswordException("Invalid password.");

			UserRoleContext rolesCtx = new();

			try
			{
				//UserRole role = rolesCtx.Roles.Where(ur => ur.username == username).FirstOrDefault();
				UserRole role = rolesCtx.Roles.Where(ur => EF.Property<string>(ur, nameof(UserRole.Username)) == username).FirstOrDefault();

				AuthGate.Role = role.Role;
				AuthGate.CurrentUser = user;
			}
			catch (Exception e)
			{
				Trace.WriteLine(e.Message);
				throw new NoUserExistsException(LocalizedMessages.Instance.GetMessage("unassignedRole"));
			}

			// Load preferences
			UserPreferenceContext preferencesCtx = new();
			UserPreference preference = preferencesCtx.UserPreferences.Where(up => up.Username == username).FirstOrDefault();
			if (preference == null)
			{
				// Save current locale & theme
				UserPreference current = new()
				{
					Username = username,
					LocaleName = LocaleHandler.Instance.CurrentLocale,
					UseDarkTheme = ThemeHandler.IsDarkMode,
					CurrencyID = 977
				};
				preference = current;
				preferencesCtx.Add(current);
				preferencesCtx.SaveChanges();
			}
			else
			{
				// Load saved locale & theme
				LocaleHandler.Instance.CurrentLocale = preference.LocaleName;
				ThemeHandler.IsDarkMode = preference.UseDarkTheme;
			}

			// Initialize preferences
			LocaleHandler.Instance.SetLocale(preference.LocaleName);
			ThemeHandler.SetTheme(preference.UseDarkTheme);

			// Update last active
			user.LastActive = DateTime.Now;
			ctx.Users.Update(user);
			ctx.SaveChanges();
		}

		public void RegisterUser(String username, String password, String passwordRepeat, String fullName)
		{
			if (username.Length >= 16)
				throw new InvalidUsernameException(LocalizedMessages.Instance.GetMessage("usernameTooLong"));

			if (usernameRegex.IsMatch(username) == false)
				throw new InvalidUsernameException(LocalizedMessages.Instance.GetMessage("invalidUsername"));
			//throw new InvalidUsernameException("Invalid username.");

			if (password != passwordRepeat)
				throw new InvalidPasswordException(LocalizedMessages.Instance.GetMessage("passwordMismatch"));
			//throw new InvalidPasswordException("Passwords do not match.");

			if (fullName == null)
				throw new EmptyFieldException(LocalizedMessages.Instance.GetMessage("emptyField"));
			//throw new EmptyFieldException("Full name field cannot be empty.");

			else if (fullName.Trim().Length == 0 || fullName.Trim() == "")
				throw new EmptyFieldException(LocalizedMessages.Instance.GetMessage("emptyField"));
			//throw new EmptyFieldException("Full name field cannot be empty.");

			UserContext ctx = new();
			if (ctx.Users.Any(u => u.Username == username))
				throw new InvalidUsernameException("Username already taken.");

			User user = new()
			{
				Username = username,
				Password = BC.EnhancedHashPassword(password),
				FullName = fullName,
				RegistrationDate = DateTime.Now,
				LastActive = DateTime.Now
			};

			ctx.Users.Add(user);
			ctx.SaveChanges();

			UserRole role = new()
			{
				Role = 2,
				Username = username
			};
			UserRoleContext rolesCtx = new();
			rolesCtx.Roles.Add(role);
			rolesCtx.SaveChanges();
		}

		public static void Logout()
		{
			// Persist preferences (not AuthGate responsibility but ok whatever)
			UserPreferenceContext preferencesCtx = new();
			UserPreference preference = preferencesCtx.UserPreferences.Where(up => up.Username == CurrentUser.Username).FirstOrDefault();

			if (preference != null)
			{
				preference.LocaleName = LocaleHandler.Instance.CurrentLocale;
				preference.UseDarkTheme = ThemeHandler.IsDarkMode;
				preferencesCtx.Update(preference);
				preferencesCtx.SaveChanges();
			}

			if (Role == 2) UserState.Instance.Persist();

			// Invalidate session
			CurrentUser = null;
			Role = 0;
		}
	}
}
