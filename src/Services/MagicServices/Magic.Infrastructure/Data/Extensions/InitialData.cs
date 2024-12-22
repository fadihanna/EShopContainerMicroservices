
namespace Magic.Infrastructure.Data.Extensions
{
    internal class InitialData
    {
        public static IEnumerable<Provider> Providers =>
        new List<Provider>
        {
            Provider.Create("Orange","اورانج",true,1),
            Provider.Create("Masary","مصاري",true,2),
            Provider.Create("Momkn","ممكن",true,3)
        };
        public static IEnumerable<ServiceCategory> ServiceCategories =>
        new List<ServiceCategory>
        {
            ServiceCategory.Create("Recharge","شحن",true,1),
            ServiceCategory.Create("Vouchers","كروت",true,2),
            ServiceCategory.Create("Bills","فواتير",true,3)
        };
        public static IEnumerable<Service> Services =>
        new List<Service>
        {
            Service.Create("Orange Recharge","شحن اورانج",true,1,1),
            Service.Create("Vodafone Recharge","شحن فوادافون",true,2,1),
            Service.Create("Etisalat Recharge","شحن اتصالات",true,3,1),
            Service.Create("WE Recharge","شحن وي",true,4,1),

            Service.Create("Orange Vouchers","شحن اورانج",true,1,2),
            Service.Create("Vodafone Vouchers","شحن فوادافون",true,2,2),
            Service.Create("Etisalat Vouchers","شحن اتصالات",true,3,2),
            Service.Create("WE Vouchers","شحن وي",true,4,2),

            Service.Create("Orange Bills","شحن اورانج",true,1,3),
            Service.Create("Vodafone Bills","شحن فوادافون",true,2,3),
            Service.Create("Etisalat Bills","شحن اتصالات",true,3,3),
            Service.Create("WE Bills","شحن وي",true,4,3)
        };
        public static IEnumerable<Denomination> Denominations =>
        new List<Denomination>
        {
            Denomination.Create("Orange Air Recharge","شحن هوا اورانج",0,5,200,false,1,1,((int)DomainEnums.PriceType.Fixed),((int)DomainEnums.Provider.Orange),true),
            Denomination.Create("Vodafone Air Recharge","شحن هوا فوادافون",0,5,200,false,2,2,((int)DomainEnums.PriceType.Fixed),((int)DomainEnums.Provider.Momkn),true),
            Denomination.Create("Etisalat Air Recharge","شحن هوا اتصالات",0,5,200,false,3,3,((int)DomainEnums.PriceType.Fixed),((int)DomainEnums.Provider.Momkn),true),
            Denomination.Create("WE Air Recharge","شحن هوا وي",0,5,200,false,4,4,((int)DomainEnums.PriceType.Fixed),((int)DomainEnums.Provider.Momkn),true),

            Denomination.Create("Orange Voucher 13.5","كارت اورانج 13.5",13.5m,13.5m,13.5m,false,1,5,((int)DomainEnums.PriceType.Fixed),((int)DomainEnums.Provider.Orange),true),
            Denomination.Create("Vodafone Voucher 13.5","كارت فوادافون 13.5",13.5m,13.5m,13.5m,false,1,6,((int)DomainEnums.PriceType.Fixed),((int)DomainEnums.Provider.Momkn),true),
            Denomination.Create("Etisalat Voucher 13.5","كارت اتصالات 13.5",13.5m,13.5m,13.5m,false,1,7,((int)DomainEnums.PriceType.Fixed),((int)DomainEnums.Provider.Momkn),true),
            Denomination.Create("WE Voucher 13.5","كارت وي 13.5",13.5m,13.5m,13.5m,false,1,8,((int)DomainEnums.PriceType.Fixed),((int)DomainEnums.Provider.Momkn),true),

            Denomination.Create("Orange Bills","فواتير اورانج",0,5,1000,false,1,9,((int)DomainEnums.PriceType.Fixed),((int)DomainEnums.Provider.Orange),true),
            Denomination.Create("Vodafone Bills","فواتير فوادافون",0,5,1000,false,1,10,((int)DomainEnums.PriceType.Fixed),((int)DomainEnums.Provider.Momkn),true),
            Denomination.Create("Etisalat Bills","فواتير اتصالات",0,5,1000,false,1,11,((int)DomainEnums.PriceType.Fixed),((int)DomainEnums.Provider.Momkn),true),
            Denomination.Create("WE Bills","فواتير وي",0,5,1000,false,1,12,((int)DomainEnums.PriceType.Fixed),((int)DomainEnums.Provider.Momkn),true),
        };
    }
}