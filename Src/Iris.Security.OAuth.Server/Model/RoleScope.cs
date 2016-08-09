using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Iris.Security.OAuth.Server.Model
{
    [Table("RoleScope")]
    public class RoleScope : EntityBase
    {
        [Key, Column(Order = 1), ForeignKey("Role")]
        public virtual Guid RoleId { get; set; }
        public virtual Role Role { get; set; }

        [Key, Column(Order = 2), ForeignKey("Scope")]
        public virtual string ScopeValue { get; set; }
        public virtual Scope Scope { get; set; }
    }
}