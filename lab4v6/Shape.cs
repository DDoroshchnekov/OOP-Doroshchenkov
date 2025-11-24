namespace lab4v6
{
    public abstract class Shape : IArea
    {
        public string Name { get; set; }

        protected Shape(string name)
        {
            Name = name;
        }

        public abstract double CalculateArea();

        public override string ToString()
        {
            return $"{Name} (площа = {CalculateArea():F2})";
        }
    }
}
