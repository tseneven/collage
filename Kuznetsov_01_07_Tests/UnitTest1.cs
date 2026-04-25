using Kuznetsov_01_07;

namespace Kuznetsov_01_07_Tests
{
    public class UnitTest1
    {
        [Fact]
        public void CreateValidCarBase()
        {
            Car mainCars = new Car();
            Car car = new Car("BMW", 2145, 12454);

            Assert.Equal(mainCars.addList(car), true);
        }

        [Fact]
        public void CreateInvalidNameCarBase()
        {
            Car mainCars = new Car();
            Car car = new Car("", 2145, 12454);

            Assert.Equal(mainCars.addList(car), false);
        }
        [Fact]
        public void CreateInvalidCapacityCarBase()
        {
            Car mainCars = new Car();
            Car car = new Car("BMW", -2144, 12454);

            Assert.Equal(mainCars.addList(car), false);
        }
        [Fact]
        public void CreateInvalidCapacityPer100KMCarBase()
        {
            Car mainCars = new Car();
            Car car = new Car("BMW", 2145, -124);

            Assert.Equal(mainCars.addList(car), false);
        }

        [Fact]
        public void QCarBase()
        {
            Car car = new Car("BMW", 2145, 124);

            Assert.Equal(car.Q(), 17);
        }

        [Fact]
        public void QCarWithYear()
        {
            CarsWithYears car = new CarsWithYears("BMW", 2145, 124, 4);

            Assert.Equal(car.Q(), 78.19);
        }
        [Fact]
        public void CreateValidCarWithYear()
        {
            CarsWithYears mainCars = new CarsWithYears();
            CarsWithYears car = new CarsWithYears("BMW", 2145, 12454, 4);

            Assert.Equal(mainCars.addList(car), true);
        }

        [Fact]
        public void CreateInvalidNameWithYear()
        {
            CarsWithYears mainCars = new CarsWithYears();
            CarsWithYears car = new CarsWithYears("", 2145, 12454, 4);

            Assert.Equal(mainCars.addList(car), false);
        }
        [Fact]
        public void CreateInvalidCapacityWithYear()
        {
            CarsWithYears mainCars = new CarsWithYears();
            CarsWithYears car = new CarsWithYears("BMW", -2144, 12454, 4);

            Assert.Equal(mainCars.addList(car), false);
        }
        [Fact]
        public void CreateInvalidCapacityPer100KMCarWithYear()
        {
            CarsWithYears mainCars = new CarsWithYears();
            CarsWithYears car = new CarsWithYears("BMW", 2145, -124, 4);

            Assert.Equal(mainCars.addList(car), false);
        }
        public void CreateInvalidYearCarWithYear()
        {
            CarsWithYears mainCars = new CarsWithYears();
            CarsWithYears car = new CarsWithYears("BMW", 2145, 124, -4);

            Assert.Equal(mainCars.addList(car), false);
        }
        [Fact]
        public void CreateInvalidMiddleQBase()
        {
            Car mainCars = new Car();
            Car car = new Car("BMW", 21504, 124);
            Car car1 = new Car("BMgdfW", 21005, 124);
            Car car2 = new Car("BdgMW", 19405, 124);

            mainCars.addList(car);
            mainCars.addList(car1);
            mainCars.addList(car2);

            Assert.Equal(mainCars.middleValuesQInList(), 166);
        }


    }
}