using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using zd3_Kuznetsovpr_23;

namespace ValidationTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethodAddCorrect()
        {
            Form1 form1 = new Form1();
            string name = "ffffff";
            int base_salary = 1000;
            double coefficient = 10;
            int numberOfEmployees = 1;
            DateTime creationDate = DateTime.Now;
            int p = 1;
            string type = "ffffff";

            string ozh = "";
            string otv = form1.add(name, base_salary, coefficient, numberOfEmployees, p, type, creationDate);

            Assert.AreEqual(ozh, otv);
        }

        [TestMethod]
        public void TestMethodAddInCorrect()
        {
            Form1 form1 = new Form1();
            string name = "1111";
            int base_salary = 1000;
            double coefficient = 10;
            int numberOfEmployees = 1;
            DateTime creationDate = DateTime.Now;
            int p = 1;
            string type = "ffffff";

            string ozh = "Ошибка валидации";
            string otv = form1.add(name, base_salary, coefficient, numberOfEmployees, p, type, creationDate);

            Assert.AreEqual(ozh, otv);
        }

        [TestMethod]
        public void TestMethodDeleteCorrect()
        {
            Form1 form1 = new Form1();
            string name = "ggggg";

            string ozh = "";
            string otv = form1.delete(name);

            Assert.AreEqual(ozh, otv);
        }

        [TestMethod]
        public void TestMethodDeleteInCorrect()
        {
            Form1 form1 = new Form1();
            string name = "ggg";

            string ozh = "Поле не может быть меньше 5";
            string otv = form1.delete(name);

            Assert.AreEqual(ozh, otv);
        }

    }
}
