using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace mynance.src.models
{
    [PrimaryKey(nameof(Username))]
    public class User
    {
        [Column("username")]
        public String Username { get; set; }

        [Column("fullname")]
        public String FullName { get; set; }

        [Column("password")]
        public String Password { get; set; }

        [Column("registration_date")]
        public DateTime RegistrationDate { get; set; }

        [Column("last_active")]
        public DateTime LastActive { get; set; }
    }
}
