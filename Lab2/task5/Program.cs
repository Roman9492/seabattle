using System;
using System.Collections.Generic;

namespace BuilderTask
{
    public class Character
    {
        public string Name { get; set; }
        public string Height { get; set; }
        public string Build { get; set; } 
        public string HairColor { get; set; }
        public string EyeColor { get; set; }
        public List<string> Inventory { get; set; } = new List<string>();
        public List<string> Actions { get; set; } = new List<string>(); 

        public void ShowInfo()
        {
            Console.WriteLine($"--- Персонаж: {Name} ---");
            Console.WriteLine($"Зріст: {Height}, Статура: {Build}");
            Console.WriteLine($"Волосся: {HairColor}, Очі: {EyeColor}");
            Console.WriteLine($"Інвентар: {string.Join(", ", Inventory)}");
            Console.WriteLine($"Справи: {string.Join(", ", Actions)}\n");
        }
    }

    public interface ICharacterBuilder
    {
        ICharacterBuilder SetName(string name);
        ICharacterBuilder SetAppearance(string height, string build);
        ICharacterBuilder SetColors(string hair, string eyes);
        ICharacterBuilder AddInventory(string item);
        ICharacterBuilder DoAction(string action);
        Character Build();
    }

    public class HeroBuilder : ICharacterBuilder
    {
        private Character _character = new Character();
        public ICharacterBuilder SetName(string name) { _character.Name = name; return this; }
        public ICharacterBuilder SetAppearance(string h, string b) { _character.Height = h; _character.Build = b; return this; }
        public ICharacterBuilder SetColors(string h, string e) { _character.HairColor = h; _character.EyeColor = e; return this; }
        public ICharacterBuilder AddInventory(string item) { _character.Inventory.Add(item); return this; }
        public ICharacterBuilder DoAction(string action) { _character.Actions.Add("Добро: " + action); return this; }
        public Character Build() => _character;
    }

    public class EnemyBuilder : ICharacterBuilder
    {
        private Character _character = new Character();
        public ICharacterBuilder SetName(string name) { _character.Name = name; return this; }
        public ICharacterBuilder SetAppearance(string h, string b) { _character.Height = h; _character.Build = b; return this; }
        public ICharacterBuilder SetColors(string h, string e) { _character.HairColor = h; _character.EyeColor = e; return this; }
        public ICharacterBuilder AddInventory(string item) { _character.Inventory.Add(item); return this; }
        public ICharacterBuilder DoAction(string action) { _character.Actions.Add("Зло: " + action); return this; }
        public Character Build() => _character;
    }

    public class Director
    {
        public Character Construct(ICharacterBuilder builder)
        {
            return builder.SetName("Стандартний")
                          .SetAppearance("Високий", "Атлетична")
                          .Build();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var hero = new HeroBuilder()
                .SetName("Артур Світлоносний")
                .SetAppearance("190см", "Могутня")
                .SetColors("Золотисте", "Блакитні")
                .AddInventory("Екскалібур")
                .AddInventory("Щит Віри")
                .DoAction("Врятував село від дракона")
                .Build();

            var enemy = new EnemyBuilder()
                .SetName("Моргорт Темний")
                .SetAppearance("210см", "Худорлява")
                .SetColors("Чорне", "Червоні")
                .AddInventory("Посох Хаосу")
                .DoAction("Викрав сонце")
                .Build();

            hero.ShowInfo();
            enemy.ShowInfo();

            Console.ReadKey();
        }
    }
}