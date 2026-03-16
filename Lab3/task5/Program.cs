using System;
using System.Collections.Generic;
using System.Text;

namespace task5
{
    public abstract class LightNode
    {
        public abstract string OuterHTML();
        public abstract string InnerHTML();
    }

    public class LightTextNode : LightNode
    {
        private readonly string _text;
        public LightTextNode(string text) => _text = text;

        public override string InnerHTML() => _text;
        public override string OuterHTML() => _text;
    }

    public class LightElementNode : LightNode
    {
        private string _tagName;
        private string _displayType; 
        private bool _isSelfClosing;
        private List<string> _cssClasses = new List<string>();
        private List<LightNode> _children = new List<LightNode>();

        public LightElementNode(string tagName, string displayType, bool isSelfClosing, List<string> cssClasses = null)
        {
            _tagName = tagName;
            _displayType = displayType;
            _isSelfClosing = isSelfClosing;
            if (cssClasses != null) _cssClasses = cssClasses;
        }

        public void AddChild(LightNode node) => _children.Add(node);

        public override string InnerHTML()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var child in _children)
            {
                sb.Append(child.OuterHTML());
            }
            return sb.ToString();
        }

        public override string OuterHTML()
        {
            string classes = _cssClasses.Count > 0 ? $" class=\"{string.Join(" ", _cssClasses)}\"" : "";

            if (_isSelfClosing)
            {
                return $"<{_tagName}{classes} />";
            }

            return $"<{_tagName}{classes}>\n  {InnerHTML()}\n</{_tagName}>";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var list = new LightElementNode("ul", "block", false, new List<string> { "main-list", "dark-theme" });

            var item1 = new LightElementNode("li", "block", false);
            item1.AddChild(new LightTextNode("Елемент 1: Основи C#"));

            var item2 = new LightElementNode("li", "block", false);
            item2.AddChild(new LightTextNode("Елемент 2: Шаблони проєктування"));

            var line = new LightElementNode("hr", "block", true);

            list.AddChild(item1);
            list.AddChild(item2);
            list.AddChild(line);

            Console.WriteLine("--- Тестування LightHTML (Composite Pattern) ---");
            Console.WriteLine("\n[OUTER HTML]:");
            Console.WriteLine(list.OuterHTML());

            Console.WriteLine("\n[INNER HTML]:");
            Console.WriteLine(list.InnerHTML());

            Console.WriteLine("\nНатисніть будь-яку клавішу для завершення...");
            Console.ReadKey();
        }
    }
}