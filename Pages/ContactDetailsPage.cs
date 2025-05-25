namespace ContactListAutomationTests.Pages;

public class ContactDetailsPage(IPage page)
{
    public ILocator TitleHeading => page.Locator("header h1");
    public ILocator LogoutButton => page.Locator("#logout");

    public ILocator EditContactButton => page.Locator("#edit-contact");
    public ILocator DeleteContactButton => page.Locator("#delete");
    public ILocator ReturnButton => page.Locator("#return");

    public ILocator DetailsForm => page.Locator("#contactDetails");

    public ILocator FirstNameInput => page.Locator("#firstName");
    public ILocator LastNameInput => page.Locator("#lastName");
    public ILocator BirthdateInput => page.Locator("#birthdate");
    public ILocator EmailInput => page.Locator("#email");
    public ILocator PhoneInput => page.Locator("#phone");
    public ILocator Street1Input => page.Locator("#street1");
    public ILocator Street2Input => page.Locator("#street2");
    public ILocator CityInput => page.Locator("#city");
    public ILocator StateProvinceInput => page.Locator("#stateProvince");
    public ILocator PostalCodeInput => page.Locator("#postalCode");
    public ILocator CountryInput => page.Locator("#country");

    public async Task DeleteContactAsync()
    {
        page.Dialog += async (_, dialog) => await dialog.AcceptAsync();
        await DeleteContactButton.ClickAsync();
        await page.WaitForURLAsync("**/contactList");
        await page.WaitForLoadStateAsync();
    }
}