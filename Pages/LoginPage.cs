namespace ContactListAutomationTests.Pages;

public class LoginPage(IPage page)
{
    public ILocator Header => page.Locator("h1");
    public ILocator FirstWelcomeMessage => page.Locator(".welcome-message").First;
    public ILocator SecondWelcomeMessage => page.Locator(".welcome-message").Nth(1);
    public ILocator ApiDocsLink => page.Locator(".welcome-message a");

    public ILocator ErrorBanner => page.Locator("#error");
    public ILocator EmailInput => page.Locator("#email");
    public ILocator PasswordInput => page.Locator("#password");
    public ILocator SubmitButton => page.Locator("#submit");
    public ILocator Form => page.Locator("form");

    public ILocator SignUpButton => page.Locator("#signup");
}