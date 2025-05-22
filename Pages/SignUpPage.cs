namespace ContactListAutomationTests.Pages;

public class SignUpPage(IPage page)
{
    public ILocator TitleHeading => page.Locator("h1");
    public ILocator Tagline => page.Locator(".main-content p").First;

    public ILocator ErrorBanner => page.Locator("#error");

    public ILocator Form => page.Locator("#add-user");
    public ILocator FirstNameInput => page.Locator("#firstName");
    public ILocator LastNameInput => page.Locator("#lastName");
    public ILocator EmailInput => page.Locator("#email");
    public ILocator PasswordInput => page.Locator("#password");

    public ILocator SubmitButton => page.Locator("#submit");
    public ILocator CancelButton => page.Locator("#cancel");
}