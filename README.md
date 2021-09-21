# GitLab variables configuration provider
[![GitHub Tag](https://img.shields.io/github/tag/denis-ivanov/Extensions.Configuration.GitLab.svg?style=flat-square)](https://github.com/denis-ivanov/Extensions.Configuration.GitLab/releases)
[![NuGet Count](https://img.shields.io/nuget/dt/Extensions.Configuration.GitLab.svg?style=flat-square)](https://www.nuget.org/packages/Extensions.Configuration.GitLab/)
[![Issues Open](https://img.shields.io/github/issues/denis-ivanov/Extensions.Configuration.GitLab.svg?style=flat-square)](https://github.com/denis-ivanov/Extensions.Configuration.GitLab/issues)

### Example:

```csharp
builder.AddGitLab(options => options
    .WithHostUrl("https://gitlab.com")
    .WithAuthenticationToken("abcd")
    .WithProjectId("100500")
    .WithEnvironmentName("Production"));
```
