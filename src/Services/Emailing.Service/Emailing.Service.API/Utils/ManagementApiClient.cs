using System;
using System.Text.Json;
using Emailing.Service.API.Utils;
using RestSharp;

namespace Emailing.Service.ApI.Utils
{
        public class ManagementApiClient
        {
                // Auth0 management api "Create user" endpoint
                private readonly string usersEndpoint = "https://peer-review-tool.eu.auth0.com/api/v2/users";

                // Auth0 management api "Create a password change ticket" endpoint
                private readonly string changePasswordTicketEndpoint = "https://peer-review-tool.eu.auth0.com/api/v2/tickets/password-change";

                // response models 
                private record _user(string user_id, bool email_verified);
                private record _changePasswordTicket(string ticket);

                private ManagementApiAccessTokenClient _accessTokenClient;

                public ManagementApiClient(ManagementApiAccessTokenClient accessTokenClient)
                {
                        _accessTokenClient = accessTokenClient;
                }

                public async System.Threading.Tasks.Task<bool> UserVerified(string userId)
                {
                        var user = await GetUserById(userId);
                        return user.email_verified;
                }

                public async System.Threading.Tasks.Task<string> CreateChangePasswordTicket(string userId, string resultUrl)
                {
                        var accessToken = await _accessTokenClient.GetApiToken();
                        var client = new RestClient(changePasswordTicketEndpoint);
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("content-type", "application/json");
                        request.AddHeader("authorization", $"Bearer {accessToken}");

                        // Create reset password ticket
                        request.AddJsonBody(new { result_url = resultUrl, user_id = userId, mark_email_as_verified = true });

                        try
                        {
                                IRestResponse response = await client.ExecuteAsync(request);
                                return System.Text.Json.JsonSerializer.Deserialize<_changePasswordTicket>(response.Content).ticket;
                        }
                        catch (Exception e)
                        {
                                throw new ApplicationException($"Exception {e}");

                        }

                }

                private async System.Threading.Tasks.Task<_user> GetUserById(string userId)
                {
                        var accessToken = await _accessTokenClient.GetApiToken();
                        var client = new RestClient($"{usersEndpoint}/{userId}");
                        var request = new RestRequest(Method.GET);
                        request.AddHeader("content-type", "application/json");
                        request.AddHeader("authorization", $"Bearer {accessToken}");

                        try
                        {
                                IRestResponse response = await client.ExecuteAsync(request);

                                return JsonSerializer.Deserialize<_user>(response.Content);
                        }
                        catch (Exception e)
                        {

                                throw new ApplicationException($"Exception {e}");
                        }
                }
        }
}