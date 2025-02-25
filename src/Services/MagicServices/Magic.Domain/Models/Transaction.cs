using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic.Domain.Models
{
    public class Transaction : Entity<int>
    {
        public int Id { get; set; } // Primary key and Foregin Key to invoiceid in balance table
        public bool IsRefunded { get; set; } = false;
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public decimal Fees { get; set; } = 0;
        public decimal TotalAmount { get; set; }
        public int RequestId { get; set; }
        public int DenominationId { get; set; }
        public int PaymentProviderId { get; set; }
        public PaymentProvider PaymentProvider { get; set; }
        public int Status { get; set; } = 1;
        public string BillingAccount { get; set; }

        public static Transaction Create(
            bool isRefunded,
            string userId,
            decimal amount,
            decimal fees,
            decimal totalAmount,
            int requestId,
            int denominationId,
            int paymentProviderId,
            int status,
            string billingAccount
            )
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("UserId cannot be empty.", nameof(UserId));

            if (amount <= 0)
                throw new ArgumentException("Amount cannot be less than or equal to zero.");

            totalAmount = amount + fees;
        
            if (totalAmount <= 0)
                throw new ArgumentException("TotalAmount cannot be less than or equal to zero.");

            return new Transaction
            {
                IsRefunded = isRefunded,
                UserId = userId,
                Amount = amount,
                Fees = fees,
                TotalAmount = totalAmount,
                RequestId = requestId,
                DenominationId = denominationId,
                PaymentProviderId = paymentProviderId, 
                Status = status,
                BillingAccount = billingAccount
            };
        }
    }
}
