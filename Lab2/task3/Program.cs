using System;

namespace SingletonTask
{
    public sealed class Authenticator
    {
        private static Authenticator _instance;

        private static readonly object _lock = new object();

        private Authenticator()
        {
            Console.WriteLine("--- Система автентифікації активована (створено екземпляр) ---");
        }

        public static Authenticator GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Authenticator();
                    }
                }
            }
            return _instance;
        }

        public void LogMessage(string message)
        {
            Console.WriteLine($"[Auth Log]: {message}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Перевірка шаблону Одинак (Singleton) ===\n");

            Authenticator auth1 = Authenticator.GetInstance();
            auth1.LogMessage("Користувач Roman намагається увійти.");

            Authenticator auth2 = Authenticator.GetInstance();
            auth2.LogMessage("Спроба входу від іншого сервісу.");

           if (ReferenceEquals(auth1, auth2))
            {
                Console.WriteLine("\nРЕЗУЛЬТАТ: auth1 та auth2 посилаються на ОДИН і той самий об'єкт.");
            }
            else
            {
                Console.WriteLine("\nПОМИЛКА: Створено різні об'єкти!");
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу...");
            Console.ReadKey();
        }
    }
}