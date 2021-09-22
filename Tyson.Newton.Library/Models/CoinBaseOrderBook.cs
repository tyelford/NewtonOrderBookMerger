using System;
using System.Collections.Generic;
using System.Text;

namespace Tyson.Newton.Library.Models
{
    public class CoinBaseOrderBook
    {

        public long sequence { get; set; }

        /// <summary>
        /// Format [price, size, num-orders]
        /// </summary>
        public List<List<string>> bids;

        /// <summary>
        /// Format [price, size, num-orders]
        /// </summary>
        public List<List<string>> asks;


    }
}
