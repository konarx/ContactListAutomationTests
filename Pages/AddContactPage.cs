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
}