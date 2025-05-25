using ContactListAutomationTests.Models;
using ContactListAutomationTests.Pages;

namespace ContactListAutomationTests.Steps;

[Binding]
public class AddContactSteps(
    IPage page,
    ScenarioContext scenarioContext,
    AddContactPage addContactPage)
{
    [Then("the user should be on the \"Add Contact\" page")]
    public async Task ThenTheUserShouldBeOnThePage()
    {
        await Assertions.Expect(addContactPage.HeaderTitle).ToHaveTextAsync("Add Contact");
    }

    [When("the user fills in the contact form with valid details")]
    public async Task WhenTheUserFillsInTheContactFormWithValidDetails()
    {
        var testContact = TestData.CreateContact();
        scenarioContext["testContact"] = testContact;

        await addContactPage.FillContactDetailsAsync(testContact);
    }

    [When("the user submits the contact form")]
    public async Task WhenTheUserSubmitsTheContactForm()
    {
        await addContactPage.SubmitButton.ClickAsync();
        await page.WaitForLoadStateAsync();
    }

    [When("the user attempts to create a contact with invalid date of birth")]
    public async Task WhenTheUserAttemptsToCreateAContactWithInvalidDateOfBirth()
    {
        var testContact = TestData.CreateContact() with { Birthdate = "12/31/2021" }; // Invalid date of birth
        scenarioContext["testContact"] = testContact;

        await addContactPage.FillContactDetailsAsync(testContact);
    }

    [Then("the user should see an error message {string}")]
    public async Task ThenTheUserShouldSeeAnErrorMessage(string errorMessage)
    {
        await Assertions.Expect(addContactPage.ErrorBanner).ToBeVisibleAsync();
        await Assertions.Expect(addContactPage.ErrorBanner).ToHaveTextAsync(errorMessage);
    }
}