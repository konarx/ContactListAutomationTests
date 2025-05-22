using ContactListAutomationTests.Models;
using ContactListAutomationTests.Pages;

namespace ContactListAutomationTests.Steps;

[Binding]
public class SignUp(
    IPage page,
    ScenarioContext scenarioContext,
    LoginPage loginPage,
    SignUpPage signUpPage)
{
    [Given("the user is on the Sign Up page")]
    public async Task GivenTheUserIsOnTheSignUpPage()
    {
        await loginPage.SignUpButton.ClickAsync();
        await page.WaitForLoadStateAsync();
        await Assertions.Expect(signUpPage.TitleHeading).ToHaveTextAsync("Add User");
    }

    [When("the user fills in the sign-up form with valid details")]
    public async Task WhenTheUserFillsInTheSignUpFormWithValidDetails()
    {
        var testUser = TestData.CreateUser();
        scenarioContext["testUser"] = testUser;

        await signUpPage.FirstNameInput.FillAsync(testUser.FirstName);
        await signUpPage.LastNameInput.FillAsync(testUser.LastName);
        await signUpPage.EmailInput.FillAsync(testUser.Email);
        await signUpPage.PasswordInput.FillAsync(testUser.Password);
    }

    [When("the user submits the sign-up form")]
    public async Task WhenTheUserSubmitsTheSignUpForm()
    {
        await signUpPage.SubmitButton.ClickAsync();
        await page.WaitForLoadStateAsync();
    }
}