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

        await addContactPage.FirstNameInput.FillAsync(testContact.FirstName);
        await addContactPage.LastNameInput.FillAsync(testContact.LastName);
        await addContactPage.BirthdateInput.FillAsync(testContact.Birthdate);
        await addContactPage.EmailInput.FillAsync(testContact.Email);
        await addContactPage.PhoneInput.FillAsync(testContact.Phone);
        await addContactPage.Street1Input.FillAsync(testContact.Street1);
        await addContactPage.Street2Input.FillAsync(testContact.Street2);
        await addContactPage.CityInput.FillAsync(testContact.City);
        await addContactPage.StateInput.FillAsync(testContact.StateProvince);
        await addContactPage.PostalCodeInput.FillAsync(testContact.PostalCode);
        await addContactPage.CountryInput.FillAsync(testContact.Country);
    }

    [When("the user submits the contact form")]
    public async Task WhenTheUserSubmitsTheContactForm()
    {
        await addContactPage.SubmitButton.ClickAsync();
        await page.WaitForLoadStateAsync();
    }
}