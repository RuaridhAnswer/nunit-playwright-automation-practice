using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace PlaywrightTests;
using PlaywrightTests.Pages;
using System.Runtime.CompilerServices;

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

    [Test]
    public async Task HasAltTextInventory()
    {

        // Login method
        await _loginPage.LoginWithCredentials("standard_user", "secret_sauce");
        // Expect a URL to be the inventory page.
        _loginPage.UrlExactCheck("https://www.saucedemo.com/inventory.html");
        // Find image
        await Expect(Page.Locator("[data-test=\"inventory-item-sauce-labs-backpack-img\"]")).ToBeVisibleAsync();
        // Find image alt text
        string altText = await Page.Locator("[data-test=\"inventory-item-sauce-labs-backpack-img\"]").GetAttributeAsync("alt");
        // Checks that alt text is there and populated
        Assert.That(altText, Is.Not.Null.And.Not.Empty, "Alt text should be included");
        // Checks the alt text matches expected result
        Assert.That(altText, Is.EqualTo("Sauce Labs Backpack"));

    }

    [Test]
    public async Task HasAltTextItemDetails()
    {
        // Test assumes alt text for images matches inventory item title
        // Login method
        await _loginPage.LoginWithCredentials("visual_user", "secret_sauce");
        // Expect a URL to be the inventory page.
        _loginPage.UrlExactCheck("https://www.saucedemo.com/inventory.html");
        // Open item page
        await Page.Locator("[data-test=\"item-4-title-link\"]").ClickAsync();
        // Find inventory title and extract text
        string itemName = await Page.Locator("[data-test=\"inventory-item-name\"]").InnerTextAsync();
        // option 1 Find image and check alt text exists and matched item name
        await Page.GetByAltText(itemName).IsVisibleAsync();
        // option 2 Assert that alt text and title match
        string altText = await Page.Locator(".inventory_details_img").GetAttributeAsync("alt");
        Assert.That(altText, Is.EqualTo(itemName), "Alt text should match item title");

    }

}