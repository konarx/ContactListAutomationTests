using ContactListAutomationTests.Models;
using ContactListAutomationTests.Steps;
using FluentAssertions;
using FluentAssertions.Execution;

namespace ContactListAutomationTests.Pages;

public class ContactListPage(IPage page, AddContactSteps addContactSteps)
{
    public ILocator TitleHeading => page.Locator("header h1");
    public ILocator LogoutButton => page.Locator("#logout");

    public ILocator InstructionText => page.Locator(".main-content p").First;
    public ILocator AddContactButton => page.Locator("#add-contact");

    public ILocator ContactsTable => page.Locator(".contactTable");
    public ILocator TableHeaderRow => page.Locator(".contactTableHead tr");
    public ILocator TableBody => page.Locator(".contactTable-Body");
    public ILocator AllRows => ContactsTable.Locator("tr.contactTableBodyRow");

    public ILocator RowByName(string fullName) => page.Locator($".contactTable-Body tr:has(td >> text='{fullName}')");

    public ILocator NameCellInRow(ILocator row) => row.Locator("td:not([hidden])").Nth(0);
    public ILocator BirthdateCellInRow(ILocator row) => row.Locator("td:not([hidden])").Nth(1);
    public ILocator EmailCellInRow(ILocator row) => row.Locator("td:not([hidden])").Nth(2);
    public ILocator PhoneCellInRow(ILocator row) => row.Locator("td:not([hidden])").Nth(3);
    public ILocator AddressCellInRow(ILocator row) => row.Locator("td:not([hidden])").Nth(4);
    public ILocator CityStatePostalCellInRow(ILocator row) => row.Locator("td:not([hidden])").Nth(5);
    public ILocator CountryCellInRow(ILocator row) => row.Locator("td:not([hidden])").Nth(6);

    public async Task<ILocator> GetCellByRowAndHeaderAsync(int rowIndex, string headerText)
    {
        var col = await ContactsTable.GetColumnIndexAsync(headerText);
        var row = ContactsTable.Locator("tr.contactTableBodyRow").Nth(rowIndex);
        return row.Locator("td:not([hidden])").Nth(col);
    }

    public async Task<string> GetFirstCellTextByHeaderAsync(string headerText)
    {
        var cell = await GetCellByRowAndHeaderAsync(0, headerText);
        return await cell.TextContentAsync();
    }

    public async Task<IReadOnlyList<string>> GetColumnValuesAsync(string headerText)
    {
        var col = await ContactsTable.GetColumnIndexAsync(headerText);
        var cells = ContactsTable.Locator($"tr.contactTableBodyRow td:not([hidden]):nth-child({col + 1})");
        return await cells.AllInnerTextsAsync();
    }

    public async Task<bool> IsContactPresentAsync(string fullName)
    {
        return await RowByName(fullName).IsVisibleAsync();
    }

    public async Task EnsureContactExistsAsync()
    {
        if (await AllRows.CountAsync() is 0)
        {
            await AddContactButton.ClickAsync();
            await page.WaitForLoadStateAsync();
            await addContactSteps.ThenTheUserShouldBeOnThePage();
            await addContactSteps.WhenTheUserFillsInTheContactFormWithValidDetails();
            await addContactSteps.WhenTheUserSubmitsTheContactForm();
            await Task.WhenAll(
                Assertions.Expect(ContactsTable).ToBeVisibleAsync(),
                Assertions.Expect(LogoutButton).ToBeVisibleAsync(),
                Assertions.Expect(TitleHeading).ToHaveTextAsync("Contact List"),
                Assertions.Expect(AllRows).ToHaveCountAsync(1)
            );
        }
    }

    public async Task ValidateContactsTableFirstRowAsync(DTOs.Contact testContact)
    {
        var name = await GetFirstCellTextByHeaderAsync("Name");
        var birthdate = await GetFirstCellTextByHeaderAsync("Birthdate");
        var email = await GetFirstCellTextByHeaderAsync("Email");
        var phone = await GetFirstCellTextByHeaderAsync("Phone");
        var address = await GetFirstCellTextByHeaderAsync("Address");
        var cityStateProvincePostalCode = await GetFirstCellTextByHeaderAsync("City, State/Province, Postal Code");
        var country = await GetFirstCellTextByHeaderAsync("Country");

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