using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWPOM.PWTests.Pages
{
    internal class NewLoginPage
    {
        private IPage _page;

        private ILocator _lnkLogin => _page.Locator(selector: "text=Login");
        private ILocator _inputUsername => _page.Locator(selector: "#UserName");
        private ILocator _inputPassword => _page.Locator(selector: "#Password");
        private ILocator _btnLogin => _page.Locator(selector: "input", new PageLocatorOptions
        { HasTextString = "Log in" });
        private ILocator _welcomeMessage => _page.Locator(selector: "text='Hello admin!'");
        public NewLoginPage(IPage page) => _page = page;

        
        public async Task ClickLoginLink() => await _lnkLogin.ClickAsync();
        public async Task Login(string username, string password)
        {
            await _inputUsername.FillAsync(username);
            await _inputPassword.FillAsync(password);
            await _btnLogin.ClickAsync();
            await Console.Out.WriteLineAsync("login");
        }
        public async Task<bool> CheckWelcomeMessage()
        {
            return await _welcomeMessage.IsVisibleAsync();
        }
    }
}
