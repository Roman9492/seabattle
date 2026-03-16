using System;
using System.Text;

namespace AbstractFactoryTech
{
    public interface ILaptop { void Info(); }
    public interface ISmartphone { void Info(); }
    public interface INetbook { void Info(); }
    public interface IEBook { void Info(); }

    public class IProneLaptop : ILaptop { public void Info() => Console.WriteLine("IProne Laptop: Retina 15', M2 Chip"); }
    public class IPronePhone : ISmartphone { public void Info() => Console.WriteLine("IProne Smartphone: iOS 17, FaceID"); }
    public class IProneNetbook : INetbook { public void Info() => Console.WriteLine("IProne Netbook: Air Edition Light"); }
    public class IProneEBook : IEBook { public void Info() => Console.WriteLine("IProne EBook: iRead Ultra HD"); }

    public class KiaomiLaptop : ILaptop { public void Info() => Console.WriteLine("Kiaomi Laptop: Mi Notebook Pro, Metal Body"); }
    public class KiaomiPhone : ISmartphone { public void Info() => Console.WriteLine("Kiaomi Smartphone: HyperOS, 120W Charging"); }
    public class KiaomiNetbook : INetbook { public void Info() => Console.WriteLine("Kiaomi Netbook: RedmiBook 13"); }
    public class KiaomiEBook : IEBook { public void Info() => Console.WriteLine("Kiaomi EBook: Mi Reader Paper"); }

    public class BalaxyLaptop : ILaptop { public void Info() => Console.WriteLine("Balaxy Laptop: Galaxy Book OLED"); }
    public class BalaxyPhone : ISmartphone { public void Info() => Console.WriteLine("Balaxy Smartphone: Android 14, S-Pen"); }
    public class BalaxyNetbook : INetbook { public void Info() => Console.WriteLine("Balaxy Netbook: Tab S Ultra Hybrid"); }
    public class BalaxyEBook : IEBook { public void Info() => Console.WriteLine("Balaxy EBook: E-Ink Display Plus"); }

    public interface ITechFactory
    {
        ILaptop CreateLaptop();
        ISmartphone CreateSmartphone();
        INetbook CreateNetbook();
        IEBook CreateEBook();
    }

    public class IProneFactory : ITechFactory
    {
        public ILaptop CreateLaptop() => new IProneLaptop();
        public ISmartphone CreateSmartphone() => new IPronePhone();
        public INetbook CreateNetbook() => new IProneNetbook();
        public IEBook CreateEBook() => new IProneEBook();
    }

    public class KiaomiFactory : ITechFactory
    {
        public ILaptop CreateLaptop() => new KiaomiLaptop();
        public ISmartphone CreateSmartphone() => new KiaomiPhone();
        public INetbook CreateNetbook() => new KiaomiNetbook();
        public IEBook CreateEBook() => new KiaomiEBook();
    }

    public class BalaxyFactory : ITechFactory
    {
        public ILaptop CreateLaptop() => new BalaxyLaptop();
        public ISmartphone CreateSmartphone() => new BalaxyPhone();
        public INetbook CreateNetbook() => new BalaxyNetbook();
        public IEBook CreateEBook() => new BalaxyEBook();
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("===== ЗАПУСК ФАБРИКИ ТЕХНІКИ =====");

            Console.WriteLine("\n--- Виробництво бренду: IProne ---");
            ITechFactory iprone = new IProneFactory();
            iprone.CreateLaptop().Info();
            iprone.CreateSmartphone().Info();
            iprone.CreateEBook().Info();

            Console.WriteLine("\n--- Виробництво бренду: Kiaomi ---");
            ITechFactory kiaomi = new KiaomiFactory();
            kiaomi.CreateSmartphone().Info();
            kiaomi.CreateNetbook().Info();

            Console.WriteLine("\n--- Виробництво бренду: Balaxy ---");
            ITechFactory balaxy = new BalaxyFactory();
            balaxy.CreateLaptop().Info();
            balaxy.CreateNetbook().Info();
            balaxy.CreateEBook().Info();

            Console.WriteLine("\n==================================");
            Console.WriteLine("Всі девайси успішно створено!");
            Console.WriteLine("Натисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}