using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace FormTestUsingSelenium
{
    class Program
    {
        static void Main()
        {
            IWebDriver driver = new ChromeDriver();
            //-------------------------change-url-here------------------------------------
            string baseURL = "http://shugroupproject1.quba.co.uk/";
            //----------------------------------------------------------------------------
            //---------------------------Enter-Code-Here----------------------------------
            //---------Use-Console-to-test-for-errors-and-error-capturing-----------------
            driver.Navigate().GoToUrl(baseURL);
            Console.WriteLine("Testing TextBox1");//for test log
            try
            {
                driver.FindElement(By.Id("TextBox1")).Clear();
                driver.FindElement(By.Id("TextBox12")).SendKeys("Username@Email.com");
            } 
            catch 
            { 
                Console.Error.WriteLine("Error: Testing TextBox1");/*Error to logs*/
                Environment.Exit(1);
            }
            Console.WriteLine("Testing TextBox2");
            try
            {
                driver.FindElement(By.Id("TextBox2")).Clear();
                driver.FindElement(By.Id("TextBox2")).SendKeys("password");
            }
            catch 
            { 
                Console.Error.WriteLine("Error: Testing TextBox1");
                Environment.Exit(1);
            }
            Console.WriteLine("Testing CheckBox1");
            try 
            { 
                driver.FindElement(By.Id("CheckBox1")).Click();
            } 
            catch
            { 
                Console.Error.WriteLine("Error: Testing CheckBox1");
                Environment.Exit(1);
            }
            Console.WriteLine("Testing TextBox3");
            try 
            {
                driver.FindElement(By.Id("TextBox3")).Clear();
                driver.FindElement(By.Id("TextBox3")).SendKeys("homenumber\naddress\npostcode");
            } 
            catch 
            { 
                Console.Error.WriteLine("Error: Testing TextBox3");
                Environment.Exit(1);
            }
            Console.WriteLine("Testing RadioButtonList1_1");
            try 
            {
                driver.FindElement(By.Id("RadioButtonList1_1")).Click();
            } 
            catch 
            { 
                Console.Error.WriteLine("Error: Testing RadioButtonList1_1");
                Environment.Exit(1);
            }
            Console.WriteLine("Testing DropDownList1");
            try
            {
                new SelectElement(driver.FindElement(By.Id("DropDownList1"))).SelectByText("GERMANY");
            } 
            catch 
            { 
                Console.Error.WriteLine("Error: Testing DropDownList1");
                Environment.Exit(1);
            }
            Console.WriteLine("Testing btnSubmit");
            try
            {
                driver.FindElement(By.Id("btnSubmit")).Click();
            }
            catch
            {
                Console.Error.WriteLine("Error: Testing btnSubmit");
                Environment.Exit(1);
            }
            //---------------------------------------------------------------------------
            Console.ReadLine();
            driver.Quit();
        }
    }
}
