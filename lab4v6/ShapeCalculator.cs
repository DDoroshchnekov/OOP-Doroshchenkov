using System;
using System.Collections.Generic;

namespace lab4v6
{
    public class ShapeCalculator
    {
        private List<Shape> shapes = new List<Shape>();

        public void AddShape(Shape shape)
        {
            shapes.Add(shape);
        }

        public void DisplayAllAreas()
        {
            Console.WriteLine("\nСписок фігур та їх площ:");
            foreach (var shape in shapes)
            {
                Console.WriteLine(shape);
            }
        }
    }
}
