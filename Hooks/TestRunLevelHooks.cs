using ContactListAutomationTests.Infrastructure;
using Microsoft.Extensions.Configuration;
using Reqnroll.BoDi;

namespace ContactListAutomationTests.Hooks;

[Binding]
public sealed class TestRunLevelHooks
{
    private static IPlaywright _playwright;
    private static IBrowser _browser;
    private static IObjectContainer _objectContainer;

    [BeforeTestRun]
    public static async Task BeforeTestRun(IObjectContainer objectContainer)
    {
        _objectContainer = objectContainer;

        var testConfig = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("test_settings.json", false, true)
            .AddEnvironmentVariables()
            .Build();

        TestConfiguration.BrowserOptions = testConfig.GetSection("BrowserOptions")
            .Get<BrowserTypeLaunchOptions>();
        TestConfiguration.BrowserContextOptions = testConfig.GetSection("BrowserContextOptions")
            .Get<BrowserNewContextOptions>();
        TestConfiguration.AppConfig = testConfig.GetSection("AppConfig").Get<AppConfig>()!;

        await StartBrowserAsync();
    }

    [AfterTestRun]
    public static async Task AfterTestRun()
    {
        await _browser.CloseAsync();
        _playwright.Dispose();

        // Clean up the state.json file
        var path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "state.json"));
        if (File.Exists(path)) File.Delete(path);
    }

    private static async Task StartBrowserAsync()
    {
        _playwright = await Playwright.CreateAsync();

        _browser = await _playwright.Chromium.LaunchAsync(TestConfiguration.BrowserOptions);

        _objectContainer.RegisterInstanceAs(_playwright);
        _objectContainer.RegisterInstanceAs(_browser);
    }
}