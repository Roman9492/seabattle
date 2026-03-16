using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace task4
{
    public interface ITextReader
    {
        char[][] ReadFile(string filePath);
    }

    public class SmartTextReader : ITextReader
    {
        public char[][] ReadFile(string filePath)
        {
            string[] lines = { "Hello World", "Proxy Pattern", "C# Programming" };

            char[][] result = new char[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                result[i] = lines[i].ToCharArray();
            }
            return result;
        }
    }

    public class SmartTextChecker : ITextReader
    {
        private readonly SmartTextReader _realReader;

        public SmartTextChecker(SmartTextReader reader)
        {
            _realReader = reader;
        }

        public char[][] ReadFile(string filePath)
        {
            Console.WriteLine($"[LOG]: Спроба відкриття файлу: {filePath}");

            var result = _realReader.ReadFile(filePath);

            Console.WriteLine($"[LOG]: Файл успішно прочитано та закрито.");

            int totalChars = 0;
            foreach (var line in result) totalChars += line.Length;

            Console.WriteLine($"[STAT]: Кількість рядків: {result.Length}");
            Console.WriteLine($"[STAT]: Загальна кількість символів: {totalChars}");

            return result;
        }
    }

    public class SmartTextReaderLocker : ITextReader
    {
        private readonly ITextReader _reader;
        private readonly Regex _lockPattern;

        public SmartTextReaderLocker(ITextReader reader, string pattern)
        {
            _reader = reader;
            _lockPattern = new Regex(pattern);
        }

        public char[][] ReadFile(string filePath)
        {
            if (_lockPattern.IsMatch(filePath))
            {
                Console.WriteLine($"[SECURITY]: Access denied! Файл '{filePath}' заблоковано фільтром.");
                return null;
            }
            return _reader.ReadFile(filePath);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            SmartTextReader realReader = new SmartTextReader();

            Console.WriteLine("--- Тест SmartTextChecker (Логування) ---");
            ITextReader checker = new SmartTextChecker(realReader);
            checker.ReadFile("data.txt");

            Console.WriteLine("\n--- Тест SmartTextReaderLocker (Захист) ---");
            ITextReader locker = new SmartTextReaderLocker(checker, ".*secret.*");

            Console.WriteLine("1. Спроба відкрити звичайний файл:");
            locker.ReadFile("public_report.txt");

            Console.WriteLine("\n2. Спроба відкрити секретний файл:");
            locker.ReadFile("my_secret_passwords.txt");

            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}