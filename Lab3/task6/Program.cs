using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace task6
{
    public class ElementInfo
    {
        public string TagName { get; }
        public string DisplayType { get; }
        public bool IsSelfClosing { get; }

        public ElementInfo(string tagName, string displayType, bool isSelfClosing)
        {
            TagName = tagName;
            DisplayType = displayType;
            IsSelfClosing = isSelfClosing;
        }
    }

    public class ElementFactory
    {
        private static Dictionary<string, ElementInfo> _elements = new Dictionary<string, ElementInfo>();

        public static ElementInfo GetElementInfo(string tag, string display, bool selfClosing)
        {
            string key = $"{tag}_{display}_{selfClosing}";
            if (!_elements.ContainsKey(key))
            {
                _elements[key] = new ElementInfo(tag, display, selfClosing);
            }
            return _elements[key];
        }
    }

    public abstract class LightNode
    {
        public abstract long GetSize();
        public abstract string Render();
    }

    public class LightTextNode : LightNode
    {
        private string _text;
        public LightTextNode(string text) => _text = text;
        public override string Render() => _text;
        public override long GetSize() => _text.Length * sizeof(char);
    }

    public class LightElementNode : LightNode
    {
        private ElementInfo _info;
        private List<LightNode> _children = new List<LightNode>();

        public LightElementNode(ElementInfo info) => _info = info;

        public void AddChild(LightNode node) => _children.Add(node);

        public override string Render()
        {
            var content = string.Join("", _children.Select(c => c.Render()));
            if (_info.IsSelfClosing) return $"<{_info.TagName} />\n";

            return $"<{_info.TagName}>{content}</{_info.TagName}>\n";
        }

        public override long GetSize()
        {
            return 32 + (_children.Count * 8) + _children.Sum(c => c.GetSize());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            string[] bookLines = {
                "ACT V",                                               
                "Scene I. Mantua. A Street.",                         
                "Dramatis Personae",                                    
                " ESCALUS, Prince of Verona.",                          
                "MERCUTIO, kinsman to the Prince, and friend to Romeo." 
            };

            var root = new LightElementNode(ElementFactory.GetElementInfo("div", "block", false));

            for (int i = 0; i < bookLines.Length; i++)
            {
                string line = bookLines[i];
                ElementInfo info;

                if (i == 0) 
                    info = ElementFactory.GetElementInfo("h1", "block", false);

                else if (line.StartsWith(" ")) 
                    info = ElementFactory.GetElementInfo("blockquote", "block", false);

                else if (line.Length < 30) 
                    info = ElementFactory.GetElementInfo("h2", "block", false);

                else 
                    info = ElementFactory.GetElementInfo("p", "block", false);

                var element = new LightElementNode(info);
                element.AddChild(new LightTextNode(line.Trim()));
                root.AddChild(element);
            }

            Console.WriteLine("--- Оптимізована HTML Верстка Книги ---\n");
            Console.WriteLine(root.Render());

            long sizeWithFlyweight = root.GetSize();
            long sizeWithoutFlyweight = sizeWithFlyweight + (bookLines.Length * 24);

            Console.WriteLine("--- Аналіз пам'яті ---");
            Console.WriteLine($"Розмір БЕЗ Flyweight: {sizeWithoutFlyweight} байт");
            Console.WriteLine($"Розмір З Flyweight: {sizeWithFlyweight} байт");
            Console.WriteLine($"Економія: {sizeWithoutFlyweight - sizeWithFlyweight} байт");

            Console.ReadKey();
        }
    }
}