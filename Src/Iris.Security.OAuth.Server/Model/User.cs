using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Iris.Persistence;

namespace Iris.Security.OAuth.Server.Model
{
    [Table("User")]
    public class User : EntityBase, ISequentialGuidId
    {
        [Key, Column(Order = 1)]
        public virtual Guid Id { get; set; }

        [Required, StringLength(120)]
        [Index("IX_User", IsClustered = false, Order = 2, IsUnique = true)]
        public virtual string Username { get; set; }

        [Required]
        public virtual int FailedLoginCount { get; set; }

        [Required]
        public virtual int FailedPasswordResetCount { get; set; }

        [Required]
        public virtual bool IsAccountVerified { get; set; }

        [Required]
        public virtual bool IsLoginAllowed { get; set; }

        [Required]
        public virtual bool IsWindowsAccount { get; set; }

        [Required]
        public virtual bool RequiresPasswordReset { get; set; }

        public virtual DateTime? LastLogin { get; set; }

        public virtual DateTime? LastFailedLogin { get; set; }

        [Required]
        [Index("IX_SessionKey", IsClustered = false, Order = 2, IsUnique = true)]
        public virtual Guid SessionKey { get; set; }

        public virtual byte[] HashedPassword { get; set; }

        [StringLength(100)]
        public virtual string VerificationCode { get; set; }


        [Required(AllowEmptyStrings = true), StringLength(120)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public virtual string FirstName { get; set; }

        [Required(AllowEmptyStrings = true), StringLength(120)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public virtual string Surname { get; set; }

        [StringLength(250, MinimumLength = 0)]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public virtual string Email { get; set; }

        [StringLength(15, MinimumLength = 0)]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public virtual string MobilePhone { get; set; }

        [StringLength(15, MinimumLength = 0)]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public virtual string OtherPhone { get; set; }

        public virtual ICollection<Accountability> Accountabilities { get; set; }
        //public virtual ICollection<User> Users { get; set; }

        public string NameAndSurname()
        {
            return String.Format("{0} {1}", FirstName, Surname).Trim();
        }

        public virtual ICollection<UserClaim> Claims { get; set; }

        public User()
        {
            Claims = new List<UserClaim>();
        }
    }
}