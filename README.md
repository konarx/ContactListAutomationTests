# Contact List Automation Tests

BDD tests (Reqnroll + NUnit) + Playwright browser automation  
for https://thinking-tester-contact-list.herokuapp.com

## Prerequisites

* [.NET 9 SDK](https://dotnet.microsoft.com/download)
* First-time only (after `dotnet build`): `pwsh bin/Debug/net9.0/playwright.ps1 install`

## Quick start

### Running the tests in GitHub Actions

This repository comes with a **Playwright-BDD Tests** workflow:

| Trigger            | What happens                                                                                                                                                                        |
|--------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| **Push to `main`** | The workflow builds the project, installs Playwright browsers, runs all Reqnroll + NUnit tests, reports results via a GitHub Check, and uploads the raw `.trx` file as an artifact. |
| **Pull request**   | The same steps run automatically for every PR, so you can see a green ✓ or red ✗ before merging.                                                                                    |
| **Manual run**     | Thanks to `workflow_dispatch` you can kick off the tests on any branch at any time.                                                                                                 |

### To run it manually

1. In the GitHub UI, click **Actions**.
2. Pick **Playwright-BDD Tests** from the list of workflows on the left.
3. Press **_Run workflow_** (upper-right), choose the branch you want, and hit **Run**.
4. Watch the job log in real time or wait for the green ✓ / red ✗ badge in the Checks tab.

> **Tip:** if a test fails you’ll see inline annotations in the PR/files tab pointing to the exact line, plus a summary
> table in the job output.

### Running the tests locally

```bash
git clone https://github.com/konarx/ContactListAutomationTests.git
cd ContactListAutomationTests
dotnet build
pwsh bin/Debug/net9.0/playwright.ps1 install
dotnet test             # runs all Reqnroll scenarios via NUnit
