using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PlaywrightTests.Pages;

public class InventoryPage
{
    private readonly IPage _page;
    private readonly ILocator _addToCartButton;
    private readonly ILocator _removeFromCartButton;
    private readonly ILocator _shoppingCartButton;

    public InventoryPage(IPage page)
    {
        _page = page;
        _addToCartButton = page.Locator("[data-test=\"add-to-cart\"]");
        _removeFromCartButton = page.Locator("[data-test=\"remove\"]");
        _shoppingCartButton = page.Locator("[data-test=\"shopping-cart-link\"]");
    }

    public async Task AddToCart()
    {
        //Add to cart button while viewing product details
        await _addToCartButton.ClickAsync();
    }

    public async Task RemoveFromCart()
    {
        //Remove from cart while viewing product details
        await _removeFromCartButton.ClickAsync();
    }

    public async Task ViewCart()
    {
        //Select shopping cart symbol to view cart and items
        await _shoppingCartButton.ClickAsync();
    }
}