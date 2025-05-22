Feature: Contact management

    Background: Explore to site
        Given the admin user logs in to site

    Scenario: Sign up with a new user, add a new contact and validate it on contact details page
        Given the user is on the Sign Up page
        When the user fills in the sign-up form with valid details
        And the user submits the sign-up form
        Then the user should be logged in
        When the user clicks on "Add a New Contact" button
        Then the user should be on the "Add Contact" page
        When the user fills in the contact form with valid details
        And the user submits the contact form
        Then the user should be on the contact details page
        And the contact should be created successfully

    Scenario: Reject contact with an invalid birth date
        Given I am logged in
        When I attempt to create a contact called "Bad Date" born "31/02/1999"
        Then I should see a birth-date validation error

    Scenario: Delete an existing contact
        Given a contact "Jane Smith" exists
        When I delete the contact "Jane Smith"
        Then the contact "Jane Smith" should no longer appear