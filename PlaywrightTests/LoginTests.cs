using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace PlaywrightTests;
using PlaywrightTests.Pages;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class LoginTest : PageTest
{
    private LoginPage _loginPage;
    

    [SetUp]
    public async Task LoginSetUp() {
        // Prepare page for testing
        _loginPage = new LoginPage(Page);

        await _loginPage.GotoHome();

    }

    [TestCase("standard_user", "secret_sauce")]
    [TestCase("problem_user", "secret_sauce")]
    [TestCase("performance_glitch_user", "secret_sauce")]
    [TestCase("visual_user", "secret_sauce")]
    [TestCase("error_user", "secret_sauce")]
    public async Task CanLogin(string username, string password)
    {
        //Login method
        await _loginPage.LoginWithCredentials(username, password);
        // Expect a URL to be the inventory page.
        await _loginPage.URLCheck("https://www.saucedemo.com/inventory.html");
    }

    [Test]
    public async Task CantLoginWhenLockedOut()
    {

        //Login method
        await _loginPage.LoginWithCredentials("locked_out_user", "secret_sauce");
        // Expect error container to be visible
        await _loginPage.ErrorContainerCheck();
        // Expect error text to be correct
        await _loginPage.ErrorTextCheck("Epic sadface: Sorry, this user has been locked out.");
    }

    [Test]
    public async Task CantLoginWhenUsernameWrong()
    {
        //Login method
        await _loginPage.LoginWithCredentials("fake_user", "secret_sauce");
        // Expect error container to be visible
        await _loginPage.ErrorContainerCheck();
        // Expect error text to be correct
        await _loginPage.ErrorTextCheck("Epic sadface: Username and password do not match any user in this service");
    }

    [Test]
    public async Task CantLoginWhenPasswordWrong()
    {
        //Login method
        await _loginPage.LoginWithCredentials("standard_user", "secret_sauces");
        // Expect error container to be visible
        await _loginPage.ErrorContainerCheck();
        // Expect error text to be correct
        await _loginPage.ErrorTextCheck("Epic sadface: Username and password do not match any user in this service");
    }

    [Test]
    public async Task CantLoginWhenUsernameMissing()
    {
        //Find the password box + type in password
        await _loginPage.TypePassword("secret_sauces");
        //Find the login button + click submit
        await _loginPage.ClickLogin();
        // Expect error container to be visible
        await _loginPage.ErrorContainerCheck();
        // Expect error text to be correct
        await _loginPage.ErrorTextCheck("Epic sadface: Username is required");
    }

    [Test]
    public async Task CantLoginWhenCredentialsMissing()
    {
        //Find the login button + click submit
        await _loginPage.ClickLogin();
        // Expect error container to be visible
        await _loginPage.ErrorContainerCheck();
        // Expect error text to be correct
        await _loginPage.ErrorTextCheck("Epic sadface: Username is required");
    }

    [Test]
    public async Task CantLoginWhenPasswordMissing()
    {
        ////Find the username box + type in username
        await _loginPage.TypeUsername("standard_user");
        //Find the login button + click submit
        await _loginPage.ClickLogin();
        // Expect error container to be visible
        await _loginPage.ErrorContainerCheck();
        // Expect error text to be correct
        await _loginPage.ErrorTextCheck("Epic sadface: Password is required");
    }
}