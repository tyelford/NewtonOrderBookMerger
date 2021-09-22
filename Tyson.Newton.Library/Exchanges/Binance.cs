using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tyson.Newton.Library.Models;

namespace Tyson.Newton.Library.Exchanges
{
    public class Binance
    {

        //For reference: https://binance-docs.github.io/apidocs/spot/en/#order-book

        private string _baseUrl = "https://api.binance.com";

        private string _symbol1;
        private string _symbol2;
        private int? _limit;

        /// <summary>
        /// Put in two symbols and an optional limit, make the limit null if not wanted
        /// </summary>
        /// <param name="symbol1"></param>
        /// <param name="symbol2"></param>
        /// <param name="limit"></param>
        public Binance(string symbol1, string symbol2, int? limit)
        {
            _symbol1 = symbol1;
            _symbol2 = symbol2;
            _limit = limit;
        }


        public async Task<BinanceOrderBook> GetBinanceOrderBook()
        {
            var reqPath = "/api/v3/depth";

            //This request requires two symbols and an options limit
            //Symbol pattern is ADAUSDT

            if(string.IsNullOrEmpty(_symbol1) || string.IsNullOrEmpty(_symbol2))
            {
                //Need both symbols
                throw new Exception("This request requries two symbols");
            }

            var orders = new BinanceOrderBook();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage res = new HttpResponseMessage();
                    res = await client.GetAsync(CreateProperUrl(reqPath));

                    //Success
                    if (res.IsSuccessStatusCode)
                    {
                        var resString = await res.Content.ReadAsStringAsync();

                        //Deserlalize to object
                        var deseralizedOrderBook = JsonConvert.DeserializeObject<BinanceOrderBook>(resString);

                        return deseralizedOrderBook;
                    }
                    else
                    {
                        throw new Exception($"Problem getting Order book from Binance - {res.StatusCode}");
                    }
                }
                catch(Exception err)
                {
                    throw new Exception($"Internal Server Error - {err.Message}");
                }
            }
        }


        //Helper
        private string CreateProperUrl(string path)
        {
            string fullUrl;
            if (_limit.HasValue)
            {
                fullUrl = $"{_baseUrl}{path}?symbol={_symbol1}{_symbol2}&limit={_limit}";
            }
            else
            {
                fullUrl =  $"{_baseUrl}{path}?symbol={_symbol1}{_symbol2}";
            }

            return fullUrl;
        }




    }
}
