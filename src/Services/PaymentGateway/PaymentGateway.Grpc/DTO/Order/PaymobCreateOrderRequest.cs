namespace PaymentGateway.DTO.Order.Request
{
    public class Item
    {
        public string? name { get; set; }
        public string? amount_cents { get; set; }
        public string? description { get; set; }
        public string? quantity { get; set; }
    }

    public class PaymobCreateOrderRequest
    {
        public string auth_token { get; set; }
        public string? delivery_needed { get; set; }
        public string? amount_cents { get; set; }
        public string currency { get; set; }
        public int merchant_order_id { get; set; }
        public List<Item>? items { get; set; }
        public ShippingData? shipping_data { get; set; }
        public ShippingDetails? shipping_details { get; set; }
    }

    public class ShippingData
    {
        public string? apartment { get; set; }
        public string? email { get; set; }
        public string? floor { get; set; }
        public string? first_name { get; set; }
        public string? street { get; set; }
        public string? building { get; set; }
        public string? phone_number { get; set; }
        public string? postal_code { get; set; }
        public string? extra_description { get; set; }
        public string? city { get; set; }
        public string? country { get; set; }
        public string? last_name { get; set; }
        public string? state { get; set; }
    }

    public class ShippingDetails
    {
        public string? notes { get; set; }
        public int? number_of_packages { get; set; }
        public int? weight { get; set; }
        public string? weight_unit { get; set; }
        public int? length { get; set; }
        public int? width { get; set; }
        public int? height { get; set; }
        public string? contents { get; set; }
    }


}
