using System;

namespace task3
{
    public interface IRenderer
    {
        string RenderAs();
    }

    public class VectorRenderer : IRenderer
    {
        public string RenderAs() => "як вектори (lines)";
    }

    public class RasterRenderer : IRenderer
    {
        public string RenderAs() => "як пікселі (pixels)";
    }

    public abstract class Shape
    {
        protected IRenderer _renderer;

        protected Shape(IRenderer renderer)
        {
            _renderer = renderer;
        }

        public abstract void Draw();
    }

    public class Circle : Shape
    {
        public Circle(IRenderer renderer) : base(renderer) { }
        public override void Draw() =>
            Console.WriteLine($"Drawing Circle {_renderer.RenderAs()}");
    }

    public class Square : Shape
    {
        public Square(IRenderer renderer) : base(renderer) { }
        public override void Draw() =>
            Console.WriteLine($"Drawing Square {_renderer.RenderAs()}");
    }

    public class Triangle : Shape
    {
        public Triangle(IRenderer renderer) : base(renderer) { }
        public override void Draw() =>
            Console.WriteLine($"Drawing Triangle {_renderer.RenderAs()}");
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            IRenderer vector = new VectorRenderer();
            IRenderer raster = new RasterRenderer();

            Console.WriteLine("--- Тестування графічного редактора ---");

            Shape triangle = new Triangle(raster);
            triangle.Draw();

            Shape circle = new Circle(vector);
            circle.Draw();

            Shape square = new Square(raster);
            square.Draw();

            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}