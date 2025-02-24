using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Magic.Infrastructure.Data.Identity.Entity
{
    public class ConsumerUser : IdentityUser<int>
    {
        public string FullName { get; set; }
        [PersonalData]
        [Column("MobileNumber")]  // Renaming the column in the database
        public override string UserName
        {
            get => base.UserName;
            set => base.UserName = value;
        }
        [PersonalData]
        [Column("NormalizedMobileNumber")]  // Renaming the column in the database
        public override string NormalizedUserName
        {
            get => base.NormalizedUserName;
            set => base.NormalizedUserName = value;
        }
    }
}