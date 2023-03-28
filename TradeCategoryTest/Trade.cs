using System;


namespace TradeCategoryTest
{
    public enum TradeCategory : byte
    {
        EXPIRED,
        HIGHRISK,
        MEDIUMRISK,
        PEP // Question #2
    }

    public interface ITrade
    {
        double Value { get; } // indicates the transaction amount in dollars
        string ClientSector { get; } // indicates the client's sector which can be "Public" or "Private"
        DateTime NextPaymentDate { get; } // indicates when the next payment from the client to the bank is expected

        // This is for question #2
        bool IsPoliticallyExposed { get; }
    }

    // This abstract class serves the purpose of encapsulating common data, as well as
    // to provide a GetCategory() method, which is to be overriden in derived classes
    public abstract class TradeBase : ITrade
    {
        private double _value;
        private string _clientSector;
        private DateTime _nextPaymentDate;

        public double Value { get { return _value; } }
        public string ClientSector { get { return _clientSector; } }
        public DateTime NextPaymentDate { get { return _nextPaymentDate; } }
        public virtual bool IsPoliticallyExposed { get { return false; } }

        public TradeBase(double value, string clientSector, DateTime nextPaymentDate)
        {
            _value = value;
            _clientSector = clientSector;
            _nextPaymentDate = nextPaymentDate;
        }

        public abstract TradeCategory GetCategory();
    }

    public class ExpiredTrade : TradeBase
    {
        public ExpiredTrade(double value, string clientSector, DateTime nextPaymentDate)
            : base(value, clientSector, nextPaymentDate)
        {
        }

        public override TradeCategory GetCategory()
        {
            return TradeCategory.EXPIRED;
        }
    }

    public class HighRiskTrade : TradeBase
    {
        public HighRiskTrade(double value, string clientSector, DateTime nextPaymentDate)
            : base(value, clientSector, nextPaymentDate)
        {
        }

        public override TradeCategory GetCategory()
        {
            return TradeCategory.HIGHRISK;
        }
    }

    public class MediumRiskTrade : TradeBase
    {
        public MediumRiskTrade(double value, string clientSector, DateTime nextPaymentDate)
            : base(value, clientSector, nextPaymentDate)
        {
        }

        public override TradeCategory GetCategory()
        {
            return TradeCategory.MEDIUMRISK;
        }
    }

    // Question #2
    public class PEPTrade : TradeBase
    {
        public override bool IsPoliticallyExposed { get { return true; } }

        public PEPTrade(double value, string clientSector, DateTime nextPaymentDate)
            : base(value, clientSector, nextPaymentDate)
        {
        }

        public override TradeCategory GetCategory()
        {
            return TradeCategory.PEP;
        }
    }
}
