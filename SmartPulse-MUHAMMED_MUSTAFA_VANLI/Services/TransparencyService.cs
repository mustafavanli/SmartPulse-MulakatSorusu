using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartPulse_MUHAMMED_MUSTAFA_VANLI.Dtos;
using SmartPulse_MUHAMMED_MUSTAFA_VANLI.Models;

namespace SmartPulse_MUHAMMED_MUSTAFA_VANLI.Services
{
    public interface ITransparencyService
    {
        Task<List<IntraDayTradeDto>> GetTradeHistory(DateTime end, DateTime start);
    }
    public class TransparencyService : ITransparencyService
    {
        public readonly HttpClient client;
        public TransparencyService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<IntraDayTradeDto>> GetTradeHistory(DateTime start,DateTime end)
        {
            string url = $"https://seffaflik.epias.com.tr/transparency/service/market/intra-day-trade-history?endDate={start.ToString("yyyy-MM-dd")}&startDate={end.ToString("yyyy-MM-dd")}";
            
            var response = await client.GetAsync(url);

            var content = await response.Content.ReadAsStringAsync();

            JObject jo = JObject.Parse(content);
            var jsonList = jo["body"]["intraDayTradeHistoryList"].ToString();
            
            if (!string.IsNullOrEmpty(jsonList))
            {
                var intraDayTradeHistoryList = JsonConvert.DeserializeObject<List<IntraDayTradeHistory>>(jsonList);

                var phOnes = intraDayTradeHistoryList
                    .Where(x => x.Conract.StartsWith("PH"))
                    .GroupBy(x => x.Conract); 

                List<IntraDayTradeDto> intraDayTradeDtoList = new List<IntraDayTradeDto>();

                foreach (var entry in phOnes) 
                {
                    IntraDayTradeDto intraDayTradeDto = new IntraDayTradeDto();
                    
                    intraDayTradeDto.TotalTransactionAmount = entry.Sum(x => (x.Price * x.Quantity) / 10);
                    intraDayTradeDto.TotalTransactionQuantity = entry.Sum(x => x.Quantity / 10);

                    intraDayTradeDto.WeightAveragePrice = intraDayTradeDto.TotalTransactionAmount / intraDayTradeDto.TotalTransactionQuantity;

                    intraDayTradeDto.Date = DateTime.ParseExact(entry.First().Conract.Substring(2), "yyMMddHH", null);

                    intraDayTradeDtoList.Add(intraDayTradeDto);
                }
                
                intraDayTradeDtoList = intraDayTradeDtoList.OrderBy(x => x.Date).ToList();
                return intraDayTradeDtoList;
            }



            //
            return new List<IntraDayTradeDto>();
        }
    }
}
