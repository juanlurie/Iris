using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Iris.Persistence;

namespace Iris.Security.OAuth.Server.Model
{
    [Table("Accountability")]
    public class Accountability : EntityBase, ISequentialGuidId
    {
        public virtual Guid Id { get; set; }

        [Required]
        public virtual Guid UserId { get; set; }
        public virtual User User { get; set; }


        [Required]
        public virtual Guid RoleId { get; set; }
        public virtual Role Role { get; set; }

        [Required]
        public virtual bool IsActive { get; set; }

        public virtual DateTime StartOfAccountability { get; set; }
        public virtual DateTime EndOfAccountability { get; set; }
    }   
}