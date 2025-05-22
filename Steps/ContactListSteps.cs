using ContactListAutomationTests.Models;
using ContactListAutomationTests.Pages;
using FluentAssertions;
using FluentAssertions.Execution;

namespace ContactListAutomationTests.Steps;

[Binding]
public class ContactListSteps(
    IPage page,
    ScenarioContext scenarioContext,
    ContactListPage contactListPage)
{
    [Then("the user should be logged in")]
    [Then("the user should be on the contact details page")]
    public async Task ThenTheUserShouldBeLoggedIn()
    {
        await Task.WhenAll(
            Assertions.Expect(contactListPage.ContactsTable).ToBeVisibleAsync(),
            Assertions.Expect(contactListPage.LogoutButton).ToBeVisibleAsync(),
            Assertions.Expect(contactListPage.TitleHeading).ToHaveTextAsync("Contact List")
        );
    }

    [When("the user clicks on \"Add a New Contact\" button")]
    public async Task WhenTheUserClicksOnButton()
    {
        await contactListPage.AddContactButton.ClickAsync();
        await page.WaitForLoadStateAsync();
    }

    [Then("the contact should be created successfully")]
    public async Task ThenTheContactShouldBeCreatedSuccessfully()
    {
        var testContact = scenarioContext.Get<DTOs.Contact>("testContact");

        var name = await contactListPage.GetFirstCellTextByHeaderAsync("Name");
        var birthdate = await contactListPage.GetFirstCellTextByHeaderAsync("Birthdate");
        var email = await contactListPage.GetFirstCellTextByHeaderAsync("Email");
        var phone = await contactListPage.GetFirstCellTextByHeaderAsync("Phone");
        var address = await contactListPage.GetFirstCellTextByHeaderAsync("Address");
        var cityStateProvincePostalCode =
            await contactListPage.GetFirstCellTextByHeaderAsync("City, State/Province, Postal Code");
        var country = await contactListPage.GetFirstCellTextByHeaderAsync("Country");

        using (new AssertionScope())
        {
            name.Should().Be($"{testContact.FirstName} {testContact.LastName}");
            birthdate.Should().Be(testContact.Birthdate);
            email.Should().Be(testContact.Email);
            phone.Should().Be(testContact.Phone);
            address.Should().Be($"{testContact.Street1} {testContact.Street2}");
            cityStateProvincePostalCode.Should()
                .Be($"{testContact.City} {testContact.StateProvince} {testContact.PostalCode}");
            country.Should().Be(testContact.Country);
        }
    }
}