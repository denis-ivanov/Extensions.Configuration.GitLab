using GitLabApiClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Extensions.Configuration.GitLab;

public class GitLabConfigurationSource : IConfigurationSource
{
    private readonly IGitLabClient _gitLabClient;
    private readonly GitLabConfigurationOptions _options;

    public GitLabConfigurationSource(
        [NotNull] IGitLabClient gitLabClient,
        [NotNull] GitLabConfigurationOptions options)
    {
        _gitLabClient = gitLabClient ?? throw new ArgumentNullException(nameof(gitLabClient));
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new GitLabConfigurationProvider(_gitLabClient, _options);
    }
}
