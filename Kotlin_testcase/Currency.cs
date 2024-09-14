using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Currency_classes
{
    class NegitiveTerminalCapacity : Exception { }
    class NoValidCurrency: Exception { }

    interface ICurrency
    {
        float GetRate(string currency);
        void ChangeExchangeRate(string currency, float rate);
        void IncreaseCapacity(float money);
        void DecreaseCapacity(float money);
        void GetCurrencyInfo();
    }
    abstract class Currency : ICurrency
    {
        protected float _capacity;
        protected Dictionary<string, float> _rates = new Dictionary<string, float>();
        protected Currency(float start_capacity)
        {
            _capacity = start_capacity;
        }
        public float GetRate(string currency)
        {
            if (_rates.ContainsKey(currency))
            {
                return _rates[currency];
            }
            throw new NoValidCurrency();
        }
        public void ChangeExchangeRate(string currency, float rate)
        {
            if (_rates.ContainsKey(currency))
            {
                _rates[currency] = rate;
                return;
            }
            throw new NoValidCurrency();
        }
        public void IncreaseCapacity(float money)
        {
            _capacity += money;
        }
        public void DecreaseCapacity(float money)
        {
            if (_capacity - money < 0)
            {
                throw new NegitiveTerminalCapacity();
            }
            _capacity -= money;
        }
        public void GetCurrencyInfo()
        {
            Console.WriteLine($"\n Валюта: {this.GetType().Name}");
            Console.WriteLine($"Количесвто в терминале: {_capacity}");
            Console.WriteLine($"Обменный курс: ");
            foreach (var key in _rates.Keys)
            {
                float money  = _rates[key];
                if (key != "BTC")
                {
                    money = (float)Math.Round(money, 2);
                }
                Console.WriteLine($"{key} = {money:N6} {this.GetType().Name}");

            }
        }
    }

    class RUB : Currency
    {
        public RUB(float start_capacity) : base(start_capacity)
        {
            _rates.Add("USD", 90);
            _rates.Add("EUR", 100);
        }

    }
    class USD : Currency
    {
        public USD(float start_capacity) : base(start_capacity)
        {
            _rates.Add("RUB", 1/90);
            _rates.Add("EUR", 1.11f);
            _rates.Add("USDT", 1);
            _rates.Add("BTC", 60000);
        }

    }
    class EUR : Currency
    {
        public EUR(float start_capacity) : base(start_capacity)
        {
            _rates.Add("RUB", 1 / 100);
            _rates.Add("USD", 1/1.11f);
        }
        
    }
    class USDT : Currency
    {
        public USDT(float start_capacity) : base(start_capacity)
        {
            _rates.Add("USD", 1);
        }
    }
    class BTC : Currency
    {
        public BTC(float start_capacity) : base(start_capacity)
        {
            _rates.Add("USD", 1 / 60000f);
        }
    }


}
