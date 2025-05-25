# Contact List Automation Tests

BDD tests (Reqnroll + NUnit) + Playwright browser automation  
for https://thinking-tester-contact-list.herokuapp.com

## Prerequisites
* [.NET 9 SDK](https://dotnet.microsoft.com/download)
* Playwright CLI – run `dotnet tool install --global Microsoft.Playwright.CLI`
* First-time only: `playwright install`

## Quick start

```bash
git clone https://github.com/konarx/ContactListAutomationTests.git
cd ContactListAutomationTests
dotnet build
dotnet test             # runs all Reqnroll scenarios via NUnit
