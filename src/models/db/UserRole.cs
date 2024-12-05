using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mynance.src.models.db
{
    // TODO: zasto mi puca kada je Username a ne username ?? ?? ?? ? 

    [PrimaryKey(nameof(Username), nameof(Role))]
    [Table("user_roles")]
    public class UserRole
    {
        [Column("username")]
        public string Username { get; set; }

        [Column("role")]
        public int Role { get; set; }
    }
}
