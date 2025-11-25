using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace PlaywrightTests;
using PlaywrightTests.Pages;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class InventoryTest : PageTest
{
    private LoginPage _loginPage;
    private InventoryPage _inventoryPage;

    [SetUp]
    public async Task LoginSetUp()
    {
        // Prepare page for testing
        _loginPage = new LoginPage(Page);
        _inventoryPage = new InventoryPage(Page);

        await _loginPage.GotoHome();

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
}