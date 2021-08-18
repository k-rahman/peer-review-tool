using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Workshop.Service.API.Domain.Services;
using Workshop.Service.API.Resources;
using Workshop.Service.API.Utils;

namespace Workshop.Service.API
{
	[Route("api/v1/participant")]
	[ApiController]
	public class ParticipantController : ControllerBase
	{


		private readonly ManagementApiAccessTokenClient _apiAccessTokenClient;
		private readonly IParticipantService _participantService;

		public ParticipantController(ManagementApiAccessTokenClient apiAccessTokenClient, IParticipantService participantService)
		{
			_apiAccessTokenClient = apiAccessTokenClient;
			_participantService = participantService;
		}

		[HttpPost]
		[Authorize]
		public async Task<ActionResult> UpdateUserMetadata(SaveParticipantResource profile)
		{

			var participantId = User.Identity.Name;

			var managementApiAccessToken = await _apiAccessTokenClient.GetApiToken(); // 1. Retrieve a Management API token. 

			// Auth0 management api "Update user" endpoint
			var client = new RestClient($"https://peer-review-tool.eu.auth0.com/api/v2/users/{participantId}"); // 2. Call the Management API supplying user_id
			var request = new RestRequest(Method.PATCH);

			request.AddHeader("content-type", "application/json");
			request.AddHeader("authorization", $"Bearer {managementApiAccessToken}");

			request.AddJsonBody(profile);

			IRestResponse response = client.Execute(request);

			if (!response.IsSuccessful)
			{
				return BadRequest();
			}

			// save participant name to participants table
			await _participantService.UpdateParticipant(profile, participantId);

			return Ok("Updated");
		}

	}
}