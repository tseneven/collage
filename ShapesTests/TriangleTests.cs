using prackt4;

namespace ShapesTests
{
    public class TriangleTests
    {
        [Fact]
        public void ZeroSideDataToArea()
        {
            Triangle triangle = new Triangle();
            triangle.SideA = 0;
            triangle.SideB = 10;
            triangle.SideC = 10;

            Assert.Throws<ArgumentException>(() => triangle.Area());

        }

        [Fact]
        public void ZeroSideDataToPerimeter()
        {
            Triangle triangle = new Triangle();
            triangle.SideA = 0;
            triangle.SideB = 10;
            triangle.SideC = 10;

            Assert.Throws<ArgumentException>(() => triangle.Perimeter());

        }

        [Fact]
        public void CorrectDataToPerimeter()
        {
            Triangle triangle = new Triangle();
            triangle.SideA = 10;
            triangle.SideB = 10;
            triangle.SideC = 10;
            double result = 30;

            Assert.Equal(result, triangle.Perimeter());

        }

        [Fact]
        public void CorrectDataToArea()
        {
            Triangle triangle = new Triangle();
            triangle.SideA = 10;
            triangle.SideB = 10;
            triangle.SideC = 10;
            double result = 10;

            Assert.Equal(result, triangle.Area());

        }
    }
}