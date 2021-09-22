using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tyson.Newton.Library.Exchanges;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tyson.Newton.Library.Exchanges.Tests
{
    [TestClass()]
    public class BinanceTests
    {
        [TestMethod()]
        public async Task GetBinanceOrderBookTestWithLimit()
        {

            try
            {
                var binance = new Binance("ADA", "USDT", 10);

                var orderBook = await binance.GetBinanceOrderBook();

                Assert.IsNotNull(orderBook);

            }
            catch(Exception err)
            {
                Assert.Fail(err.Message);
            }
        }


        [TestMethod()]
        public async Task GetBinanceOrderBookTestWithNoLimit()
        {

            try
            {
                var binance = new Binance("ADA", "USDT", null);

                var orderBook = await binance.GetBinanceOrderBook();

                Assert.IsNotNull(orderBook);

            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

    }
}