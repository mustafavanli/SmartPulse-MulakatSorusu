using System.Xml.Serialization;

namespace SmartPulse_MUHAMMED_MUSTAFA_VANLI.Models
{
    public class IntraDayTradeHistory
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Conract { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
