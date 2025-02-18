namespace PaymentGateway.DTO.Order.Response
{
    //  myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Data
    {
    }

    public class Item
    {
        public string name { get; set; }
        public string description { get; set; }
        public int amount_cents { get; set; }
        public int quantity { get; set; }
    }

    public class Merchant
    {
        public int id { get; set; }
        public DateTime created_at { get; set; }
        public List<string> phones { get; set; }
        public List<object> company_emails { get; set; }
        public string company_name { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string postal_code { get; set; }
        public string street { get; set; }
    }

    public class PaymobCreateOrderResponse
    {
        public int id { get; set; }
        public DateTime created_at { get; set; }
        public bool delivery_needed { get; set; }
        public Merchant merchant { get; set; }
        public object collector { get; set; }
        public int amount_cents { get; set; }
        public ShippingData shipping_data { get; set; }
        public string currency { get; set; }
        public bool is_payment_locked { get; set; }
        public bool is_return { get; set; }
        public bool is_cancel { get; set; }
        public bool is_returned { get; set; }
        public bool is_canceled { get; set; }
        public string merchant_order_id { get; set; }
        public object wallet_notification { get; set; }
        public int paid_amount_cents { get; set; }
        public bool notify_user_with_email { get; set; }
        public List<Item> items { get; set; }
        public string order_url { get; set; }
        public int commission_fees { get; set; }
        public int delivery_fees_cents { get; set; }
        public int delivery_vat_cents { get; set; }
        public string payment_method { get; set; }
        public object merchant_staff_tag { get; set; }
        public string api_source { get; set; }
        public Data data { get; set; }
        public string token { get; set; }
        public string url { get; set; }
    }

    public class ShippingData
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string street { get; set; }
        public string building { get; set; }
        public string floor { get; set; }
        public string apartment { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
        public string postal_code { get; set; }
        public string extra_description { get; set; }
        public string shipping_method { get; set; }
        public int order_id { get; set; }
        public int order { get; set; }
    }


}
