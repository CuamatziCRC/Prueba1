// Importamos las librerías necesarias para trabajar con Selenium y Chrome
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using System;
using System.Threading;
using System.Drawing;
using OpenQA.Selenium.Support.UI;

namespace Prueba1
{
    // Esta clase contiene nuestras pruebas
    [TestFixture]
    public class TestClass
    {
        // Declaramos una variable para el navegador
        IWebDriver _driver;

        // Este método se ejecuta antes de cada prueba
        [SetUp]
        public void Setup()
        {
            // Inicializamos el navegador Chrome
            _driver = new ChromeDriver();
            // Maximizamos la ventana del navegador
            _driver.Manage().Window.Maximize();
        }

        // Este es el método de prueba para loguearse
        [Test]
        [Category("Smoke Test")]
        public void LoginToSauceDemo()
        {
            // Navegamos a la página de Google
            _driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            // Aqui colocamos el nombre de usuario
            _driver.FindElement(By.Id("user-name")).SendKeys("standard_user");
            // Aqui colocamos la contraseña del usuario
            _driver.FindElement(By.Id("password")).SendKeys("secret_sauce");
            //Se da click en el boton enviar
            _driver.FindElement(By.Id("login-button")).Submit();
            //Tiempo de espera con un hilo
            Thread.Sleep(3000);
            //Se le condiciona con un assert
            Assert.That(_driver.FindElement(By.Id("react-burger-menu-btn")).Displayed);

        }

        // Este es el método de prueba para agregar al carrito
        [Test]
        [Category("Smoke Test")]
        public void AddToCar()
        {
            // login
            LoginToSauceDemo();
            //Se da click en el boton de "Sauce Labs Backpack" con su ID 
            _driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack")).Submit();
            //Tiempo de espera con un hilo
            Thread.Sleep(3000);
            //Se da click en el boton de "Sauce Labs Bike Light" con su ID 
            _driver.FindElement(By.Id("add-to-cart-sauce-labs-bike-light")).Submit();
            //Tiempo de espera con un hilo
            Thread.Sleep(3000);
            //Se observa que hay dos productos en el carrito de compra
            //Se le condiciona con un assert para validacion de datos
            Assert.That((_driver.FindElement(By.Id("item_4_title_link")).Text), Is
                .EqualTo("Sauce Labs Backpack"));

        }

        // Este es el método de prueba para agregar al carrito y comprar
        [Test]
        [Category("Smoke Test")]
        public void ConfirmPurchase()
        {
            //VARIABLES
            string Nombre = "Juan";
            string Apellido = "Perez";
           
            // Metodo login
            LoginToSauceDemo();
            
            //Agregar productos
            //Se da click en el boton de "Sauce Labs Backpack" con su ID 
            _driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack")).Submit();
            //Se da click en el boton de "Sauce Labs Bike Light" con su ID 
            _driver.FindElement(By.Id("add-to-cart-sauce-labs-bike-light")).Submit();
            //Se da click en el boton de "Sauce Labs Fleece Jacket" con su ID 
            _driver.FindElement(By.Id("add-to-cart-sauce-labs-fleece-jacket")).Submit();
            //Aqui se confirma la compra del carrito
            _driver.FindElement(By.Id("shopping_cart_container")).Click();
            //Tiempo de espera con un hilo
            Thread.Sleep(3000);
            //Se da click en el boton del carrito => : Your Cart
            _driver.FindElement(By.Id("checkout")).Click();

            //=========================================================
            //===============Aqui se llena el formulario===============
            //=========================================================
            // Aqui colocamos el dato del nombre
            _driver.FindElement(By.Id("first-name")).SendKeys(Nombre);
            // Aqui colocamos el dato del apellido
            _driver.FindElement(By.Id("last-name")).SendKeys(Apellido);
            // Aqui colocamos el dato codigo postal
            _driver.FindElement(By.Id("postal-code")).SendKeys("0660");
            //Tiempo de espera con un hilo
            Thread.Sleep(3000);
            //=========================================================
            //===============Aqui se lleno el formulario===============
            //=========================================================
            //Se da click en el boton Continue del Checkout: Overview
            _driver.FindElement(By.Id("continue")).Click();
            //Tiempo de espera con un hilo
            Thread.Sleep(3000);
            //Se da click en el boton Finish del Checkout: Overview
            _driver.FindElement(By.Id("finish")).Click();
            //Tiempo de espera con un hilo
            Thread.Sleep(3000);
            //Termina la compra
            //Se le condiciona con un assert para validacion de datos
            Assert.That((_driver.FindElement(By.Id("back-to-products")).Text), Is
                .EqualTo("Back Home"));
        }

        // Este es el método de prueba para desplegar el menu hamburguesa y cerrar sesion
        [Test]
        [Category("Smoke Test")]
        public void MenuHamburguesa()
        {
            // login
            LoginToSauceDemo();
            //Se da click en el boton del boton hamburguesa con su ID 
            _driver.FindElement(By.Id("react-burger-menu-btn")).Click();
            //Tiempo de espera con un hilo
            Thread.Sleep(3000);
            
            //Se le condiciona con un assert para validacion de datos
            Assert.That((_driver.FindElement(By.Id("reset_sidebar_link")).Text), Is
                .EqualTo("Reset App State"));

            //Se da click en el boton X del boton hamburguesa con su ID 
            _driver.FindElement(By.Id("react-burger-cross-btn")).Click();
            //Tiempo de espera con un hilo
            Thread.Sleep(3000);

            //Se da click en el boton del boton hamburguesa con su ID 
            _driver.FindElement(By.Id("react-burger-menu-btn")).Click();
            //Tiempo de espera con un hilo
            Thread.Sleep(3000);

            //Se da click en el boton Logout del boton hamburguesa con su ID 
            _driver.FindElement(By.Id("logout_sidebar_link")).Click();
            //Tiempo de espera con un hilo
            Thread.Sleep(3000);

            //Se le condiciona con un assert para validacion de datos
            Assert.That(_driver.FindElement(By.Id("login-button")).Displayed);

        }

        // Este es el método de prueba para ordenar y usar el submenu con xPath
        [Test]
        [Category("Smoke Test")]
        public void SubmenuXpath()
        {
            // login
            LoginToSauceDemo();
            //
            // Encuentra el elemento usando XPath relativo
            IWebElement element = _driver.FindElement(By.XPath("//select[@class='product_sort_container']"));
            // Haz clic en el elemento
            element.Click();
            //Tiempo de espera con un hilo
            Thread.Sleep(3000);

            //ORDENA DEL PRECIO MAS BAJO AL MAS ALTO
            // Crea una instancia de SelectElement
            SelectElement dropdown = new SelectElement(element);
            // Selecciona el valor 'lohi'
            dropdown.SelectByValue("lohi");
            //Tiempo de espera con un hilo
            Thread.Sleep(3000);
            //Se le condiciona con un assert para validacion de datos
            Assert.That(_driver.FindElement(By.Id("react-burger-menu-btn")).Displayed);
        }

        // Este método se ejecuta después de cada prueba
        [TearDown]
        public void TearDown()
        {
            // Cerramos el navegador
            _driver.Quit();
        }
    }
}
