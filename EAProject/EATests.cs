using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EANUnit
{
    [TestFixture]
    internal class EATests:PageTest
    {
        [SetUp]
        public async Task SetUp()
        {
            Console.WriteLine("Opened Browser");
            await Page.GotoAsync("http://eaapp.somee.com/",
                new PageGotoOptions
                { Timeout=3000,WaitUntil=WaitUntilState.DOMContentLoaded});
            Console.WriteLine("Page Loaded");

        }
        [Test]
        public async Task LoginTest()
        {        
            //1. await Page.GetByText("Login").ClickAsync();

            //2. var lnkLogin = Page.Locator(selector: "text=Login");
            //await lnkLogin.ClickAsync();

            await Page.ClickAsync(selector: "text=Login");
            await Console.Out.WriteLineAsync("Link Clicked");

            await Expect(Page).ToHaveURLAsync("http://eaapp.somee.com/Account/Login");

            //Username and password

            //await Page.Locator("#UserName").FillAsync(value: "admin");
            //await Page.Locator("#Password").FillAsync(value: "password");
            //await Console.Out.WriteLineAsync("Typed");

            await Page.FillAsync("#UserName", "admin");
            await Page.FillAsync("#Password","password");
            await Console.Out.WriteLineAsync("Typed");

            //await Page.Locator("//input[@value='Log in']").ClickAsync();

            var btnLogin = Page.Locator(selector: "input", new PageLocatorOptions
            { HasTextString = "Log in" });
            await btnLogin.ClickAsync();

            await Console.Out.WriteLineAsync("Clicked");

            await Expect(Page).ToHaveTitleAsync("Home - Execute Automation Employee App");

            await Expect(
                Page.Locator(selector: "text='Hello admin!'") 
                //Page.Locator(selector: "text='Log off'")
                )
                .ToBeVisibleAsync();
        }

    }
}
