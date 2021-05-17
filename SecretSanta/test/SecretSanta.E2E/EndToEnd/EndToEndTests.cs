using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlaywrightSharp;
using System.Linq;

namespace SecretSanta.Web.Tests {
  [TestClass]
  public class EndToEndTests {
    private static WebHostServerFixture<Web.Startup, SecretSanta.Api.Startup> Server;

    [ClassInitialize]
    public static void InitializeClass(TestContext testContext) {
      Server = new();
    }

    [TestMethod]
    public async Task LaunchHomepage() {
      var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
      using var playwright = await Playwright.CreateAsync();
      await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions {
        Headless = true
      });

      var page = await browser.NewPageAsync();
      var response = await page.GoToAsync(localhost);

      Assert.IsTrue(response.Ok);

      var headerContent = await page.GetTextContentAsync("body > header > div > a");
      Assert.AreEqual("SecretSanta", headerContent);
    }

    [TestMethod]
    public async Task NavigateToUsers() {
      var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
      using var playwright = await Playwright.CreateAsync();
      await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions {
        Headless = true
      });

      var page = await browser.NewPageAsync();
      var response = await page.GoToAsync(localhost);

      Assert.IsTrue(response.Ok);

      await page.ClickAsync("text=Users");

      Assert.IsTrue(page.Url.EndsWith("/Users"));
    }

    [TestMethod]
    public async Task NavigateToGroups() {
      var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
      using var playwright = await Playwright.CreateAsync();
      await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions {
        Headless = true
      });

      var page = await browser.NewPageAsync();
      var response = await page.GoToAsync(localhost);

      Assert.IsTrue(response.Ok);

      await page.ClickAsync("text=Groups");

      Assert.IsTrue(page.Url.EndsWith("/Groups"));
    }

    [TestMethod]
    public async Task NavigateToGifts() {
      var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
      using var playwright = await Playwright.CreateAsync();
      await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions {
        Headless = true
      });

      var page = await browser.NewPageAsync();
      var response = await page.GoToAsync(localhost);

      Assert.IsTrue(response.Ok);

      await page.ClickAsync("text=Gifts");

      Assert.IsTrue(page.Url.EndsWith("/Gifts"));
    }

    [TestMethod]
    public async Task CreateGift() {
      var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
      using var playwright = await Playwright.CreateAsync();
      await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions {
        Headless = true
      });

      var page = await browser.NewPageAsync();
      var response = await page.GoToAsync(localhost);

      Assert.IsTrue(response.Ok);

      await page.ClickAsync("text=Gifts");

      await page.WaitForSelectorAsync("body > section > section");

      var gifts = await page.QuerySelectorAllAsync("body > section > section");
      int giftCount = gifts.Count();

      await page.ClickAsync("text=Create");

      await page.TypeAsync("input#Title", "Test Gift");
      await page.TypeAsync("input#Description", "An entirely theoretical abstract gift.");
      await page.TypeAsync("input#Priority", "1");
      await page.SelectOptionAsync("select#UserId", "1");

      await page.ClickAsync("text=Create");

      await page.WaitForSelectorAsync("body > section > section");

      gifts = await page.QuerySelectorAllAsync("body > section > section");
      Assert.AreEqual(giftCount + 1, gifts.Count());
    }

    [TestMethod]
    public async Task UpdateGift() {
      var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
      using var playwright = await Playwright.CreateAsync();
      await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions {
        Headless = true
      });

      var page = await browser.NewPageAsync();
      var response = await page.GoToAsync(localhost);

      Assert.IsTrue(response.Ok);

      await page.ClickAsync("text=Gifts");

      await page.ClickAsync("body > section > section:last-child");

      await page.FillAsync("input#Title", "");
      await page.TypeAsync("input#Title", "Updated Gift");

      await page.ClickAsync("text=Update");

      await page.WaitForSelectorAsync("body > section > section");

      var giftText = await page.GetTextContentAsync("body > section > section:last-child");
      Assert.IsTrue(giftText.Contains("Updated Gift"));
    }

    [TestMethod]
    public async Task DeleteGift() {
      var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
      using var playwright = await Playwright.CreateAsync();
      await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions {
          Headless = true
      });
    
      var page = await browser.NewPageAsync();
      var response = await page.GoToAsync(localhost);
    
      Assert.IsTrue(response.Ok);
    
      await page.ClickAsync("text=Gifts");
   
      await page.WaitForSelectorAsync("body > section > section");

      var gifts = await page.QuerySelectorAllAsync("body > section > section");
      int giftCount = gifts.Count();
    
      page.Dialog += (_, args) => args.Dialog.AcceptAsync();
    
      await page.ClickAsync("body > section > section:last-child > a > section > form > button");
    
      gifts = await page.QuerySelectorAllAsync("body > section > section");
      Assert.AreEqual(giftCount - 1, gifts.Count());
    }
  }
}
