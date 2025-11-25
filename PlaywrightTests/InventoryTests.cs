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
    public async Task CanAddToBasket()
    {

        //Login method
        await _loginPage.LoginWithCredentials("standard_user", "secret_sauce");
        // Expect a URL to be the inventory page.
        _loginPage.UrlExactCheck("https://www.saucedemo.com/inventory.html");
        // Add to basket
        await Page.Locator("[data-test=\"item-4-title-link\"]").ClickAsync();
        await Page.Locator("[data-test=\"add-to-cart\"]").ClickAsync();
        await Page.Locator("[data-test=\"remove\"]").ClickAsync();
        await Page.Locator("[data-test=\"add-to-cart\"]").ClickAsync();
        await Page.Locator("[data-test=\"shopping-cart-link\"]").ClickAsync();

    }
}