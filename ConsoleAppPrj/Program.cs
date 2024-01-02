using Microsoft.Playwright;

//playwright startup
using var playwright = await Playwright.CreateAsync();

//Launch browser
await using var browser = await playwright.Chromium.LaunchAsync();

//page instance
var context = await browser.NewContextAsync();
var page = await context.NewPageAsync();

Console.WriteLine("Opened Browser");
await page.GotoAsync("https://www.google.com");
Console.WriteLine("Page Loaded");

await page.GetByTitle("Search").FillAsync("hp laptop");
//await page.Locator("#APjFgb").FillAsync("hp laptop");//another method,# for id,. for class
Console.WriteLine("Typed");
await page.Locator("(//input[@value='Google Search'])[2]").ClickAsync();
Console.WriteLine("Clicked");