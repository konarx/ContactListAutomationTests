namespace ContactListAutomationTests.Models;

public class DTOs
{
    public record User(string FirstName, string LastName, string Email, string Password);

    public record Contact(
        string FirstName,
        string LastName,
        string Birthdate,
        string Email,
        string Phone,
        string Street1,
        string Street2,
        string City,
        string StateProvince,
        string PostalCode,
        string Country
    );
}