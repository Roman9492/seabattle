using System;
using System.Collections.Generic;
using System.Text;

namespace task5
{
    public interface INodeState
    {
        string Render(LightElementNode context);
    }

    public class NormalState : INodeState
    {
        public string Render(LightElementNode context)
        {
            string classes = context.GetClassesCount() > 0 ? $" class=\"{context.GetClassesString()}\"" : "";
            return $"<{context.TagName}{classes}>\n  {context.InnerHTML()}\n</{context.TagName}>";
        }
    }

    public class HiddenState : INodeState
    {
        public string Render(LightElementNode context) => ""; 
    }

    public abstract class LightNode
    {
        public abstract string OuterHTML();
        public abstract string InnerHTML();
        public virtual List<LightNode> GetChildren() => new List<LightNode>();
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
        public string TagName { get; }
        private List<string> _cssClasses = new List<string>();
        private List<LightNode> _children = new List<LightNode>();
        
        private INodeState _state;

        public LightElementNode(string tagName)
        {
            TagName = tagName;
            _state = new NormalState(); 
        }

        public void SetState(INodeState state) => _state = state;
        public void AddChild(LightNode node) => _children.Add(node);
        public override List<LightNode> GetChildren() => _children;
        public int GetClassesCount() => _cssClasses.Count;
        public string GetClassesString() => string.Join(" ", _cssClasses);

        public override string InnerHTML()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var child in _children) sb.Append(child.OuterHTML());
            return sb.ToString();
        }

        public override string OuterHTML() => _state.Render(this);
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var div = new LightElementNode("div");
            div.AddChild(new LightTextNode("Цей текст може зникнути!"));

            Console.WriteLine("--- Поточний стан: Normal ---");
            Console.WriteLine(div.OuterHTML());

            div.SetState(new HiddenState());
            Console.WriteLine("\n--- Поточний стан: Hidden ---");
            Console.WriteLine("Результат: " + div.OuterHTML() + "(порожньо)");

            Console.ReadKey();
        }
    }
}
