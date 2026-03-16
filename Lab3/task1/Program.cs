using System;
using System.IO;

namespace Lab3Task1
{
    public class Logger
    {
        public virtual void Log(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[LOG]: {message}");
            Console.ResetColor();
        }

        public virtual void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR]: {message}");
            Console.ResetColor();
        }

        public virtual void Warn(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow; 
            Console.WriteLine($"[WARN]: {message}");
            Console.ResetColor();
        }
    }

    public class FileWriter
    {
        private string _filePath;

        public FileWriter(string filePath)
        {
            _filePath = filePath;
        }

        public void Write(string text)
        {
            File.AppendAllText(_filePath, text);
        }

        public void WriteLine(string text)
        {
            File.AppendAllText(_filePath, text + Environment.NewLine);
        }
    }


    public class FileLoggerAdapter : Logger
    {
        private readonly FileWriter _fileWriter;

        public FileLoggerAdapter(FileWriter fileWriter)
        {
            _fileWriter = fileWriter;
        }

        public override void Log(string message)
        {
            _fileWriter.WriteLine($"[FILE-LOG]: {message} (Time: {DateTime.Now})");
        }

        public override void Error(string message)
        {
            _fileWriter.WriteLine($"[FILE-ERROR]: {message} (Time: {DateTime.Now})");
        }

        public override void Warn(string message)
        {
            _fileWriter.WriteLine($"[FILE-WARN]: {message} (Time: {DateTime.Now})");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Logger consoleLogger = new Logger();
            consoleLogger.Log("Це звичайне повідомлення");
            consoleLogger.Warn("Це попередження");
            consoleLogger.Error("Це помилка");

            Console.WriteLine("\n--- Перемикаємось на файловий логер ---\n");

            FileWriter writer = new FileWriter("log.txt");
            Logger fileLogger = new FileLoggerAdapter(writer);

            fileLogger.Log("Це повідомлення піде у файл");
            fileLogger.Warn("Це попередження також у файл");
            fileLogger.Error("Помилка записана у log.txt");

            Console.WriteLine("Перевірте файл log.txt у папці з програмою.");
            Console.ReadKey();
        }
    }
}