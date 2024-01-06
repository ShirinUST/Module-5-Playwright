using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PWPOM.PWTests.Pages;
using PWPOM.Test_Helper_Classes;
using PWPOM.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWPOM.PWTests.Tests
{
    [TestFixture]
    internal class LoginPageTest:PageTest
    {
        Dictionary<string, string> Properties;
        string? currentDir;
        private void ReadConfigSettings()
        {
            
            currentDir = Directory.GetParent(@"../../../").FullName;

            string fullPath = currentDir + "/configsettings/config.properties";

            string[] lines = File.ReadAllLines(fullPath);
            Properties = new Dictionary<string, string>();
            foreach (string line in lines)
            {
                if (!string.IsNullOrEmpty(line) && line.Contains('='))
                {
                    string[] split = line.Split('=');
                    string key = split[0].Trim();
                    string value = split[1].Trim();
                    Properties[key] = value;
                }
            }

        }
        [SetUp]
        public async Task SetUp()
        {
            ReadConfigSettings();
            Console.WriteLine("Opened Browser");
            await Page.GotoAsync(Properties["baseUrl"]/*,
                new PageGotoOptions
                { Timeout = 10000, WaitUntil = WaitUntilState.DOMContentLoaded }*/
                );
            Console.WriteLine("Page Loaded");
        }
        [Test]
        //[TestCase("admin","password")]
        //[TestCase("admin","xxxx")]
        public async Task LoginTest()
        {
            NewLoginPage loginPage = new(Page);

            string? excelFilePath = currentDir + "/Test Data/EAData.xlsx";
            string? sheetName = "Sheet 1";

            //List<EAText> excelDataList = DataRead.ReadLoginData(excelFilePath, sheetName);

            //foreach (var excelData in excelDataList)
            //{
                //string? username = excelData.UserName;
                //string? password= excelData.Password;
                await loginPage.ClickLoginLink();
                await loginPage.Login("admin", "password");

                //await loginPage.Login(username, password);

                await Page.ScreenshotAsync(new()
                {
                    Path = currentDir + "/Screenshots/screenshot.png",
                    FullPage=true,
                });
                Assert.IsTrue(await loginPage.CheckWelcomeMessage());
            //}
            
        }
    }
}
