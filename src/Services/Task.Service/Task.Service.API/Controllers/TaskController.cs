using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task.Service.API.Domain.Services;
using Task.Service.API.Resources;

namespace Task.Service.API.Controllers
{
        [Route("api/v1/tasks")]
        [ApiController]
        public class TaskController : ControllerBase
        {
                private readonly ITaskService _taskService;

                public TaskController(ITaskService taskService)
                {
                        _taskService = taskService;
                }

                [HttpGet]
                [Authorize(Roles = "Instructor, Participant")]
                public async Task<ActionResult<IEnumerable<TaskResource>>> GetTasks()
                {
                        // depending on the user role, user id from token will be used to return only tasks that belong to that user
                        var userId = User.Identity.Name;

                        if (User.IsInRole("Instructor"))
                                return Ok(await _taskService.GetByInstructorIdAsync(userId));

                        if (User.IsInRole("Participant"))
                                return Ok(await _taskService.GetByParticipantIdAsync(userId));


                        // if for some reason server "Authorize" rule failed, return Forbidden "403"  
                        return Forbid();
                }

                [HttpGet("{uid}")]
                [Authorize(Roles = "Instructor, Participant")]
                public async Task<IActionResult> GetTaskByUid(Guid uid)
                {
                        var task = await _taskService.GetByUidAsync(uid);

                        if (task == null)
                                return NotFound();

                        return Ok(task);
                }

                [HttpPost]
                [Authorize(Roles = "Instructor")]
                public async Task<IActionResult> createTask([FromForm] SaveTaskResource resource)
                {
                        var InstructorId = User.Identity.Name;

                        var result = await _taskService.InsertAsync(resource, InstructorId);

                        if (!result.Success)
                                return BadRequest(result.Message);

                        return Ok(result.Task);
                }

                [HttpPut("{id}")]
                [Authorize(Roles = "Instructor")]
                public async Task<IActionResult> UpdateTask(int id, SaveTaskResource resource)
                {
                        var result = await _taskService.UpdateAsync(id, resource);

                        if (!result.Success)
                                return NotFound(result.Message);

                        return Ok(result.Task);
                }

                [HttpDelete("{id}")]
                [Authorize]
                public async Task<IActionResult> DeleteTask(int id)
                {
                        var result = await _taskService.DeleteAsync(id);

                        if (!result.Success)
                                return NotFound(result.Message);

                        return Ok(new { id = result.Task.Id });
                }
        }
}