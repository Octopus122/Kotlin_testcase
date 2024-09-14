// See https://aka.ms/new-console-template for more information
using System.Reflection;
using System.IO;
using System;
using User_class;
using Terminal_class;

ITerminal terminal = new Terminal();
IUser user = new User(terminal);
Console.WriteLine("=========== Стартовая информация об активах пользователя ===========");
user.GetUserBalance();
Console.WriteLine("============= Стартовая инфомация об бменном терминале =============");
user.GetCurrentInfo();
Console.WriteLine("======================== Приятных обменов) ========================");

string Command;
do
{
    
    Console.WriteLine("Введите команду:");
    Command = Console.ReadLine();
    Command = Command.ToUpper();
    try
    {
        if (Command == "BALANCE") user.GetUserBalance();

        if (Command == "INFO") user.GetCurrentInfo();

        if (Command == "EXCHANGE")
        {
            Console.WriteLine("Введите валюту, которую хотите обменять >>");
            string curr = Console.ReadLine().ToUpper();
            Console.WriteLine("Введите количество, которое хотите обменять >>");
            float money = float.Parse(Console.ReadLine());
            Console.WriteLine("Введите валюту, на которую хотите поменять >>");
            user.Exchange(curr, Console.ReadLine().ToUpper(), money);
            terminal.ChangeAll();
        }
        
    }
    catch (NotEnoughtUserMoney) { Console.WriteLine("На счету недостаточно средств для данного обмена. Отмена операции..."); }
    catch (WrongCurrency) { Console.WriteLine("Ввдена неверная валюта. Отмена операции.."); }
    
}
while (Command != "");