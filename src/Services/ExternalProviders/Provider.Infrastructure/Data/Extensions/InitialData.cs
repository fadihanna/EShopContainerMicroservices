
using BuildingBlocks.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Provider.Domain.Models;

namespace Provider.Infrastructure.Data.Extensions
{
    internal class InitialData
    {
        public static IEnumerable<MasaryService> MasaryServices =>
        new List<MasaryService>(){
            new MasaryService(){
                ServiceId = 1234,
                ProviderId = ((int)CommonEnums.Provider.Masary),
                Name = "Masary",
                NameAr = "مصاري",
                PriceType = "1",
                MinValue = 10,
                MaxValue = 10,
                SortOrder = 1,
                InquiryRequired = false  // 0 in your row => false
            } 
        };
        public static IEnumerable<MasaryServiceCharge> MasaryServiceCharges =>
        new List<MasaryServiceCharge>
        {
            new MasaryServiceCharge(){
                ServiceId = 1,
                From = 1,
                To = 100,
                Charge = 5,
                Slap = 3,          // as provided
                Percentage = false
            }
        };
        public static IEnumerable<MasaryServiceParameter> MasaryServiceParameters =>
        new List<MasaryServiceParameter>
        {
            new MasaryServiceParameter(){ 
                ServiceId = 1,
                Key = "MobileNumber",
                Name = "Mobile Number",
                NameAr = "رقم التليفون",
                Position = 1,
                Visible = true,             // 1
                Required = true,            // 1
                ParameterType = "0",          // as provided
                ClientId = true,
                DefaultValue = "0",
                MinLength = 12,
                MaxLength = 12,
                ConfirmRequired = true      // 1
            }
        };
    }
}