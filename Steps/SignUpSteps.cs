using ContactListAutomationTests.Models;
using ContactListAutomationTests.Pages;

namespace ContactListAutomationTests.Steps;

[Binding]
public class SignUpSteps(
    IPage page,
    ScenarioContext scenarioContext,
    SignUpPage signUpPage)
{
    [Given("the user is on the Sign Up page")]
    public async Task GivenTheUserIsOnTheSignUpPage()
    {
        await signUpPage.EnsureUserIsIsOnSignUpPageAsync();
    }

    [When("the user fills in the sign-up form with valid details")]
    public async Task WhenTheUserFillsInTheSignUpFormWithValidDetails()
    {
        var testUser = TestData.CreateUser();
        scenarioContext["testUser"] = testUser;

        await signUpPage.FillSignUpDetailsAsync(testUser);
    }

    [When("the user submits the sign-up form")]
    public async Task WhenTheUserSubmitsTheSignUpForm()
    {
        await signUpPage.SubmitButton.ClickAsync();
        await page.WaitForLoadStateAsync();
    }
}