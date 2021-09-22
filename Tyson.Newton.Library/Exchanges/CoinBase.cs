using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tyson.Newton.Library.Models;

namespace Tyson.Newton.Library.Exchanges
{
    public class CoinBase
    {
        //For Reference: https://docs.pro.coinbase.com/#get-product-order-book

        private string _baseUrl = "https://api.pro.coinbase.com";

        private string _symbol1;
        private string _symbol2;
        private int? _level;

        /// <summary>
        /// Put in two symbols and an options level
        /// Level can be 1 (default), 2, or 3
        /// 1 is best bid and ask
        /// 2 is full order book non aggregated
        /// 3 is full order book aggregated
        /// </summary>
        /// <param name="symbol1"></param>
        /// <param name="symbol2"></param>
        /// <param name="level"></param>
        public CoinBase(string symbol1, string symbol2, int? level)
        {
            _symbol1 = symbol1;
            _symbol2 = symbol2;
            _level = level;
        }


        public async Task<CoinBaseOrderBook> GetCoinBaseOrderBook()
        {
            //Not going to support level 3 for this iteration
            if(_level.HasValue && (_level > 2 || _level < 1))
            {
                throw new Exception($"Unsupported level, currently supported levels are 1 and 2");
            }

            var reqPath = "/products/<<SYMBOLS>>/book";

            //This request requires two symbols and an options level
            //Symbol pattern is BTC-USD

            if (string.IsNullOrEmpty(_symbol1) || string.IsNullOrEmpty(_symbol2))
            {
                //Need both symbols
                throw new Exception("This request requries two symbols");
            }

            var orders = new CoinBaseOrderBook();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //Create headers
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0");
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage res = new HttpResponseMessage();
                    res = await client.GetAsync(CreateProperUrl(reqPath));

                    //Success
                    if (res.IsSuccessStatusCode)
                    {
                        var resString = await res.Content.ReadAsStringAsync();

                        //Deserlalize to object
                        var deseralizedOrderBook = JsonConvert.DeserializeObject<CoinBaseOrderBook>(resString);

                        return deseralizedOrderBook;
                    }
                    else
                    {
                        throw new Exception($"Problem getting Order book from Coin Base - {res.StatusCode}");
                    }
                }
                catch (Exception err)
                {
                    throw new Exception($"Internal Server Error - {err.Message}");
                }
            }
        }


        //Helper
        private string CreateProperUrl(string path)
        {
            string fullUrl;

            path = path.Replace("<<SYMBOLS>>", $"{_symbol1}-{_symbol2}");

            if (_level.HasValue)
            {
                fullUrl = $"{_baseUrl}{path}?level={_level}";
            }
            else
            {
                fullUrl = $"{_baseUrl}{path}";
            }

            return fullUrl;
        }

    }
}
