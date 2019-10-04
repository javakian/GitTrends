using System;
using System.Net.Http;
using System.Threading.Tasks;
using GitTrends.Shared;
using Refit;

namespace GitTrends.Functions
{
    public class GitHubAuthService : BaseApiService
    {
        public GitHubAuthService(IHttpClientFactory httpClientFactory)
        {
            var client = httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(GitHubConstants.GitHubAuthBaseUrl);

            GitHubAuthClient = RestService.For<IGitHubAuthApi>(client);
        }

        IGitHubAuthApi GitHubAuthClient { get; }

        public Task<GitHubToken> GetGitHubToken(string clientId, string clientSecret, string loginCode, string state) =>
            AttemptAndRetry(() => GitHubAuthClient.GetAccessToken(clientId, clientSecret, loginCode, state));
    }
}
