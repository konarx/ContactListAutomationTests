using ContactListAutomationTests.Models;
using ContactListAutomationTests.Pages;

namespace ContactListAutomationTests.Steps;

[Binding]
public class ContactListSteps(
    IPage page,
    ScenarioContext scenarioContext,
    ContactListPage contactListPage)
{
    [Then("the user should be logged in")]
    [Then("the user should be on the Contact List page")]
    public async Task ThenTheUserShouldBeLoggedIn()
    {
        await Task.WhenAll(
            Assertions.Expect(contactListPage.ContactsTable).ToBeVisibleAsync(),
            Assertions.Expect(contactListPage.LogoutButton).ToBeVisibleAsync(),
            Assertions.Expect(contactListPage.TitleHeading).ToHaveTextAsync("Contact List")
        );
    }

    [When("the user clicks on \"Add a New Contact\" button")]
    public async Task WhenTheUserClicksOnAddNewContactButton()
    {
        await contactListPage.AddContactButton.ClickAsync();
        await page.WaitForLoadStateAsync();
    }

    [Then("the contact should be created successfully")]
    public async Task ThenTheContactShouldBeCreatedSuccessfully()
    {
        var testContact = scenarioContext.Get<DTOs.Contact>("testContact");
        await contactListPage.ValidateContactsTableFirstRowAsync(testContact);
    }

    [Given("a contact exist in table")]
    public async Task GivenAContactExistInTable()
    {
        await contactListPage.EnsureContactExistsAsync();
    }

    [When("the user navigates to the contact details page of the existing contact")]
    public async Task WhenTheUserNavigatesToTheContactDetailsPageOfTheExistingContact()
    {
        await contactListPage.AllRows.First.ClickAsync();
        await page.WaitForLoadStateAsync();
    }

    [When("the contact should be deleted successfully")]
    public async Task WhenTheContactShouldBeDeletedSuccessfully()
    {
        await Assertions.Expect(contactListPage.AllRows).ToHaveCountAsync(0);
    }
}