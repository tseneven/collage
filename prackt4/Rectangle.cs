namespace prackt4
{
    public class Rectangle : Shape
    {
        public double width { get; set; }
        public double height { get; set; }
        public override double Area()
        {
            if (width < 0.1 || height < 0.1)
            {
                throw new ArgumentException("Неверные входные данные");
            }
            return width * height;
        }

        public override double Perimeter()
        {
            if (width < 0.1 || height < 0.1)
            {
                throw new ArgumentException("Неверные входные данные");
            }
            return (2 * width) + (2 * height);
        }

        public override string ToString()
        {
            return $"width: {width} height: {height}";
        }
    }
}
