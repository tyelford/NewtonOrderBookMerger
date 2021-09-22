using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tyson.Newton.Library.Exchanges;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tyson.Newton.Library.Exchanges.Tests
{
    [TestClass()]
    public class CoinBaseTests
    {
        [TestMethod()]
        public async Task GetBinanceOrderBookTestWithLevel()
        {
            try
            {
                var cb = new CoinBase("BTC", "USD", 2);

                var orderBook = await cb.GetCoinBaseOrderBook();

                Assert.IsNotNull(orderBook);

            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }


        [TestMethod()]
        public async Task GetBinanceOrderBookTestWithOutLevel()
        {
            try
            {
                var cb = new CoinBase("BTC", "USD", null);

                var orderBook = await cb.GetCoinBaseOrderBook();

                Assert.IsNotNull(orderBook);

            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }


        [TestMethod()]
        public async Task GetBinanceOrderBookTestWithBadLevel()
        {
            try
            {
                var cb = new CoinBase("BTC", "USD", 0);

                var orderBook = await cb.GetCoinBaseOrderBook();

                Assert.Fail();

            }
            catch (Exception err)
            {
                Assert.IsNotNull(err);
                //This is a pass
            }
        }
    }
}