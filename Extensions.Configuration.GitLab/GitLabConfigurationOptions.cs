using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Extensions.Configuration.GitLab
{
    public class GitLabConfigurationOptions
    {
        public GitLabConfigurationOptions()
        {
        }

        public GitLabConfigurationOptions(
            [NotNull] string hostUrl,
            [NotNull] string projectId,
            [NotNull] string authenticationToken,
            [NotNull] string environmentName)
        {
            HostUrl = hostUrl ?? throw new ArgumentNullException(nameof(hostUrl));
            ProjectId = projectId ?? throw new ArgumentNullException(nameof(projectId));
            AuthenticationToken = authenticationToken ?? throw new ArgumentNullException(nameof(authenticationToken));
            EnvironmentName = environmentName ?? throw new ArgumentNullException(nameof(environmentName));
        }
        
        public TimeSpan ReloadInterval { get; set; } = TimeSpan.FromSeconds(3);
        
        public string HostUrl { get; set; }

        public string AuthenticationToken { get; set; }

        public string ProjectId { get; set; }

        public string EnvironmentName { get; set; } = "*";
        
        public Func<string, string> KeyNormalizer { get; set; } = NormalizeKey;

        public GitLabConfigurationOptions WithReloadInterval(TimeSpan reloadInterval)
        {
            ReloadInterval = reloadInterval;
            return this;
        }

        public GitLabConfigurationOptions WithHostUrl([NotNull] string hostUrl)
        {
            HostUrl = hostUrl ?? throw new ArgumentNullException(nameof(hostUrl));
            return this;
        }

        public GitLabConfigurationOptions WithAuthenticationToken([NotNull] string authenticationToken)
        {
            AuthenticationToken = authenticationToken ?? throw new ArgumentNullException(nameof(authenticationToken));
            return this;
        }

        public GitLabConfigurationOptions WithProjectId([NotNull] string projectId)
        {
            ProjectId = projectId ?? throw new ArgumentNullException(nameof(projectId));
            return this;
        }

        public GitLabConfigurationOptions WithEnvironmentName([NotNull] string environmentName)
        {
            EnvironmentName = environmentName ?? throw new ArgumentNullException(nameof(environmentName));
            return this;
        }

        public GitLabConfigurationOptions WithKeyNormalizer([NotNull] Func<string, string> keyNormalizer)
        {
            KeyNormalizer = keyNormalizer ?? throw new ArgumentNullException(nameof(keyNormalizer));
            return this;
        }
        
        private static string NormalizeKey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return key;
            }

            var segments = Array.ConvertAll(key.Split('_'), e =>
            {
                e = e.ToLower();
                return e.Length <= 1 ? e : char.ToUpper(e[0]) + e.Substring(1);
            });

            return string.Join(ConfigurationPath.KeyDelimiter, segments);
        }
    }
}
