using ContactListAutomationTests.Infrastructure;
using ContactListAutomationTests.Pages;

namespace ContactListAutomationTests.Steps;

[Binding]
public class LoginSteps(IPage page, LoginPage loginPage)
{
    [Given("the user navigates to site")]
    public async Task GivenTheUserNavigatesToSite()
    {
        await page.GotoAsync(TestConfiguration.AppConfig.BaseUrl);
        await page.WaitForLoadStateAsync();
    }

    [Given("the user is logged in")]
    public async Task GivenTheUserIsLoggedIn()
    {
        await loginPage.LoginAsync(TestConfiguration.AppConfig.Username, TestConfiguration.AppConfig.Password);
    }
}