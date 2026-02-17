using GitLabApiClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Extensions.Configuration.GitLab;

public static class GitLabConfigurationExtensions
{
    public static IConfigurationBuilder AddGitLab(
        [NotNull] this IConfigurationBuilder builder,
        [NotNull] string hostUrl,
        [NotNull] string projectId,
        [NotNull] string authenticationToken,
        [NotNull] string environmentName)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(hostUrl);
        ArgumentNullException.ThrowIfNull(projectId);
        ArgumentNullException.ThrowIfNull(authenticationToken);
        ArgumentNullException.ThrowIfNull(environmentName);

        var options = new GitLabConfigurationOptions(hostUrl, projectId, authenticationToken, environmentName);
        return builder.AddGitLab(options);
    }

    public static IConfigurationBuilder AddGitLab(
        [NotNull] this IConfigurationBuilder builder,
        [NotNull] Action<GitLabConfigurationOptions> configure)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(configure);

        var options = new GitLabConfigurationOptions();
        configure(options);
        return builder.AddGitLab(options);
    }

    public static IConfigurationBuilder AddGitLab(
        [NotNull] this IConfigurationBuilder builder,
        [NotNull] GitLabConfigurationOptions options)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(options);

        var gitlabClient = new GitLabClient(options.HostUrl, options.AuthenticationToken);
        var source = new GitLabConfigurationSource(gitlabClient, options);
        return builder.Add(source);
    }
}
