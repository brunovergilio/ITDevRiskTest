using System;
using System.Diagnostics;


namespace TradeCategoryTest
{
    // This is a middle-ground for collecting information and passing it over to the
    // trade process interface
    public struct TradeContextData
    {
        public double value;
        public string clientSector;
        public DateTime nextPaymentDate;
        // The property below is for question #2 of this test
        public bool isPoliticallyExposed;
    }

    // This is the main object used to request a trade based on the details provided.
    // It takes a process strategy, which contains the logic used in determining
    // the type of trade that will be returned, and a reference date
    public class TradeContext
    {
        public ITradeProcessStrategy Strategy { get; set; } = null;
        public DateTime ReferenceDate { get; set; } = DateTime.Now;

        public TradeContext()
        {
        }

        public TradeContext(ITradeProcessStrategy strategy, DateTime referenceDate)
        {
            Strategy = strategy;
            ReferenceDate = referenceDate;
        }

        public TradeBase ProcessTrade(double value, string clientSector, DateTime nextPaymentDate)
        {
            Debug.Assert(Strategy != null, "TradeContext needs a strategy for categorizing trades!");
            TradeContextData data = new TradeContextData()
            {
                value = value,
                clientSector = clientSector,
                nextPaymentDate = nextPaymentDate
            };

            return Strategy.ProcessTrade(ref data, ReferenceDate);
        }

        // Overload for PEP
        public TradeBase ProcessTrade(double value, string clientSector, DateTime nextPaymentDate, bool isPoliticallyExposed)
        {
            Debug.Assert(Strategy != null, "TradeContext needs a strategy for categorizing trades!");
            TradeContextData data = new TradeContextData()
            {
                value = value,
                clientSector = clientSector,
                nextPaymentDate = nextPaymentDate,
                isPoliticallyExposed = isPoliticallyExposed
            };

            return Strategy.ProcessTrade(ref data, ReferenceDate);
        }
    }
}
