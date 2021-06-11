using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workshop.Service.API.Domain.Services;
using Workshop.Service.API.Resources;

namespace Workshop.Service.API.Controllers
{
        [Route("api/v1/workshops")]
        [ApiController]
        public class WorkshopController : ControllerBase
        {
                private readonly IWorkshopService _workshopService;

                public WorkshopController(IWorkshopService workshopService)
                {
                        _workshopService = workshopService;
                }

                [HttpGet]
                [Authorize(Roles = "Instructor, Participant")]
                public async Task<ActionResult<IEnumerable<WorkshopResource>>> GetWorkshops()
                {
                        // depending on the user role, user id from token will be used to return only workshops that belong to that user
                        var userId = User.Identity.Name;

                        if (User.IsInRole("Instructor"))
                                return Ok(await _workshopService.GetByInstructorIdAsync(userId));

                        if (User.IsInRole("Participant"))
                                return Ok(await _workshopService.GetByParticipantIdAsync(userId));


                        // if for some reason server "Authorize" rule failed, return Forbidden "403"  
                        return Forbid();
                }

                [HttpGet("{uid}")]
                [Authorize(Roles = "Instructor, Participant")]
                public async Task<IActionResult> GetWorkshopByUid(Guid uid)
                {
                        var workshop = await _workshopService.GetByUidAsync(uid);

                        if (workshop == null)
                                return NotFound();

                        return Ok(workshop);
                }

                [HttpPost]
                [Authorize(Roles = "Instructor")]
                public async Task<IActionResult> CreateWorkshop([FromForm] SaveWorkshopResource resource)
                {
                        var InstructorId = User.Identity.Name;

                        var result = await _workshopService.InsertAsync(resource, InstructorId);

                        if (!result.Success)
                                return BadRequest(result.Message);

                        return Ok(result.Workshop);
                }

                [HttpPut("{id}")]
                [Authorize(Roles = "Instructor")]
                public async Task<IActionResult> UpdateWorkshop(int id, [FromForm] SaveWorkshopResource resource)
                {
                        var result = await _workshopService.UpdateAsync(id, resource);

                        if (!result.Success)
                                return NotFound(result.Message);

                        return Ok(result.Workshop);
                }

                [HttpDelete("{id}")]
                [Authorize]
                public async Task<IActionResult> DeleteWorkshop(int id)
                {
                        var result = await _workshopService.DeleteAsync(id);

                        if (!result.Success)
                                return NotFound(result.Message);

                        return Ok(new { id = result.Workshop.Id });
                }
        }
}