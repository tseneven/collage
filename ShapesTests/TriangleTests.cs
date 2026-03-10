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
    }
}