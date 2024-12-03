using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using mynance.src.exceptions;
using mynance.src.exceptions.auth;
using mynance.src.models;
using mynance.src.models.contexts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
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
        public void LoginUser(String username, String password) {
            if (username == null || password == null || username.Trim().Length == 0 || password.Trim().Length == 0)
                throw new EmptyFieldException(LocalizedMessages.Instance.GetMessage("emptyField"));
                //throw new EmptyFieldException("Username field cannot be empty.");

            if (usernameRegex.IsMatch(username) == false)
                throw new InvalidUsernameException(LocalizedMessages.Instance.GetMessage("invalidUsername"));
                //throw new InvalidUsernameException("Invalid username.");

            UserContext ctx = new();
            User user = ctx.Users.Where(x => x.Username == username).FirstOrDefault() 
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
        }

        public void RegisterUser(String username, String password, String passwordRepeat, String fullName)
        {
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
            CurrentUser = null;
            Role = 0;
        }
    }
}
