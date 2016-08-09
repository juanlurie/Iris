using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Iris.Persistence;

namespace Iris.Security.OAuth.Server.Model
{
    [Table("Role")]
    public class Role : EntityBase, ISequentialGuidId
    {
        public virtual Guid Id { get; set; }

        [Required, StringLength(120)]
        [Index(IsClustered = false, IsUnique = true)]
        public virtual string Name { get; set; }

        [Required(AllowEmptyStrings = true), StringLength(1000)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Description { get; set; }

        public virtual ICollection<Accountability> Accountabilities { get; set; }
        public virtual ICollection<RoleScope> RoleScopes { get; set; }
    }
}