
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PlaywrightTests.Pages;

public class LoginPage
{
    private readonly IPage _page;
    private readonly ILocator _usernameField;
    private readonly ILocator _passwordField;
    private readonly ILocator _loginButton;
    private readonly ILocator _errorLocator;
    private readonly ILocator _errorContainer;
    private readonly ILocator _mainMenuButton;
    private readonly ILocator _logoutOption;

    public LoginPage(IPage page)
    {
        _page = page;
        _usernameField = page.GetByRole(AriaRole.Textbox, new() { Name = "Username" });
        _passwordField = page.GetByRole(AriaRole.Textbox, new() { Name = "Password" });
        _loginButton = page.GetByRole(AriaRole.Button, new() { Name = "Login" });
        _errorLocator = page.Locator("[data-test=\"error\"]");
        _errorContainer = page.Locator(".error-message-container");
        _mainMenuButton = page.GetByRole(AriaRole.Button, new() { Name = "Open Menu" });
        _logoutOption = page.Locator("[data-test=\"logout-sidebar-link\"]");

    }

    public async Task GotoHome()
    {
        await _page.GotoAsync("https://www.saucedemo.com/");
    }

    public async Task TypeUsername(string username)
    {
        await _usernameField.FillAsync(username);
    }

    public async Task TypePassword(string password)
    {
        await _passwordField.FillAsync(password);
    }

    public async Task ClickLogin()
    {
        await _loginButton.ClickAsync();
    }

    public async Task LoginWithCredentials(string username, string password)
    {
        //Find the username box + type in username
        await TypeUsername(username);
        //Find the password box + type in password
        await TypePassword(password);
        //Find the login button + click submit
        await ClickLogin();
    }

    public async Task Logout()
    {
        //Open the menu
        await _mainMenuButton.ClickAsync();
        //Select logout
        await _logoutOption.ClickAsync();
        //Check logged out
        await UrlCheck("https://www.saucedemo.com/");
        await UrlNotCheck("https://www.saucedemo.com/inventory.html");
    }

    public async Task ErrorTextCheck(string errorMessage)
    {
        await Assertions.Expect(_errorLocator).ToHaveTextAsync(new Regex(errorMessage));
    }

    public async Task ErrorContainerCheck()
    {
        await Assertions.Expect(_errorContainer).ToBeVisibleAsync();
    }

    public async Task UrlCheck(string Url)
    {
        await Assertions.Expect(_page).ToHaveURLAsync(new Regex(Url));
    }

    public async Task UrlNotCheck(string Url)
    {
        await Assertions.Expect(_page).Not.ToHaveURLAsync(new Regex(Url));
    }

    public void UrlExactCheck(string expectedUrl)
    {
        string actualUrl = _page.Url;

        Assert.That(expectedUrl, Is.EqualTo(actualUrl));
    }
}