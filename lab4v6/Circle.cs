using System;

namespace lab4v6
{
    public class Circle : Shape
    {
        public double Radius { get; set; }

        public Circle(double radius) : base("Коло")
        {
            Radius = radius;
        }

        public override double CalculateArea()
        {
            return Math.PI * Radius * Radius;
        }
    }
}
