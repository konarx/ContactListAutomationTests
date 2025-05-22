using System.Collections;
using System.Text;
using ContactListAutomationTests.Infrastructure;
using Reqnroll.BoDi;

namespace ContactListAutomationTests.Hooks;

[Binding]
public sealed class ScenarioLevelHooks(
    IObjectContainer objectContainer,
    IBrowser browser,
    ScenarioContext scenarioContext)
{
    private IBrowserContext _browserContext;
    private IPage _page;

    [BeforeScenario(Order = 1)]
    public async Task BeforeScenario()
    {
        //Reuse the state.json file if it exists for non login scenarios
        var path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "state.json"));
        TestConfiguration.BrowserContextOptions.StorageStatePath =
            !scenarioContext.ScenarioInfo.CombinedTags.Contains("login") && File.Exists(path) ? path : null;

        _browserContext = await browser.NewContextAsync(TestConfiguration.BrowserContextOptions);

        _page = await _browserContext.NewPageAsync();

        _page.SetDefaultTimeout((float)TestConfiguration.BrowserOptions!.Timeout!);
        _page.SetDefaultNavigationTimeout((float)TestConfiguration.BrowserOptions!.Timeout!);
        Assertions.SetDefaultExpectTimeout((float)TestConfiguration.BrowserOptions!.Timeout!);

        objectContainer.RegisterInstanceAs(_browserContext);
        objectContainer.RegisterInstanceAs(_page);
    }

    [BeforeScenario(Order = 2)]
    public async Task StartTracing()
    {
        await _browserContext.Tracing.StartAsync(new TracingStartOptions
        {
            Screenshots = true,
            Snapshots = true,
            Sources = true,
            Title = BuildName(scenarioContext.ScenarioInfo),
            Name = BuildName(scenarioContext.ScenarioInfo)
        });
    }

    [AfterScenario(Order = 2)]
    public async Task AfterScenario()
    {
        await _page.CloseAsync();
        await _browserContext.CloseAsync();
    }

    [AfterScenario(Order = 1)]
    public async Task StopTracing()
    {
        if (scenarioContext.ScenarioExecutionStatus is not ScenarioExecutionStatus.OK)
        {
            var fileName = BuildFilename(scenarioContext.ScenarioInfo, "zip", "trace");

            var tracePath = Path.Combine("traces", fileName);
            await _browserContext.Tracing.StopAsync(new TracingStopOptions
            {
                Path = tracePath
            });
        }
        else
        {
            // Stop and discard the trace if the scenario passes
            await _browserContext.Tracing.StopAsync();
        }
    }

    [AfterScenario(Order = 0)]
    public async Task AfterScenario(ScenarioContext scenarioContext)
    {
        if (scenarioContext.ScenarioExecutionStatus is ScenarioExecutionStatus.TestError)
        {
            var fileName = BuildFilename(scenarioContext.ScenarioInfo, "png");

            await _page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = Path.Combine("screenshots", fileName)
            });
        }
    }

    private static string BuildFilename(ScenarioInfo scenarioInfo, string fileType, string? prefix = null)
    {
        var name = BuildName(scenarioInfo);

        if (string.IsNullOrWhiteSpace(name)) name = string.Empty;

        // Add a timestamp
        var timestamp = DateTime.UtcNow.ToString("yyyyMMdd_HHmmss");

        var filename = $"{name}_{timestamp}.{fileType}";

        return prefix is null ? filename : $"{prefix}_{filename}";
    }

    private static string BuildName(ScenarioInfo scenarioInfo)
    {
        var words = scenarioInfo.Title
            .Replace('-', ' ')
            .Replace(',', ' ')
            .Replace('/', ' ')
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (words.Length is 0) return string.Empty;

        var title = new StringBuilder(words[0].ToLowerInvariant());

        // Capitalize the first letter of the remaining words
        foreach (var word in words[1..])
            title.Append(char.ToUpperInvariant(word[0])).Append(word[1..].ToLowerInvariant());

        // Convert the arguments into a formatted string if present
        var arguments = scenarioInfo.Arguments is { Count: > 0 }
            ? string.Join('&', scenarioInfo.Arguments
                .Cast<DictionaryEntry>()
                .Select(arg => $"{arg.Key}{arg.Value}"))
            : null;

        return arguments is null ? title.ToString() : $"{title}With{arguments}";
    }
}