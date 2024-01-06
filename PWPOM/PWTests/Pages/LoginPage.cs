using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWPOM.PWTests.Pages
{
    internal class LoginPage
    {
        private IPage _page;

        private ILocator _lnkLogin;
        private ILocator _inputUsername;
        private ILocator _inputPassword;
        private ILocator _btnLogin;
        private ILocator _welcomeMessage;
        public LoginPage(IPage page)
        {

            _page = page;
            _lnkLogin = _page.Locator(selector: "text=Login");
            _inputUsername = _page.Locator(selector: "#UserName");
            _inputPassword = _page.Locator(selector: "#Password");
            _btnLogin = _page.Locator(selector: "input", new PageLocatorOptions
            { HasTextString = "Log in" });
            _welcomeMessage = _page.Locator(selector: "text='Hello admin!'");

        }
        public async Task ClickLoginLink()
        {
            await _lnkLogin.ClickAsync();
        }
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
