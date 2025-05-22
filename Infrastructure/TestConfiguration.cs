namespace ContactListAutomationTests.Infrastructure;

public static class TestConfiguration
{
    public static BrowserNewContextOptions? BrowserContextOptions { get; set; }
    public static BrowserTypeLaunchOptions? BrowserOptions { get; set; }
    public static AppConfig AppConfig { get; set; }
}