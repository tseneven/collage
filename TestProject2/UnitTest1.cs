using Kuznetsov_Lib;

namespace TestProject2
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestCorrect1()
        {
            ComputerQ computerQ = new ComputerQ("Intel", "1000", "1000");
            string result = computerQ.Q();
            Assert.AreEqual(result, "1500");
            Assert.Pass();
        }
        [Test]
        public void TestCorrect2()
        {
            ComputerQp computerQp = new ComputerQp("Intel", "1000", "1000", "1000");
            string result = computerQp.Q();
            Assert.AreEqual(result, "2300");
            Assert.Pass();
        }
        [Test]
        public void TestInCorrect1()
        {
            ComputerQp computerQp = new ComputerQp("Intel", "1000", "1000", "-1");
            string result = computerQp.Q();
            Assert.AreEqual(result, "Число не может быть меньше 0");
            Assert.Pass();
        }
        [Test]
        public void TestInCorrect2()
        {
            ComputerQp computerQp = new ComputerQp("Intel", "1000", "1000", "ggg");
            string result = computerQp.Q();
            Assert.AreEqual(result, "Данные должны быть числовыми");
            Assert.Pass();
        }
        [Test]
        public void TestInCorrect3()
        {
            ComputerQ computerQ = new ComputerQ("Intel", "-fff", "1000");
            string result = computerQ.Q();
            Assert.AreEqual(result, "Данные должны быть числовыми в строке");
            Assert.Pass();
        }
        [Test]
        public void TestInCorrect4()
        {
            ComputerQ computerQ = new ComputerQ("", "1000", "1000");
            string result = computerQ.Q();
            Assert.AreEqual(result, "Поля пустые");
            Assert.Pass();
        }
    }
}