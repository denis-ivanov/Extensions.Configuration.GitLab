using GitLabApiClient;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Extensions.Configuration.GitLab
{
    public static class GitLabConfigurationExtensions
    {
        public static IConfigurationBuilder AddGitLab(
            [NotNull] this IConfigurationBuilder builder,
            [NotNull] string hostUrl,
            [NotNull] string projectId,
            [NotNull] string authenticationToken,
            [NotNull] string environmentName)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (hostUrl == null)
            {
                throw new ArgumentNullException(nameof(hostUrl));
            }

            if (projectId == null)
            {
                throw new ArgumentNullException(nameof(projectId));
            }

            if (authenticationToken == null)
            {
                throw new ArgumentNullException(nameof(authenticationToken));
            }

            if (environmentName == null)
            {
                throw new ArgumentNullException(nameof(environmentName));
            }
            
            var options = new GitLabConfigurationOptions(hostUrl, projectId, authenticationToken, environmentName);
            return builder.AddGitLab(options);
        }

        public static IConfigurationBuilder AddGitLab(
            [NotNull] this IConfigurationBuilder builder,
            [NotNull] Action<GitLabConfigurationOptions> configure)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            var options = new GitLabConfigurationOptions();
            configure(options);
            return builder.AddGitLab(options);
        }

        public static IConfigurationBuilder AddGitLab(
            [NotNull] this IConfigurationBuilder builder,
            [NotNull] GitLabConfigurationOptions options)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var gitlabClient = new GitLabClient(options.HostUrl, options.AuthenticationToken);
            var source = new GitLabConfigurationSource(gitlabClient, options);
            return builder.Add(source);
        }
    }
}
