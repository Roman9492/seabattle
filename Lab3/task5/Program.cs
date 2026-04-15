using System;
using System.Collections.Generic;
using System.Text;

namespace task5
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    public class AddClassCommand : ICommand
    {
        private readonly LightElementNode _node;
        private readonly string _className;

        public AddClassCommand(LightElementNode node, string className)
        {
            _node = node;
            _className = className;
        }

        public void Execute() => _node.AddClass(_className);
        public void Undo() => _node.RemoveClass(_className);
    }

    public abstract class LightNode
    {
        public abstract string OuterHTML();
        public abstract string InnerHTML();
        public virtual List<LightNode> GetChildren() => new List<LightNode>();
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
        private List<string> _cssClasses = new List<string>();
        private List<LightNode> _children = new List<LightNode>();

        public LightElementNode(string tagName) => _tagName = tagName;

        public void AddChild(LightNode node) => _children.Add(node);
        public override List<LightNode> GetChildren() => _children;

        public void AddClass(string className) => _cssClasses.Add(className);
        public void RemoveClass(string className) => _cssClasses.Remove(className);

        public override string InnerHTML()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var child in _children) sb.Append(child.OuterHTML());
            return sb.ToString();
        }

        public override string OuterHTML()
        {
            string classes = _cssClasses.Count > 0 ? $" class=\"{string.Join(" ", _cssClasses)}\"" : "";
            return $"<{_tagName}{classes}>{InnerHTML()}</{_tagName}>";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var div = new LightElementNode("div");
            div.AddChild(new LightTextNode("Текст з командою"));

            Console.WriteLine("До команди: " + div.OuterHTML());

            var command = new AddClassCommand(div, "highlight");
            command.Execute();
            Console.WriteLine("Після Execute: " + div.OuterHTML());

            command.Undo();
            Console.WriteLine("Після Undo: " + div.OuterHTML());

            Console.ReadKey();
        }
    }
}
