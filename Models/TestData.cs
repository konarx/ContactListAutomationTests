using Bogus;

namespace ContactListAutomationTests.Models;

public static class TestData
{
    public static DTOs.User CreateUser()
    {
        var faker = new Faker<DTOs.User>()
            .CustomInstantiator(f =>
            {
                var first = f.Name.FirstName();
                var last = f.Name.LastName();
                return new DTOs.User(first, last, string.Empty, string.Empty);
            })
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName).ToLowerInvariant())
            .RuleFor(u => u.Password, f => f.Internet.Password(length: 12));

        return faker.Generate();
    }

    public static DTOs.Contact CreateContact()
    {
        var faker = new Faker<DTOs.Contact>()
            .CustomInstantiator(f =>
            {
                var first = f.Name.FirstName();
                var last = f.Name.LastName();
                return new DTOs.Contact(
                    FirstName: first,
                    LastName: last,
                    Birthdate: string.Empty,
                    Email: string.Empty,
                    Phone: string.Empty,
                    Street1: string.Empty,
                    Street2: string.Empty,
                    City: string.Empty,
                    StateProvince: string.Empty,
                    PostalCode: string.Empty,
                    Country: string.Empty
                );
            })
            .RuleFor(c => c.Email, (f, c) =>
                f.Internet.Email(c.FirstName, c.LastName).ToLowerInvariant())
            .RuleFor(c => c.Birthdate, f =>
                f.Date.Past(60, DateTime.Today.AddYears(-18)).ToString("yyyy-MM-dd"))
            .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber("##########"))
            .RuleFor(c => c.Street1, f => f.Address.StreetAddress())
            .RuleFor(c => c.Street2, f => f.Address.SecondaryAddress())
            .RuleFor(c => c.City, f => f.Address.City())
            .RuleFor(c => c.StateProvince, f => f.Address.State())
            .RuleFor(c => c.PostalCode, f => f.Address.ZipCode())
            .RuleFor(c => c.Country, f => f.Address.Country());

        return faker.Generate();
    }

    public static async Task<int> GetColumnIndexAsync(this ILocator locator, string headerText)
    {
        var headerTexts = await locator.Locator("tr th").AllTextContentsAsync();

        var index = headerTexts
            .Select((text, i) => new { text = text.Trim(), i })
            .FirstOrDefault(h => string.Equals(h.text, headerText.Trim(),
                StringComparison.OrdinalIgnoreCase))
            ?.i ?? -1;

        if (index is -1)
            throw new ArgumentException($"Header '{headerText}' not found");

        return index;
    }
}