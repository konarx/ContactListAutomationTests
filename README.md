# Contact List Automation Tests

BDD tests (Reqnroll + NUnit) + Playwright browser automation  
for https://thinking-tester-contact-list.herokuapp.com

## Prerequisites
* [.NET 9 SDK](https://dotnet.microsoft.com/download)
* First-time only (after `dotnet build`): `pwsh bin/Debug/net9.0/playwright.ps1 install`

## Quick start

```bash
git clone https://github.com/konarx/ContactListAutomationTests.git
cd ContactListAutomationTests
dotnet build
pwsh bin/Debug/net9.0/playwright.ps1 install
dotnet test             # runs all Reqnroll scenarios via NUnit
