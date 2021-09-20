# GitLab variables configuration provider
[![GitHub Tag](https://img.shields.io/github/tag/denis-ivanov/Microsoft.Extensions.Configuration.GitLab.svg?style=flat-square)](https://github.com/denis-ivanov/Microsoft.Extensions.Configuration.GitLab/releases)
[![NuGet Count](https://img.shields.io/nuget/dt/Microsoft.Extensions.Configuration.GitLab.svg?style=flat-square)](https://www.nuget.org/packages/Microsoft.Extensions.Configuration.GitLab/)
[![Issues Open](https://img.shields.io/github/issues/denis-ivanov/Microsoft.Extensions.Configuration.GitLab.svg?style=flat-square)](https://github.com/denis-ivanov/Microsoft.Extensions.Configuration.GitLab/issues)

### Example:

```csharp
builder.AddGitLab(options => options
    .WithHostUrl("https://gitlab.com")
    .WithAuthenticationToken("abcd")
    .WithProjectId("100500")
    .WithEnvironmentName("Production"));
```
