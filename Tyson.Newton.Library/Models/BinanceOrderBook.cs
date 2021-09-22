using System;
using System.Collections.Generic;
using System.Text;

namespace Tyson.Newton.Library.Models
{
    public class BinanceOrderBook
    {

        //NOTE: A custom deserliazer would make this better, but for the interest of time

        public long lastUpdateId { get; set; }

        /// <summary>
        /// bids
        /// Index 0 is price
        /// Index 1 is quantity
        /// </summary>
        public List<List<string>> bids { get; set; }

    }
}
