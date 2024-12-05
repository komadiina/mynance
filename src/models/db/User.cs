using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace mynance.src.models.db
{
    [PrimaryKey(nameof(Username))]
    public class User
    {
        [Column("username")]
        public string Username { get; set; }

        [Column("fullname")]
        public string FullName { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("registration_date")]
        public DateTime RegistrationDate { get; set; }

        [Column("last_active")]
        public DateTime LastActive { get; set; }
    }
}
