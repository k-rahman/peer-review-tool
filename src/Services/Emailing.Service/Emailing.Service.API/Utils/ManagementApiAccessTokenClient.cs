using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Emailing.Service.API.Utils
{
	public class ManagementApiAccessTokenClient
	{
		private static string clientId => Environment.GetEnvironmentVariable("AUTH0_CLIENT_ID");
		private static string clientSecret => Environment.GetEnvironmentVariable("AUTH0_CLIENT_SECRET");
		private readonly string tokenEndpoint = "https://peer-review-tool.eu.auth0.com/oauth/token";
		private readonly string accessTokenAudience = "https://peer-review-tool.eu.auth0.com/api/v2/";
		private readonly string grant_type = "client_credentials";

		private ConcurrentDictionary<string, AccessTokenItem> _accessTokens = new ConcurrentDictionary<string, AccessTokenItem>();
		// access token placeholder
		private record tokenResponse(string access_token, double expires_in);
		private class AccessTokenItem
		{
			public string AccessToken { get; set; } = string.Empty;
			public DateTime ExpiresIn { get; set; }
		}


		private readonly ILogger<ManagementApiAccessTokenClient> _logger;

		public ManagementApiAccessTokenClient(
		    ILoggerFactory loggerFactory)
		{
			_logger = loggerFactory.CreateLogger<ManagementApiAccessTokenClient>();
		}

		public async Task<string> GetApiToken()
		{

			if (_accessTokens.ContainsKey(accessTokenAudience))
			{
				var accessToken = _accessTokens.GetValueOrDefault(accessTokenAudience);
				if (accessToken.ExpiresIn > DateTime.UtcNow)
				{
					return accessToken.AccessToken;
				}
				else
				{
					// remove
					_accessTokens.TryRemove(accessTokenAudience, out AccessTokenItem accessTokenItem);
				}
			}

			_logger.LogDebug($"GetApiToken new from Auth0 token server for {accessTokenAudience}");

			// add
			var newAccessToken = await getApiToken();
			_accessTokens.TryAdd(accessTokenAudience, newAccessToken);

			return newAccessToken.AccessToken;
		}

		private async Task<AccessTokenItem> getApiToken()
		{
			var clientCredentials = new
			{
				client_id = clientId,
				client_secret = clientSecret,
				audience = accessTokenAudience,
				grant_type = grant_type,
			};

			var client = new RestClient(tokenEndpoint);
			var request = new RestRequest(Method.POST);
			request.AddHeader("content-type", "application/json");
			request.AddJsonBody(clientCredentials);

			try
			{
				IRestResponse response = await client.ExecuteAsync(request);
				var token = System.Text.Json.JsonSerializer.Deserialize<tokenResponse>(response.Content);

				if (!response.IsSuccessful)
				{
					_logger.LogError($"tokenResponse.IsError Status code: {response.IsSuccessful}, Error: {response.ErrorMessage}");
					throw new ApplicationException($"Status code: {response.IsSuccessful}, Error: {response.ErrorMessage}");
				}

				return new AccessTokenItem
				{
					ExpiresIn = DateTime.UtcNow.AddSeconds(token.expires_in),
					AccessToken = token.access_token
				};

			}
			catch (Exception e)
			{
				_logger.LogError($"Exception {e}");
				throw new ApplicationException($"Exception {e}");
			}
		}
	}
}