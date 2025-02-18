using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic.Domain.Models
{
    [Table("Balance")]
    public class Balance : Entity<int>
    {
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string InvoiceId { get; set; }
        public DateTime CreatedOn { get; set; }
        public decimal Before { get; set; }
        public decimal After { get; set; }  
        public string BillingAccount { get; set; }
        public decimal CurrentBalance { get; set; }
        public int ProviderId { get; set; }

        public static Balance Create(
           string UserId,
           decimal Amount,
           string Description,
           string InvoiceId,
           DateTime CreatedOn,
           decimal Before,
           decimal After,
           string BillingAccount,
           decimal CurrentBalance,
           int providerId
        )
        {
            if (string.IsNullOrWhiteSpace(InvoiceId))
                throw new ArgumentException("InvoiceId name cannot be empty.", nameof(InvoiceId));

            if (Amount <=0M)
                throw new ArgumentException("Amount cannot be less than or equal to zero.");

            return new Balance
            {
                UserId = UserId,
                Amount = Amount,
                Description = Description,
                InvoiceId = InvoiceId,
                CreatedOn = CreatedOn,
                Before = Before,
                After = After,
                BillingAccount = BillingAccount, // Foreign Key Value
                CurrentBalance = CurrentBalance,
                ProviderId = providerId
            };
        }
    }
}
