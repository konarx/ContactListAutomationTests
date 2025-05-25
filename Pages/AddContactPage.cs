using ContactListAutomationTests.Models;

namespace ContactListAutomationTests.Pages;

public class AddContactPage(IPage page)
{
    public ILocator HeaderTitle => page.Locator("header h1");
    public ILocator LogoutButton => page.Locator("#logout");

    public ILocator ErrorBanner => page.Locator("#error");

    public ILocator Form => page.Locator("#add-contact");

    public ILocator FirstNameInput => page.Locator("#firstName");
    public ILocator LastNameInput => page.Locator("#lastName");
    public ILocator BirthdateInput => page.Locator("#birthdate");

    public ILocator EmailInput => page.Locator("#email");
    public ILocator PhoneInput => page.Locator("#phone");

    public ILocator Street1Input => page.Locator("#street1");
    public ILocator Street2Input => page.Locator("#street2");
    public ILocator CityInput => page.Locator("#city");
    public ILocator StateInput => page.Locator("#stateProvince");
    public ILocator PostalCodeInput => page.Locator("#postalCode");
    public ILocator CountryInput => page.Locator("#country");

    public ILocator SubmitButton => page.Locator("#submit");
    public ILocator CancelButton => page.Locator("#cancel");

    public async Task FillContactDetailsAsync(DTOs.Contact contact)
    {
        await FirstNameInput.FillAsync(contact.FirstName);
        await LastNameInput.FillAsync(contact.LastName);
        await BirthdateInput.FillAsync(contact.Birthdate);
        await EmailInput.FillAsync(contact.Email);
        await PhoneInput.FillAsync(contact.Phone);
        await Street1Input.FillAsync(contact.Street1);
        await Street2Input.FillAsync(contact.Street2);
        await CityInput.FillAsync(contact.City);
        await StateInput.FillAsync(contact.StateProvince);
        await PostalCodeInput.FillAsync(contact.PostalCode);
        await CountryInput.FillAsync(contact.Country);
    }
}