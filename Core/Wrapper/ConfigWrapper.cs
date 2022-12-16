using System.Configuration;

namespace Core.Wrapper
{
    public class Config: IConfig
    {
        public string language => ConfigurationManager.AppSettings["Language"];
        public string[] denominationsMX => ConfigurationManager.AppSettings["MX_denominations"].Split(",");
        public string[] denominationsUS => ConfigurationManager.AppSettings["US_denominations"].Split(",");
    }

    public interface IConfig
    {
        string language { get; }
        string[] denominationsMX { get; }
        string[] denominationsUS { get; }
    }
}
