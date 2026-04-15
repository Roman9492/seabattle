using System;
using System.Collections.Generic;
using System.Text;

namespace task5
{
    public interface IVisitor
    {
        void VisitTextNode(LightTextNode textNode);
        void VisitElementNode(LightElementNode elementNode);
    }

    public class WordCountVisitor : IVisitor
    {
        public int TotalChars { get; private set; } = 0;

        public void VisitTextNode(LightTextNode textNode)
        {
            TotalChars += textNode.GetText().Length;
        }

        public void VisitElementNode(LightElementNode elementNode)
        {
        }
    }

    public abstract class LightNode
    {
        public abstract string OuterHTML();
        public abstract string InnerHTML();
        public virtual List<LightNode> GetChildren() => new List<LightNode>();
        
        public abstract void Accept(IVisitor visitor);
    }

    public class LightTextNode : LightNode
    {
        private readonly string _text;
        public LightTextNode(string text) => _text = text;
        public string GetText() => _text;

        public override string InnerHTML() => _text;
        public override string OuterHTML() => _text;

        public override void Accept(IVisitor visitor) => visitor.VisitTextNode(this);
    }

    public class LightElementNode : LightNode
    {
        public string TagName { get; }
        private List<LightNode> _children = new List<LightNode>();

        public LightElementNode(string tagName) => TagName = tagName;
        public void AddChild(LightNode node) => _children.Add(node);
        public override List<LightNode> GetChildren() => _children;

        public override string InnerHTML()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var child in _children) sb.Append(child.OuterHTML());
            return sb.ToString();
        }

        public override string OuterHTML() => $"<{TagName}>{InnerHTML()}</{TagName}>";

        public override void Accept(IVisitor visitor)
        {
            visitor.VisitElementNode(this);
            foreach (var child in _children)
            {
                child.Accept(visitor);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var body = new LightElementNode("body");
            var p = new LightElementNode("p");
            p.AddChild(new LightTextNode("Привіт, світ!"));
            body.AddChild(p);

            var statsVisitor = new WordCountVisitor();
            body.Accept(statsVisitor);

            Console.WriteLine(body.OuterHTML());
            Console.WriteLine($"\n[Статистика]: Всього символів у тексті: {statsVisitor.TotalChars}");

            Console.ReadKey();
        }
    }
}
