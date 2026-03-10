namespace prackt4
{
    public class Triangle : Shape
    {
        public double SideA { get; set; }
        public double SideB { get; set; }
        public double SideC { get; set; }

        public override double Area()
        {
            if (SideA < 0.1 || SideB < 0.1 || SideC < 0.1)
            {
                throw new ArgumentException("Неверные входные данные");
            }

            double p = (SideA + SideB + SideC) / 2;

            return Math.Sqrt(p * (p - SideA) * (p - SideB) * (p-SideC));
        }

        public override double Perimeter()
        {
            if (SideA < 0.1 || SideB < 0.1 || SideC < 0.1)
            {
                throw new ArgumentException("Неверные входные данные");
            }

            return (SideA + SideB + SideC);
        }

        public override string ToString()
        {
            return $"SideA: {SideA} SideB: {SideB} SideC: {SideC}";
        }
    }
}
