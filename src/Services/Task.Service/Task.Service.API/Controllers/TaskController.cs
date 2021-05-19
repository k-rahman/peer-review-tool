using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
                public async Task<IEnumerable<TaskResource>> GetTasks()
                {
                        return await _taskService.GetAsync();
                }

                [HttpGet("{id}")]
                public async Task<IActionResult> GetTaskById(int id)
                {
                        var task = await _taskService.GetByIdAsync(id);

                        if (task == null)
                                return NotFound();

                        return Ok(task);
                }

                [HttpPost]
                public async Task<IActionResult> createTask(SaveTaskResource resource)
                {
                        var result = await _taskService.InsertAsync(resource);

                        if (!result.Success)
                                return BadRequest(result.Message);

                        return Ok(result.Task);
                }

                [HttpPut("{id}")]
                public async Task<IActionResult> UpdateTask(int id, SaveTaskResource resource)
                {
                        var result = await _taskService.UpdateAsync(id, resource);

                        if (!result.Success)
                                return NotFound(result.Message);

                        return Ok(result.Task);
                }

                [HttpDelete("{id}")]
                public async Task<IActionResult> DeleteTask(int id)
                {
                        var result = await _taskService.DeleteAsync(id);

                        if (!result.Success)
                                return NotFound(result.Message);

                        return Ok(new { id = result.Task.Id });
                }
        }
}