# GitLab variables configuration provider

### Example:

```csharp
builder.AddGitLab(options => options
    .WithHostUrl("https://gitlab.com")
    .WithAuthenticationToken("abcd")
    .WithProjectId("100500")
    .WithEnvironmentName("Production"));
```
