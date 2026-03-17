using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace prakt5
{
    public class SeleniumUITests : IDisposable
    {
        IWebDriver driver = new ChromeDriver();

        public void Dispose()
        {
            driver.Quit();
        }

        [Fact]
        public void RegisterTest()
        {
            string url = "https://test.webmx.ru/";
            string login = "fffgffdghgffd";
            string pass = "qwertyui";
            string expected = "Регистрация успешна. Теперь выполните вход.";
            string xpath = "//*[@id=\"message\"]/span";

            driver.Url = url;

            driver.FindElement(By.Id("registerTab")).Click();
            IWebElement loginInput = driver.FindElement(By.Id("authUsername"));
            IWebElement passInput = driver.FindElement(By.Id("authPassword"));

            loginInput.SendKeys(login);
            passInput.SendKeys(pass);

            driver.FindElement(By.Id("authSubmit")).Click();
            Thread.Sleep(1000);

            IWebElement result = driver.FindElement(By.XPath(xpath));
            Thread.Sleep(1000);

            Assert.Equal(expected, result.Text);

            driver.Close();
        }

        [Fact]
        public void RegisterWithAnExistingLogin()
        {
            string url = "https://test.webmx.ru/";
            string login = "fffgfd";
            string pass = "qwertyui";
            string expected = "Пользователь с таким логином уже существует.";
            string xpath = "//*[@id=\"message\"]/span";

            driver.Url = url;

            driver.FindElement(By.Id("registerTab")).Click();
            IWebElement loginInput = driver.FindElement(By.Id("authUsername"));
            IWebElement passInput = driver.FindElement(By.Id("authPassword"));

            loginInput.SendKeys(login);
            passInput.SendKeys(pass);

            driver.FindElement(By.Id("authSubmit")).Click();
            Thread.Sleep(1000);

            IWebElement result = driver.FindElement(By.XPath(xpath));
            Thread.Sleep(1000);

            Assert.Equal(expected, result.Text);

            driver.Close();
        }

        [Fact]
        public void AuthTest()
        {
            string url = "https://test.webmx.ru/";
            string login = "fffgfdgfd";
            string pass = "qwertyui";
            string expected = $"Здравствуйте, {login}!";

            driver.Url = url;

            IWebElement loginInput = driver.FindElement(By.Id("authUsername"));
            IWebElement passInput = driver.FindElement(By.Id("authPassword"));

            loginInput.SendKeys(login);
            passInput.SendKeys(pass);

            driver.FindElement(By.Id("authSubmit")).Click();
            Thread.Sleep(1000);

            IWebElement result = driver.FindElement(By.Id("welcomeText"));
            Thread.Sleep(1000);

            Assert.Equal(expected, result.Text);

            driver.Close();
        }

        [Fact]
        public void AuthEmptyLoginTest()
        {
            string url = "https://test.webmx.ru/";
            string pass = "qwertyui";
            string expected = "Заполните это поле.";

            driver.Url = url;

            IWebElement passInput = driver.FindElement(By.Id("authPassword"));
            passInput.SendKeys(pass);

            driver.FindElement(By.Id("authSubmit")).Click();
            Thread.Sleep(1000);

            IWebElement loginInput = driver.FindElement(By.Id("authUsername"));
            string message = loginInput.GetAttribute("validationMessage");

            Thread.Sleep(1000);

            Assert.Equal(expected, message);

            driver.Close();
        }

        [Fact]
        public void AuthEmptyPassTest()
        {
            string url = "https://test.webmx.ru/";
            string login = "qwertyui";
            string expected = "Заполните это поле.";

            driver.Url = url;

            IWebElement loginInput = driver.FindElement(By.Id("authUsername"));
            loginInput.SendKeys(login);

            driver.FindElement(By.Id("authSubmit")).Click();
            Thread.Sleep(1000);

            IWebElement passInput = driver.FindElement(By.Id("authPassword"));
            string message = passInput.GetAttribute("validationMessage");

            Thread.Sleep(1000);

            Assert.Equal(expected, message);

            driver.Close();
        }

        [Fact]
        public void AuthLoginMin3Test()
        {
            string url = "https://test.webmx.ru/";
            string login = "qw";
            string expected = $"Минимально допустимое количество символов: 3. Длина текста сейчас: {login.Length}.";

            driver.Url = url;

            IWebElement loginInput = driver.FindElement(By.Id("authUsername"));
            loginInput.SendKeys(login);

            driver.FindElement(By.Id("authSubmit")).Click();
            Thread.Sleep(1000);

            string message = loginInput.GetAttribute("validationMessage");

            Thread.Sleep(1000);

            Assert.Equal(expected, message);

            driver.Close();
        }

        [Fact]
        public void AuthPassMin6Test()
        {
            string url = "https://test.webmx.ru/";
            string login = "qwertyui";
            string pass = "qwe";
            string expected = $"Минимально допустимое количество символов: 6. Длина текста сейчас: {pass.Length}.";

            driver.Url = url;

            IWebElement loginInput = driver.FindElement(By.Id("authUsername"));
            IWebElement passInput = driver.FindElement(By.Id("authPassword"));

            loginInput.SendKeys(login);
            passInput.SendKeys(pass);

            driver.FindElement(By.Id("authSubmit")).Click();
            Thread.Sleep(1000);

            string message = passInput.GetAttribute("validationMessage");

            Thread.Sleep(1000);

            Assert.Equal(expected, message);

            driver.Close();
        }

        [Fact]
        public void AuthWithIncorrectLoginTest()
        {
            string url = "https://test.webmx.ru/";
            string login = "fffgfdgfddd";
            string pass = "qwertyui";
            string expected = "Неверный логин или пароль.";
            string xpath = "//*[@id=\"message\"]/span";

            driver.Url = url;

            IWebElement loginInput = driver.FindElement(By.Id("authUsername"));
            IWebElement passInput = driver.FindElement(By.Id("authPassword"));

            loginInput.SendKeys(login);
            passInput.SendKeys(pass);

            driver.FindElement(By.Id("authSubmit")).Click();
            Thread.Sleep(1000);

            IWebElement result = driver.FindElement(By.XPath(xpath));
            Thread.Sleep(1000);

            Assert.Equal(expected, result.Text);

            driver.Close();
        }

        [Fact]
        public void AuthWithIncorrectPasswordTest()
        {
            string url = "https://test.webmx.ru/";
            string login = "fffgfdgfd";
            string pass = "qwertyui1";
            string expected = "Неверный логин или пароль.";
            string xpath = "//*[@id=\"message\"]/span";

            driver.Url = url;

            IWebElement loginInput = driver.FindElement(By.Id("authUsername"));
            IWebElement passInput = driver.FindElement(By.Id("authPassword"));

            loginInput.SendKeys(login);
            passInput.SendKeys(pass);

            driver.FindElement(By.Id("authSubmit")).Click();
            Thread.Sleep(1000);

            IWebElement result = driver.FindElement(By.XPath(xpath));
            Thread.Sleep(1000);

            Assert.Equal(expected, result.Text);

            driver.Close();
        }

        [Fact]
        public void ExitFromProfile()
        {
            string url = "https://test.webmx.ru/";
            string login = "fffgffdgfd";
            string pass = "qwertyui";
            string expected = "Вы вышли из системы.";
            string xpath = "//*[@id=\"message\"]/span";

            driver.Url = url;

            IWebElement loginInput = driver.FindElement(By.Id("authUsername"));
            IWebElement passInput = driver.FindElement(By.Id("authPassword"));

            loginInput.SendKeys(login);
            passInput.SendKeys(pass);

            driver.FindElement(By.Id("authSubmit")).Click();
            Thread.Sleep(1000);

            driver.FindElement(By.Id("logoutBtn")).Click();
            Thread.Sleep(1000);

            IWebElement result = driver.FindElement(By.XPath(xpath));
            Thread.Sleep(1000);

            Assert.Equal(expected, result.Text);

            driver.Close();
        }

        [Fact]
        public void CreateNote()
        {
            string url = "https://test.webmx.ru/";
            string login = "fffgffdgfd";
            string pass = "qwertyui";
            string title = "login";
            string content = "pass";
            string expected = "Заметка создана.";
            string xpath = "//*[@id=\"message\"]/span";

            driver.Url = url;

            IWebElement loginInput = driver.FindElement(By.Id("authUsername"));
            IWebElement passInput = driver.FindElement(By.Id("authPassword"));

            loginInput.SendKeys(login);
            passInput.SendKeys(pass);

            driver.FindElement(By.Id("authSubmit")).Click();
            Thread.Sleep(1000);

            IWebElement titleInput = driver.FindElement(By.Id("noteTitle"));
            IWebElement descInput = driver.FindElement(By.Id("noteContent"));

            titleInput.SendKeys(title);
            descInput.SendKeys(content);

            driver.FindElement(By.Id("saveBtn")).Click();
            Thread.Sleep(1000);

            IWebElement result = driver.FindElement(By.XPath(xpath));
            Thread.Sleep(1000);

            Assert.Equal(expected, result.Text);

            driver.Close();
        }

        [Fact]
        public void CreateNoteNotitle()
        {
            string url = "https://test.webmx.ru/";
            string login = "fffgffdgfd";
            string pass = "qwertyui";
            string content = "pass";
            string expected = "Заполните это поле.";
            string xpath = "//*[@id=\"message\"]/span";

            driver.Url = url;

            IWebElement loginInput = driver.FindElement(By.Id("authUsername"));
            IWebElement passInput = driver.FindElement(By.Id("authPassword"));

            loginInput.SendKeys(login);
            passInput.SendKeys(pass);

            driver.FindElement(By.Id("authSubmit")).Click();
            Thread.Sleep(1000);

            IWebElement titleInput = driver.FindElement(By.Id("noteTitle"));
            IWebElement descInput = driver.FindElement(By.Id("noteContent"));

            descInput.SendKeys(content);

            driver.FindElement(By.Id("saveBtn")).Click();
            Thread.Sleep(1000);

            string message = titleInput.GetAttribute("validationMessage");

            Thread.Sleep(1000);

            Assert.Equal(expected, message);

            driver.Close();
        }


        [Fact]
        public void DeleteNoteTest()
        {
            string url = "https://test.webmx.ru/";
            string login = "fffgffdgfd";
            string pass = "qwertyui";
            string title = "login";
            string content = "pass";
            string expected = "Заметка удалена.";
            string xpath = "//*[@id=\"message\"]/span";

            driver.Url = url;

            IWebElement loginInput = driver.FindElement(By.Id("authUsername"));
            IWebElement passInput = driver.FindElement(By.Id("authPassword"));

            loginInput.SendKeys(login);
            passInput.SendKeys(pass);

            driver.FindElement(By.Id("authSubmit")).Click();
            Thread.Sleep(1000);

            IWebElement titleInput = driver.FindElement(By.Id("noteTitle"));
            IWebElement descInput = driver.FindElement(By.Id("noteContent"));

            titleInput.SendKeys(title);
            descInput.SendKeys(content);

            driver.FindElement(By.Id("saveBtn")).Click();
            Thread.Sleep(1000);

            driver.FindElement(By.Id("deleteBtn")).Click();
            Thread.Sleep(1000);

            driver.SwitchTo().Alert().Accept();
            Thread.Sleep(1000);

            IWebElement result = driver.FindElement(By.XPath(xpath));
            Thread.Sleep(1000);

            Assert.Equal(expected, result.Text);

            driver.Close();
        }

        [Fact]
        public void EditNoteTest()
        {
            string url = "https://test.webmx.ru/";
            string login = "fffgffdgfd";
            string pass = "qwertyui";
            string title = "login";
            string content = "pass";
            string updatedTitle = "loginf";
            string expected = "Заметка обновлена.";
            string xpath = "//*[@id=\"message\"]/span";

            driver.Url = url;

            IWebElement loginInput = driver.FindElement(By.Id("authUsername"));
            IWebElement passInput = driver.FindElement(By.Id("authPassword"));

            loginInput.SendKeys(login);
            passInput.SendKeys(pass);

            driver.FindElement(By.Id("authSubmit")).Click();
            Thread.Sleep(1000);

            IWebElement titleInput = driver.FindElement(By.Id("noteTitle"));
            IWebElement descInput = driver.FindElement(By.Id("noteContent"));

            titleInput.SendKeys(title);
            descInput.SendKeys(content);

            driver.FindElement(By.Id("saveBtn")).Click();
            Thread.Sleep(1000);

            titleInput.SendKeys(updatedTitle);
            Thread.Sleep(1000);

            driver.FindElement(By.Id("saveBtn")).Click();
            Thread.Sleep(1000);

            IWebElement result = driver.FindElement(By.XPath(xpath));
            Thread.Sleep(1000);

            Assert.Equal(expected, result.Text);

            driver.Close();
        }

        [Fact]
        public void ShareNoteTest()
        {
            string url = "https://test.webmx.ru/";
            string login = "fffgffdgfd";
            string pass = "qwertyui";
            string title = "login";
            string content = "pass";
            string shareUser = "@neven49";
            string expected = "Доступ успешно выдан.";
            string xpath = "//*[@id=\"message\"]/span";

            driver.Url = url;

            IWebElement loginInput = driver.FindElement(By.Id("authUsername"));
            IWebElement passInput = driver.FindElement(By.Id("authPassword"));

            loginInput.SendKeys(login);
            passInput.SendKeys(pass);

            driver.FindElement(By.Id("authSubmit")).Click();
            Thread.Sleep(1000);

            IWebElement titleInput = driver.FindElement(By.Id("noteTitle"));
            IWebElement descInput = driver.FindElement(By.Id("noteContent"));

            titleInput.SendKeys(title);
            descInput.SendKeys(content);

            driver.FindElement(By.Id("saveBtn")).Click();
            Thread.Sleep(1000);

            IWebElement shareInput = driver.FindElement(By.Id("shareUsername"));
            shareInput.SendKeys(shareUser);

            Thread.Sleep(1000);

            driver.FindElement(By.Id("shareBtn")).Click();
            Thread.Sleep(1000);

            IWebElement result = driver.FindElement(By.XPath(xpath));
            Thread.Sleep(1000);

            Assert.Equal(expected, result.Text);

            driver.Close();
        }

        [Fact]
        public void DoubleShareNoteTest()
        {
            string url = "https://test.webmx.ru/";
            string login = "fffgffdgfd";
            string pass = "qwertyui";
            string title = "login";
            string content = "pass";
            string shareUser = "@neven49";
            string expected = "Доступ этому пользователю уже выдан.";
            string xpath = "//*[@id=\"message\"]/span";

            driver.Url = url;

            IWebElement loginInput = driver.FindElement(By.Id("authUsername"));
            IWebElement passInput = driver.FindElement(By.Id("authPassword"));

            loginInput.SendKeys(login);
            passInput.SendKeys(pass);

            driver.FindElement(By.Id("authSubmit")).Click();
            Thread.Sleep(1000);

            IWebElement titleInput = driver.FindElement(By.Id("noteTitle"));
            IWebElement descInput = driver.FindElement(By.Id("noteContent"));

            titleInput.SendKeys(title);
            descInput.SendKeys(content);

            driver.FindElement(By.Id("saveBtn")).Click();
            Thread.Sleep(1000);

            IWebElement shareInput = driver.FindElement(By.Id("shareUsername"));
            shareInput.SendKeys(shareUser);
            Thread.Sleep(1000);

            driver.FindElement(By.Id("shareBtn")).Click();
            Thread.Sleep(1000);

            shareInput.SendKeys(shareUser);
            Thread.Sleep(1000);

            driver.FindElement(By.Id("shareBtn")).Click();
            Thread.Sleep(1000);

            IWebElement result = driver.FindElement(By.XPath(xpath));
            Thread.Sleep(1000);

            Assert.Equal(expected, result.Text);

            driver.Close();
        }

        [Fact]
        public void ShareNoUsernameNoteTest()
        {
            string url = "https://test.webmx.ru/";
            string login = "fffgffdgfd";
            string pass = "qwertyui";
            string title = "login";
            string content = "pass";
            string expected = "Укажите логин пользователя для совместного доступа.";
            string xpath = "//*[@id=\"message\"]/span";

            driver.Url = url;

            IWebElement loginInput = driver.FindElement(By.Id("authUsername"));
            IWebElement passInput = driver.FindElement(By.Id("authPassword"));

            loginInput.SendKeys(login);
            passInput.SendKeys(pass);

            driver.FindElement(By.Id("authSubmit")).Click();
            Thread.Sleep(1000);

            IWebElement titleInput = driver.FindElement(By.Id("noteTitle"));
            IWebElement descInput = driver.FindElement(By.Id("noteContent"));

            titleInput.SendKeys(title);
            descInput.SendKeys(content);

            driver.FindElement(By.Id("saveBtn")).Click();
            Thread.Sleep(1000);

            driver.FindElement(By.Id("shareBtn")).Click();
            Thread.Sleep(1000);

            IWebElement result = driver.FindElement(By.XPath(xpath));
            Thread.Sleep(1000);

            Assert.Equal(expected, result.Text);

            driver.Close();
        }

        [Fact]
        public void ShareIncorrectUsernameNoteTest()
        {
            string url = "https://test.webmx.ru/";
            string login = "fffgffdgfd";
            string pass = "qwertyui";
            string title = "login";
            string content = "pass";
            string shareUser = "@neveynhtbgrvfdn49";
            string expected = "Пользователь не найден.";
            string xpath = "//*[@id=\"message\"]/span";

            driver.Url = url;

            IWebElement loginInput = driver.FindElement(By.Id("authUsername"));
            IWebElement passInput = driver.FindElement(By.Id("authPassword"));

            loginInput.SendKeys(login);
            passInput.SendKeys(pass);

            driver.FindElement(By.Id("authSubmit")).Click();
            Thread.Sleep(1000);

            IWebElement titleInput = driver.FindElement(By.Id("noteTitle"));
            IWebElement descInput = driver.FindElement(By.Id("noteContent"));

            titleInput.SendKeys(title);
            descInput.SendKeys(content);

            driver.FindElement(By.Id("saveBtn")).Click();
            Thread.Sleep(1000);

            IWebElement shareInput = driver.FindElement(By.Id("shareUsername"));
            shareInput.SendKeys(shareUser);
            Thread.Sleep(1000);

            driver.FindElement(By.Id("shareBtn")).Click();
            Thread.Sleep(1000);

            IWebElement result = driver.FindElement(By.XPath(xpath));
            Thread.Sleep(1000);

            Assert.Equal(expected, result.Text);

            driver.Close();
        }
    }
}