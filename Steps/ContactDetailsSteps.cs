using ContactListAutomationTests.Pages;

namespace ContactListAutomationTests.Steps;

[Binding]
public class ContactDetailsSteps(ContactDetailsPage contactDetailsPage)
{
    [Then("the user should be on the contact details page")]
    public async Task ThenTheUserShouldBeOnTheContactDetailsPage()
    {
        await Task.WhenAll(
            Assertions.Expect(contactDetailsPage.EditContactButton).ToBeVisibleAsync(),
            Assertions.Expect(contactDetailsPage.DeleteContactButton).ToBeVisibleAsync(),
            Assertions.Expect(contactDetailsPage.ReturnButton).ToBeVisibleAsync(),
            Assertions.Expect(contactDetailsPage.TitleHeading).ToHaveTextAsync("Contact Details")
        );
    }

    [When("the user clicks on \"Delete Contact\" button")]
    public async Task WhenTheUserClicksOnButton()
    {
        await contactDetailsPage.DeleteContactAsync();
    }
}