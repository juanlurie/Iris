using System;
using System.ComponentModel.DataAnnotations;
using Iris.Persistence;

namespace Iris.Security.OAuth.Server
{
    public abstract class EntityBase : IPersistenceAudit
    {
        [Timestamp]
        public byte[] TimeStamp { get; set; }

        [StringLength(120)]
        public virtual string ModifiedBy { get; set; }

        [StringLength(120)]
        public virtual string CreatedBy { get; set; }
        public virtual DateTime ModifiedTimestamp { get; set; }
        public virtual DateTime CreatedTimestamp { get; set; }
    }
}