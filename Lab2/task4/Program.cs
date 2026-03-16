using System;
using System.Collections.Generic;

namespace PrototypeTask
{
    public interface IPrototype<T>
    {
        T Clone();
    }

    public class Virus : IPrototype<Virus>
    {
        public double Weight { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public List<Virus> Children { get; set; }

        public Virus(string name, string species, double weight, int age)
        {
            Name = name;
            Species = species;
            Weight = weight;
            Age = age;
            Children = new List<Virus>();
        }

        public Virus Clone()
        {
            Virus clone = new Virus(this.Name + " (Clone)", this.Species, this.Weight, this.Age);

            foreach (var child in this.Children)
            {
                clone.Children.Add(child.Clone());
            }

            return clone;
        }

        public void PrintStructure(string indent = "")
        {
            Console.WriteLine($"{indent}V- {Name} ({Species}, Age: {Age})");
            foreach (var child in Children)
            {
                child.PrintStructure(indent + "  ");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Virus grandfather = new Virus("Alpha-Prime", "Root", 1.5, 10);

            Virus father = new Virus("Beta-1", "Sub", 0.8, 5);
            Virus child = new Virus("Gamma-1.1", "Nano", 0.2, 1);

            father.Children.Add(child);
            grandfather.Children.Add(father);

            Console.WriteLine("=== ОРИГІНАЛЬНЕ СІМЕЙСТВО ===");
            grandfather.PrintStructure();

            Virus cloneGrandfather = grandfather.Clone();

            Console.WriteLine("\n=== КЛОНОВАНЕ СІМЕЙСТВО ===");
            cloneGrandfather.PrintStructure();

            Console.WriteLine("\n--- Перевірка незалежності ---");
            cloneGrandfather.Children[0].Name = "MODIFIED-Beta";
            Console.WriteLine($"Оригінальний син: {grandfather.Children[0].Name}");
            Console.WriteLine($"Клонований син: {cloneGrandfather.Children[0].Name}");

            Console.ReadKey();
        }
    }
}