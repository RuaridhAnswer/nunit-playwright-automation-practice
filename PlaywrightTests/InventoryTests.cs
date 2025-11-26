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
        // Open item page
        await Page.Locator("[data-test=\"item-4-title-link\"]").ClickAsync();
        // Add to cart
        await _inventoryPage.AddToCart();
        // Remove from cart
        await _inventoryPage.RemoveFromCart();
        // Add to cart
        await _inventoryPage.AddToCart();
        // View items in cart
        await _inventoryPage.ViewCart();

    }
}