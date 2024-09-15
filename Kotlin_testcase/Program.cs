// See https://aka.ms/new-console-template for more information

using User_class;
using Terminal_class;


string infoline = "Доступные команды: \n info - получение информации о текущем курсе валют и наличии их в терминале" +
            "\n balance - получение информации о валютах на счету у пользователя " +
            "\n exchange - операция обмена" +
            "\n exit - выход из системы обмена, завершение сеанса" +
            "\n help - получение справки о доступных операциях";
ITerminal terminal = new Terminal();
IUser user = new User(terminal);
Console.WriteLine("=========== Стартовая информация об активах пользователя ===========");
user.GetUserBalance();
Console.WriteLine("============= Стартовая инфомация об бменном терминале =============");
user.GetCurrentInfo();
Console.WriteLine("============= Доступные команды =============");
Console.WriteLine(infoline);
Console.WriteLine("======================== Приятных обменов) ========================");

string Command;
do
{
    
    Console.WriteLine("Введите команду:");
    Command = Console.ReadLine();
    Command = Command.ToUpper();
    try
    {
        if (Command == "BALANCE")
        {
            user.GetUserBalance();
            continue;
        } 

        if (Command == "INFO")
        {
            user.GetCurrentInfo();
            continue;
        }
            

        if (Command == "EXCHANGE")
        {
            Console.WriteLine("Введите валюту, которую хотите обменять >>");
            string curr = Console.ReadLine().ToUpper();
            Console.WriteLine("Введите количество, которое хотите обменять >>");
            float money = float.Parse(Console.ReadLine());
            Console.WriteLine("Введите валюту, на которую хотите поменять >>");
            user.Exchange(curr, Console.ReadLine().ToUpper(), money);
            terminal.ChangeAll();
            continue;
        }
        if (Command == "EXIT")
        {
            Console.WriteLine("Выход из системы обмена...");
            break;
        }
        if (Command == "HELP")
        {
            Console.WriteLine(infoline);
        }
        Console.WriteLine("Такой команды не существует."+ infoline);
        
        
    }
    catch (NotEnoughtUserMoney) { Console.WriteLine("На счету недостаточно средств для данного обмена. Отмена операции..."); }
    catch (WrongCurrency) { Console.WriteLine("Ввдена неверная валюта. Отмена операции..."); }
    catch (NegativeExchange) { Console.WriteLine("Введена отрицательная сумма для обмена. Отмена операции..."); }

}
while (Command != "");