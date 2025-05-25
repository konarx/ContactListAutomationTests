Feature: Contact management

    Background: Explore to site
        Given the user navigates to site

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

    Scenario: Try to add a contact with invalid date of birth and validate the error message
        Given the user is logged in
        When the user clicks on "Add a New Contact" button
        Then the user should be on the "Add Contact" page
        When the user attempts to create a contact with invalid date of birth
        And the user submits the contact form
        Then the user should see an error message "Contact validation failed: birthdate: Birthdate is invalid"

    Scenario: Delete an existing contact
        Given the user is logged in
        And a contact exist in table