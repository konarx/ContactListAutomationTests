using ContactListAutomationTests.Infrastructure;

namespace ContactListAutomationTests.Steps;

[Binding]
public class LoginSteps(IPage page)
{
    [Given("the admin user logs in to site")]
    public async Task GivenTheAdminUserLogsInToSite()
    {
        await page.GotoAsync(TestConfiguration.AppConfig.BaseUrl);
        await page.WaitForLoadStateAsync();
    }
}