using System;


namespace TradeCategoryTest
{
    // I'm using the strategy pattern, so I can separate the logic used to determine which type of trade I will return.
    // The interface exposes only one method, ProcessTrade(), which is where the correct type of trade will be chosen,
    // based on the specific logic provided. Each class that implements this interface can provide a different
    // logic, which can be chosen in the TradeContext object - this allows for extensibility in the event
    // new trade categories are added and require a different or more complex logic

    public interface ITradeProcessStrategy
    {
        TradeBase ProcessTrade(ref TradeContextData data, DateTime referenceDate);
    }

    public class DefaultTradeProcessStrategy : ITradeProcessStrategy
    {
        public TradeBase ProcessTrade(ref TradeContextData data, DateTime referenceDate)
        {
            if ((int)((data.nextPaymentDate - referenceDate).TotalDays) <= -30)
            {
                return new ExpiredTrade(data.value, data.clientSector, data.nextPaymentDate);
            }
            else if (data.clientSector.ToLower().Contains("private") && data.value >= 1000000.0)
            {
                return new HighRiskTrade(data.value, data.clientSector, data.nextPaymentDate);
            }
            else if (data.clientSector.ToLower().Contains("public") && data.value >= 1000000.0)
            {
                return new MediumRiskTrade(data.value, data.clientSector, data.nextPaymentDate);
            }
            
            return null;
        }
    }

    // The code below is for question #2
    public class ExtendedTradeProcessStrategy : ITradeProcessStrategy
    {
        public TradeBase ProcessTrade(ref TradeContextData data, DateTime referenceDate)
        {
            if (data.isPoliticallyExposed)
            {
                return new PEPTrade(data.value, data.clientSector, data.nextPaymentDate);
            }
            else
            {
                // If not PEP, resort back to the default strategy
                return new DefaultTradeProcessStrategy().ProcessTrade(ref data, referenceDate);
            }
        }
    }
}