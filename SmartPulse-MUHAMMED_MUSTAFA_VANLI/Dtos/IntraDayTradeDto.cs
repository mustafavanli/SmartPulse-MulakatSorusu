namespace SmartPulse_MUHAMMED_MUSTAFA_VANLI.Dtos
{
    public class IntraDayTradeDto
    {
        public DateTime Date { get; set; } 
        public double TotalTransactionAmount { get; set; } // Toplam İşlem Tutarı
        public double TotalTransactionQuantity { get; set; } // Toplam İşlem Miktarı
        public double WeightAveragePrice { get; set; } // Ağırlık Ortalama Fiyat
    }
}
