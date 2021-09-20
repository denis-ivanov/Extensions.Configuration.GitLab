using GitLabApiClient;
using GitLabApiClient.Models.AwardEmojis.Responses;
using GitLabApiClient.Models.Variables.Response;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Configuration.GitLab
{
    public class GitLabConfigurationProvider : ConfigurationProvider, IDisposable
    {
        private readonly IGitLabClient _gitlabClient;
        private readonly GitLabConfigurationOptions _options;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private bool _changeTrackingStarted;

        public GitLabConfigurationProvider(
            [NotNull] IGitLabClient gitlabClient,
            [NotNull] GitLabConfigurationOptions options)
        {
            _gitlabClient = gitlabClient ?? throw new ArgumentNullException(nameof(gitlabClient));
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public override void Load()
        {
            LoadAsync().GetAwaiter().GetResult();
        }

        private async Task LoadVariablesAsync()
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                await WaitForReaload();

                try
                {
                    await LoadAsync();
                }
                catch
                {
                    // ignored
                }
            }
        }

        private async Task WaitForReaload()
        {
            await Task.Delay(_options.ReloadInterval, _cancellationTokenSource.Token).ConfigureAwait(false);
        }

        private async Task LoadAsync()
        {
            var newData = await GetNewDataAsync();

            if (Changed(newData))
            {
                Data = newData;
                OnReload();
            }

            if (!_changeTrackingStarted)
            {
                _changeTrackingStarted = true;
                LoadVariablesAsync();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
            }
        }

        private bool Changed(Dictionary<string, string> newData)
        {
            if (Data.Count != newData.Count)
            {
                return true;
            }

            foreach (var kv in Data)
            {
                if (!newData.TryGetValue(kv.Key, out var value) ||
                    kv.Value != value)
                {
                    return true;
                }
            }

            return false;
        }

        private async Task<Dictionary<string, string>> GetNewDataAsync()
        {
            var variables = await _gitlabClient.Projects.GetVariablesAsync(_options.ProjectId);
            var newData = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach (var variable in variables)
            {
                if (variable.EnvironmentScope == _options.EnvironmentName ||
                    variable.EnvironmentScope == "*")
                {
                    newData[_options.KeyNormalizer(variable.Key)] = variable.Value;
                }
            }

            return newData;
        }
    }
}
