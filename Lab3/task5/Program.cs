using System;
using System.Collections.Generic;
using System.Text;

namespace task5
{
    public interface IIterator
    {
        LightNode Next();
        bool HasNext();
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
        public override List<LightNode> GetChildren() => _children;

        public override string InnerHTML()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var child in _children) sb.Append(child.OuterHTML());
            return sb.ToString();
        }

        public override string OuterHTML()
        {
            OnBeforeRender();
            string classes = _cssClasses.Count > 0 ? $" class=\"{string.Join(" ", _cssClasses)}\"" : "";
            string result = _isSelfClosing ? $"<{_tagName}{classes} />" : $"<{_tagName}{classes}>\n  {InnerHTML()}\n</{_tagName}>";
            OnAfterRender();
            return result;
        }
    }

    public class DepthFirstIterator : IIterator
    {
        private Stack<LightNode> _stack = new Stack<LightNode>();
        public DepthFirstIterator(LightNode root) => _stack.Push(root);
        public bool HasNext() => _stack.Count > 0;
        public LightNode Next()
        {
            var node = _stack.Pop();
            var children = node.GetChildren();
            for (int i = children.Count - 1; i >= 0; i--) _stack.Push(children[i]);
            return node;
        }
    }

    public class BreadthFirstIterator : IIterator
    {
        private Queue<LightNode> _queue = new Queue<LightNode>();
        public BreadthFirstIterator(LightNode root) => _queue.Enqueue(root);
        public bool HasNext() => _queue.Count > 0;
        public LightNode Next()
        {
            var node = _queue.Dequeue();
            foreach (var child in node.GetChildren()) _queue.Enqueue(child);
            return node;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var list = new LightElementNode("ul", "block", false);
            var item1 = new LightElementNode("li", "block", false);
            item1.AddChild(new LightTextNode("Перший"));
            list.AddChild(item1);
            list.AddChild(new LightElementNode("li", "block", false));

            Console.WriteLine("--- Тест Ітератора DFS (В глибину) ---");
            var dfs = new DepthFirstIterator(list);
            while (dfs.HasNext()) Console.WriteLine("Вузол: " + dfs.Next().GetType().Name);

            Console.WriteLine("\n--- Тест Ітератора BFS (В ширину) ---");
            var bfs = new BreadthFirstIterator(list);
            while (bfs.HasNext()) Console.WriteLine("Вузол: " + bfs.Next().GetType().Name);

            Console.ReadKey();
        }
    }
}
