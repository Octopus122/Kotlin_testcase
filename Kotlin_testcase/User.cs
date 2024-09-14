using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal_class;


namespace User_class
{
    class NotEnoughtUserMoney : Exception { };
    class WrongCurrency : Exception { };
    internal interface IUser
    {
        void GetCurrentInfo();
        void Exchange(string user_curr, string terminal_curr, float money);
        void GetUserBalance();
        void GetCurrBalance(string currency);
    }
    public class User : IUser
    {
        private ITerminal _terminal;
        private Dictionary<string, float> _money = new Dictionary<string, float>();

        public User(ITerminal terminal)
        {
            _terminal = terminal;
            _money.Add("RUB", 1000000);
            _money.Add("USD", 0);
            _money.Add("EUR", 0);
            _money.Add("USDT", 0);
            _money.Add("BTC", 0);
        }
        public void GetCurrentInfo()
        {
            _terminal.GetTerminalInfo();
        }
        public void GetUserBalance()
        {
            Console.WriteLine("\n Текущий баланс пользователя:");
            foreach ( var key in _money.Keys )
            {
                Console.WriteLine($"{key}: {_money[key]}");
            }
        }
        public void Exchange(string user_curr, string terminal_curr, float money)
        {
            float money2;
            if (!_money.ContainsKey(user_curr))
            {
                throw new WrongCurrency();
            }
            if (money > _money[user_curr])
            {
                throw new NotEnoughtUserMoney();
            }
                
            try
            {
                money2 = _terminal.ExchangeMoney(user_curr, terminal_curr, money);
            }
            catch (NoIncreaseCaurrency) { Console.WriteLine("Валюты, которую вы пытаетесь обменять нет в терминале, порпобуйте ввести еще раз"); return; }
            catch (NoDecreaseCaurrency) { Console.WriteLine("Валюты, на которую вы пытаетесь обменять нет в терминале, порпобуйте ввести еще раз"); return; }
            catch (NotEnougthMoney) { Console.WriteLine("В терминале недостаточно средств, отмена операции..."); return; }
            _money[user_curr] -= money;
            _money[terminal_curr] += money2;
            Console.WriteLine("\n Операция обмена завершема успешно!");
            Console.WriteLine($"Ваши {money} {user_curr} были обменяны на {money2} {terminal_curr}");

        }
        public void GetCurrBalance(string currency)
        {
            if (!_money.ContainsKey(currency))
            {
                throw new WrongCurrency();
            }
            Console.WriteLine($"На баллансе на данный момент {_money[currency]} {currency}");
        }
    }

}
