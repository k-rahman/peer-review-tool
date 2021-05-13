using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Task.Service.API.Resources;

namespace Task.Service.API.Controllers
{

  [Route("api/v1/[controller]")]
  [ApiController]
  public class TaskController : ControllerBase
  {
    private static readonly List<TaskDto> tasks = new()
    {
      new TaskDto(
        Guid.NewGuid(),
        "first task", "do that and this",
        "http://peerReviewTool.com/task/1",
        DateTimeOffset.Now,
        DateTimeOffset.Now,
        DateTimeOffset.Now,
        DateTimeOffset.Now,
        DateTimeOffset.Now,
        DateTimeOffset.Now,
        1
      ),
      new TaskDto(
        Guid.NewGuid(),
        "second task", "do those and these",
        "http://peerReviewTool.com/task/2",
        DateTimeOffset.Now,
        DateTimeOffset.Now,
        DateTimeOffset.Now,
        DateTimeOffset.Now,
        DateTimeOffset.Now,
        DateTimeOffset.Now,
        2
      ),
      new TaskDto(
        Guid.NewGuid(),
        "third task", "do this and that",
        "http://peerReviewTool.com/task/3",
        DateTimeOffset.Now,
        DateTimeOffset.Now,
        DateTimeOffset.Now,
        DateTimeOffset.Now,
        DateTimeOffset.Now,
        DateTimeOffset.Now,
        3
      )
    };

    // GET api/v1/[controller]/tasks
    [HttpGet]
    [Route("tasks")]
    public IEnumerable<TaskDto> GetTasks()
    {
      return tasks;
    }

    [HttpGet("{id}")]
    public TaskDto GetTaskById(Guid id)
    {
      var task = tasks.Where(item => item.Id == id).SingleOrDefault();
      return task;
    }

    [HttpPost]
    public ActionResult createTask(TaskDto taskToCreate)
    {
      var task = new TaskDto(
        Guid.NewGuid(),
        taskToCreate.Name,
        taskToCreate.Description,
        taskToCreate.link,
        DateTimeOffset.Now,
        DateTimeOffset.Now,
        DateTimeOffset.Now,
        DateTimeOffset.Now,
        DateTimeOffset.Now,
        DateTimeOffset.Now,
        3
      );
      tasks.Add(task);

      return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateTask(Guid id, TaskDto taskToUpdate)
    {
      var task = tasks.Where(task => task.Id == id).SingleOrDefault();
      var updatedTask = taskToUpdate with
      {
        Name = task.Name,
        Description = task.Description,
        link = task.link,
      };

      var index = tasks.FindIndex(task => task.Id == id);
      tasks[index] = updatedTask;

      return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTask(Guid id)
    {
      var index = tasks.FindIndex(task => task.Id == id);
      tasks.RemoveAt(index);

      return NoContent();
    }
  }
}