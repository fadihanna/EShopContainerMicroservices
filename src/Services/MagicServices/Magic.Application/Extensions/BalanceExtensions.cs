using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic.Application.Extensions
{
    public static class BalanceExtensions
    {
        public static IEnumerable<BalanceDto> ToBalanceDtoList(this IEnumerable<Balance> balances)
        {
            return balances.Select(balance => new BalanceDto(
                UserId: balance.UserId,
                Amount:  balance.Amount,
                Description: balance.Description,
                InvoiceId: balance.InvoiceId,
                CreatedOn:  balance.CreatedOn,
                Before: balance.Before,
                After: balance.After,
                BillingAccount: balance.BillingAccount,
                CurrentBalance:  balance.CurrentBalance,
                ProviderId: balance.ProviderId
            ));
        }

        public static BalanceDto ToBalanceDto(this Balance balance)
        {
            return DtoFromBalance(balance);
        }
        private static BalanceDto DtoFromBalance(Balance balance)
        {
            return new BalanceDto(
                UserId: balance.UserId,
                Amount: balance.Amount,
                Description: balance.Description,
                InvoiceId: balance.InvoiceId,
                CreatedOn: balance.CreatedOn,
                Before: balance.Before,
                After: balance.After,
                BillingAccount: balance.BillingAccount,
                CurrentBalance: balance.CurrentBalance,
                ProviderId: balance.ProviderId
            );
        }
        public static Balance DtoToBalance(this BalanceDto balanceDto)
        {
            return BalanceFromDto(balanceDto);
        }
        private static Balance BalanceFromDto(BalanceDto dto)
        {
            return Balance.Create(
                dto.UserId,
                dto.Amount,
                dto.Description,
                dto.InvoiceId,
                dto.CreatedOn,
                dto.Before,
                dto.After,
                dto.BillingAccount,
                dto.CurrentBalance,
                dto.ProviderId
            );
        }
    }
}
