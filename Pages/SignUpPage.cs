using ContactListAutomationTests.Models;

namespace ContactListAutomationTests.Pages;

public class SignUpPage(IPage page, LoginPage loginPage)
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

    public async Task EnsureUserIsIsOnSignUpPageAsync()
    {
        const string expectedTitle = "Add User";
        var pageTitle = await TitleHeading.TextContentAsync();
        if (pageTitle is not expectedTitle)
        {
            await loginPage.SignUpButton.ClickAsync();
            await page.WaitForLoadStateAsync();
        }

        await Assertions.Expect(TitleHeading).ToHaveTextAsync(expectedTitle);
    }

    public async Task FillSignUpDetailsAsync(DTOs.User user)
    {
        await FirstNameInput.FillAsync(user.FirstName);
        await LastNameInput.FillAsync(user.LastName);
        await EmailInput.FillAsync(user.Email);
        await PasswordInput.FillAsync(user.Password);
    }
}