
namespace Magic.Infrastructure.Data.Extensions
{
    internal class InitialData
    {
        public static IEnumerable<Lookups.Provider> Providers =>
        new List<Lookups.Provider>
        {
            Lookups.Provider.Create("Orange","اورانج",true,1),
            Lookups.Provider.Create("Masary","مصاري",true,2),
            Lookups.Provider.Create("Momkn","ممكن",true,3)
        };
        public static IEnumerable<ServiceCategory> ServiceCategories =>
        new List<ServiceCategory>
        {
            ServiceCategory.Create("Recharge","شحن","RechargeIcone",true,"",1),
            ServiceCategory.Create("Vouchers","كروت","CardIcone",true,"", 2),
            ServiceCategory.Create("Bills","فواتير","BillIcone",true,"", 3)
        };
        public static IEnumerable<Service> Services =>
        new List<Service>
        {
            Service.Create("Orange Recharge","شحن اورانج","IconeRecharge",true,"", 1,1),
            Service.Create("Vodafone Recharge","شحن فوادافون","IconeRecharge",true,"",2,1),
            Service.Create("Etisalat Recharge","شحن اتصالات","IconeRecharge",true,"", 3,1),
            Service.Create("WE Recharge","شحن وي","IconeRecharge",true,"", 4,1),

            Service.Create("Orange Vouchers","شحن اورانج","IconeRecharge",true,"", 1,2),
            Service.Create("Vodafone Vouchers","شحن فوادافون","IconeRecharge",true,"", 2,2),
            Service.Create("Etisalat Vouchers","شحن اتصالات","IconeRecharge",true,"", 3,2),
            Service.Create("WE Vouchers","شحن وي","IconeRecharge",true,"", 4,2),

            Service.Create("Orange Bills","شحن اورانج","IconeRecharge",true,"", 1,3),
            Service.Create("Vodafone Bills","شحن فوادافون","IconeRecharge",true,"", 2,3),
            Service.Create("Etisalat Bills","شحن اتصالات","IconeRecharge",true,"", 3,3),
            Service.Create("WE Bills","شحن وي","IconeRecharge", true, "", 4, 3)
        };
        public static IEnumerable<DenominationGroup> DenominationGroups =>
            new List<DenominationGroup>
            {
              DenominationGroup.Create(  "Orange Air Recharge" ,"شحن هوا " ,1,false,25,true),
              DenominationGroup.Create(  "Orange Units" ,"وحدات " ,2,false,25,true),
              DenominationGroup.Create(  "Orange Mega" ,"ميجا نت  " ,3,false,25,true),

            };
        public static IEnumerable<Denomination> Denominations =>
        new List<Denomination>
        {
            Denomination.Create("Orange Air Recharge","شحن هوا اورانج",0,5,200,false,1,25,((int)DomainEnums.PriceType.Fixed),((int)CommonEnums.Provider.Orange),true,null),
            Denomination.Create("Vodafone Air Recharge","شحن هوا فوادافون",0,5,200,false,2,26,((int)DomainEnums.PriceType.Fixed),((int)CommonEnums.Provider.Momkn),true, null),
            Denomination.Create("Etisalat Air Recharge","شحن هوا اتصالات",0,5,200,false,3,27,((int)DomainEnums.PriceType.Fixed),((int)CommonEnums.Provider.Momkn),true, null),
            Denomination.Create("WE Air Recharge","شحن هوا وي",0,5,200,false,4,28,((int)DomainEnums.PriceType.Fixed),((int)CommonEnums.Provider.Momkn),true, null),

            Denomination.Create("Orange Voucher 13.5","كارت اورانج 13.5",13.5m,13.5m,13.5m,false,1,29,((int)DomainEnums.PriceType.Fixed),((int)CommonEnums.Provider.Orange),true,null),
            Denomination.Create("Vodafone Voucher 13.5","كارت فوادافون 13.5",13.5m,13.5m,13.5m,false,1,30,((int)DomainEnums.PriceType.Fixed),((int)CommonEnums.Provider.Momkn),true, null),
            Denomination.Create("Etisalat Voucher 13.5","كارت اتصالات 13.5",13.5m,13.5m,13.5m,false,1,31,((int)DomainEnums.PriceType.Fixed),((int)CommonEnums.Provider.Momkn),true, null),
            Denomination.Create("WE Voucher 13.5","كارت وي 13.5",13.5m,13.5m,13.5m,false,1,32,((int)DomainEnums.PriceType.Fixed),((int)CommonEnums.Provider.Momkn),true, null),

            Denomination.Create("Orange Bills","فواتير اورانج",0,5,1000,false,1,33,((int)DomainEnums.PriceType.Fixed),((int)CommonEnums.Provider.Orange),true, null),
            Denomination.Create("Vodafone Bills","فواتير فوادافون",0,5,1000,false,1,34,((int)DomainEnums.PriceType.Fixed),((int)CommonEnums.Provider.Momkn),true, null),
            Denomination.Create("Etisalat Bills","فواتير اتصالات",0,5,1000,false,1,35,((int)DomainEnums.PriceType.Fixed),((int)CommonEnums.Provider.Momkn),true, null),
            Denomination.Create("WE Bills","فواتير وي",0,5,1000,false,1,36,((int)DomainEnums.PriceType.Fixed),((int)CommonEnums.Provider.Momkn),true, null),
        };
    }
}