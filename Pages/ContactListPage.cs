using ContactListAutomationTests.Models;

namespace ContactListAutomationTests.Pages;

public class ContactListPage(IPage page)
{
    public ILocator TitleHeading => page.Locator("header h1");
    public ILocator LogoutButton => page.Locator("#logout");

    public ILocator InstructionText => page.Locator(".main-content p").First;
    public ILocator AddContactButton => page.Locator("#add-contact");

    public ILocator ContactsTable => page.Locator(".contactTable");
    public ILocator TableHeaderRow => page.Locator(".contactTableHead tr");
    public ILocator TableBody => page.Locator(".contactTable-Body");
    public ILocator AllRows => ContactsTable.Locator("tr");

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
}