using System;
using System.Collections.Generic;
using System.Text;

namespace task5
{
    public abstract class LightNode
    {
        public abstract string OuterHTML();
        public abstract string InnerHTML();
        public virtual void OnBeforeRender() { }
        public virtual void OnAfterRender() { }
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
        protected string _tagName; 
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
            OnBeforeRender(); 

            string classes = _cssClasses.Count > 0 ? $" class=\"{string.Join(" ", _cssClasses)}\"" : "";
            string result;

            if (_isSelfClosing)
            {
                result = $"<{_tagName}{classes} />";
            }
            else
            {
                result = $"<{_tagName}{classes}>\n  {InnerHTML()}\n</{_tagName}>";
            }

            OnAfterRender();
            return result;
        }
    }

    public class LoggingElementNode : LightElementNode
    {
        public LoggingElementNode(string tagName, string displayType, bool isSelfClosing) 
            : base(tagName, displayType, isSelfClosing) { }

        public override void OnBeforeRender() => Console.WriteLine($"[LOG]: Початок рендерингу <{_tagName}>");
        public override void OnAfterRender() => Console.WriteLine($"[LOG]: Завершено рендеринг <{_tagName}>");
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            
            var list = new LoggingElementNode("ul", "block", false);
            list.AddChild(new LightTextNode("Демонстрація Template Method"));

            Console.WriteLine(list.OuterHTML());
            Console.ReadKey();
        }
    }
}
