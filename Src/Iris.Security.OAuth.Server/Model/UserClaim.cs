using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Iris.Security.OAuth.Server.Model
{
    [Table("UserClaim")]
    public class UserClaim : EntityBase
    {
        [Key, Column(Order = 1), ForeignKey("User")]
        public virtual Guid UserId { get; set; }
        public virtual User User { get; set; }

        [Key, Column(Order = 2)]
        [StringLength(150)]
        public virtual string Type { get; set; }

        [Key, Column(Order = 3)]
        [StringLength(150)]
        public virtual string Value { get; set; }

        public UserClaim()
        {
        }

        public UserClaim(string type, string value)
        {
            Type = type;
            Value = value;
        }
    }
}