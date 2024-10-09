using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Currency_classes;

namespace Terminal_class
{
    class NoIncreaseCaurrency : Exception { };
    class NoDecreaseCaurrency : Exception { };
    class NotEnougthMoney : Exception { };
    class NegativeExchange : Exception { };
    public interface ITerminal
    {
        public float ExchangeMoney(string increaseCurrency, string DecreaseCurrency, float money);
        public void GetTerminalInfo();
        void ChangeAll();
    }
    public class Terminal : ITerminal
    {
        Dictionary<string, Currency> _currencies = new Dictionary<string, Currency>();

        public Terminal()
        {
            _currencies.Add("RUB",new RUB(10000));
            _currencies.Add("USD", new USD(1000));
            _currencies.Add("EUR", new EUR(1000));
            _currencies.Add("USDT", new USDT(1000));
            _currencies.Add("BTC", new BTC(1.5f));
        }
        public void ChangeUSD_RUBRate()
        {
            Random rnd = new Random();
            float percent = 5 * (float)rnd.NextDouble() - 10;
            float rub = _currencies["RUB"].GetRate("USD") * (100 + percent) / 100;
            float usd = 1 / rub;
            _currencies["RUB"].ChangeExchangeRate("USD", rub);
            _currencies["USD"].ChangeExchangeRate("RUB", usd);
        }

        public void ChangeEUR_RUBRate()
        {
            Random rnd = new Random();
            float percent = 5 * (float)rnd.NextDouble() - 10;
            float rub = _currencies["RUB"].GetRate("EUR") * (100 + percent) / 100;
            float eur = 1 / rub;
            _currencies["RUB"].ChangeExchangeRate("EUR", rub);
            _currencies["EUR"].ChangeExchangeRate("RUB", eur);
        }

        public void ChangeUSD_EURRate()
        {
            Random rnd = new Random();
            float percent = 5 * (float)rnd.NextDouble() - 10;
            float usd = _currencies["USD"].GetRate("EUR") * (100 + percent) / 100;
            float eur = 1 / usd;
            _currencies["USD"].ChangeExchangeRate("EUR", usd);
            _currencies["EUR"].ChangeExchangeRate("USD", eur);
        }
        public void ChangeUSD_USTDRate()
        {
            Random rnd = new Random();
            float percent = 5 * (float)rnd.NextDouble() - 10;
            float usd = _currencies["USD"].GetRate("USDT") * (100 + percent) / 100;
            float usdt = 1 / usd;
            _currencies["USD"].ChangeExchangeRate("USDT", usd);
            _currencies["USDT"].ChangeExchangeRate("USD", usdt);
        }
        public void ChangeUSD_BTCRate()
        {
            Random rnd = new Random();
            float percent = 5 * (float)rnd.NextDouble() - 10;
            float usd = _currencies["USD"].GetRate("BTC") * (100 + percent) / 100;
            float btc = 1 / usd;
            _currencies["USD"].ChangeExchangeRate("BTC", usd);
            _currencies["BTC"].ChangeExchangeRate("USD", btc);
        }
        public float ExchangeMoney(string increaseCurrency, string DecreaseCurrency, float money)
        {
            if (money < 0 )
            {
                throw new NegativeExchange();
            }
            if (!_currencies.ContainsKey(DecreaseCurrency))
            {
                throw new NoDecreaseCaurrency();
            }
            if (!_currencies.ContainsKey(increaseCurrency))
            {
                throw new NoIncreaseCaurrency();
            }
            float rate  = _currencies[increaseCurrency].GetRate(DecreaseCurrency);
            float result_money = money / rate;

            if (increaseCurrency != "BTC")
            {
                money = (float)Math.Round(money, 2);
            }
            if (DecreaseCurrency != "BTC")
            {
                result_money = (float)Math.Round(result_money, 2);
            }
            try
            {
                _currencies[DecreaseCurrency].DecreaseCapacity(result_money);
            }
            catch (NegitiveTerminalCapacity)
            {
                throw new NotEnougthMoney();
            }
            _currencies[increaseCurrency].IncreaseCapacity(money);
            return result_money;
        }

        public void GetTerminalInfo()
        {
            Console.WriteLine("=== Информация из терминала ===");
            foreach (Currency curr in _currencies.Values)
            {
                curr.GetCurrencyInfo();
            }
        }
        //Big fun
        public void ChangeAll()
        {
            ChangeEUR_RUBRate();
            ChangeUSD_BTCRate();
            ChangeUSD_USTDRate();
            ChangeUSD_EURRate();
            ChangeUSD_RUBRate();
        }
    }
}
