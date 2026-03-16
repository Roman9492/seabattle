using System;

namespace Lab3Task2
{
    public abstract class Hero
    {
        public abstract string Name { get; }
        public abstract int GetAttack();
        public abstract int GetDefense();

        public virtual void ShowStats()
        {
            Console.WriteLine($"{Name}: Атака = {GetAttack()}, Захист = {GetDefense()}");
        }
    }

    public class Warrior : Hero
    {
        public override string Name => "Воїн";
        public override int GetAttack() => 15;
        public override int GetDefense() => 10;
    }

    public class Mage : Hero
    {
        public override string Name => "Маг";
        public override int GetAttack() => 25;
        public override int GetDefense() => 5;
    }

    public class Paladin : Hero
    {
        public override string Name => "Паладин";
        public override int GetAttack() => 12;
        public override int GetDefense() => 15;
    }

    public abstract class InventoryDecorator : Hero
    {
        protected Hero _hero;

        public InventoryDecorator(Hero hero)
        {
            _hero = hero;
        }
    }

    public class ArmorDecorator : InventoryDecorator
    {
        public ArmorDecorator(Hero hero) : base(hero) { }
        public override string Name => _hero.Name + " в залізній броні";
        public override int GetAttack() => _hero.GetAttack(); 
        public override int GetDefense() => _hero.GetDefense() + 20; 
    }

    public class SwordDecorator : InventoryDecorator
    {
        public SwordDecorator(Hero hero) : base(hero) { }
        public override string Name => _hero.Name + " з магічним мечем";
        public override int GetAttack() => _hero.GetAttack() + 15;
        public override int GetDefense() => _hero.GetDefense();
    }

    public class ArtifactDecorator : InventoryDecorator
    {
        public ArtifactDecorator(Hero hero) : base(hero) { }
        public override string Name => _hero.Name + " з артефактом сили";
        public override int GetAttack() => _hero.GetAttack() + 10;
        public override int GetDefense() => _hero.GetDefense() + 10;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== Створення героїв ===");

            Hero myHero = new Warrior();
            myHero.ShowStats();

            myHero = new ArmorDecorator(myHero);
            myHero.ShowStats();

            myHero = new SwordDecorator(myHero);
            myHero.ShowStats();

            myHero = new ArtifactDecorator(myHero);
            myHero.ShowStats();

            Console.WriteLine("\n=== Створення прокачаного Мага ===");
            Hero myMage = new Mage();
            myMage = new ArtifactDecorator(myMage);
            myMage = new ArtifactDecorator(myMage);
            myMage.ShowStats();

            Console.ReadKey();
        }
    }
}