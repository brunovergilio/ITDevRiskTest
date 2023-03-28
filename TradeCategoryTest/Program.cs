using System;
using System.Collections.Generic;


namespace TradeCategoryTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            if (!DateTime.TryParse(input, out var referenceDate))
            {
                Console.WriteLine("Invalid reference date!");
                return;
            }

            input = Console.ReadLine();
            if (!int.TryParse(input, out var count) || count == 0)
            {
                Console.WriteLine("Invalid value!");
                return;
            }

            TradeContext context = new TradeContext(new DefaultTradeProcessStrategy(), referenceDate);
            List<TradeBase> trades = new List<TradeBase>();
            for (var i = 0; i < count; i++)
            {
                var tradeDetails = Console.ReadLine().Split(' ');
                if (tradeDetails.Length != 3 || !double.TryParse(tradeDetails[0], out var value) || !DateTime.TryParse(tradeDetails[2], out var nextPaymentDate))
                {
                    continue;
                }
                string clientSector = tradeDetails[1];

                var trade = context.ProcessTrade(value, clientSector, nextPaymentDate);
                trades.Add(trade);
            }

            Console.WriteLine();
            for (var i = 0; i < trades.Count; i++)
            {
                if (trades[i] != null)
                {
                    Console.WriteLine(trades[i].GetCategory().ToString());
                }
                else
                {
                    Console.WriteLine("UNKNOWN");
                }
            }
        }
    }
}
