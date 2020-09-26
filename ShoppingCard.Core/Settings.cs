namespace ShoppingCard.Core
{
    public class Settings
    {
        public AppSettings AppSettings { get; set; }
        public CalculatorSettings CalculatorSettings { get; set; }
    }
    
    public class AppSettings
    {
        public string Name { get; set; }
    }
    public class CalculatorSettings
    {
        public decimal DeliveryPrice { get; set; }
        public decimal ProductDeliveryPrice { get; set; }
        public decimal BasketDeliveryPrice { get; set; }
    }
}