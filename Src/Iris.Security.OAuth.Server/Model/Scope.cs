using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Iris.Security.OAuth.Server.Model
{
    [Table("Scope")]
    public class Scope : EntityBase
    {
        [Key]
        [Required, StringLength(250)]
        public virtual string Value { get; set; }

        public virtual string Name { get; set; }

        public virtual ICollection<RoleScope> RoleScopes { get; set; }
    }
}