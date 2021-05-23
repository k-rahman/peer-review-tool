using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Work.Service.API.Domain.Services;
using Work.Service.API.Resources;

namespace Work.Service.API.Controllers
{
        [Route("/api/v1/works")]
        [ApiController]
        public class WorkController : ControllerBase
        {
                private readonly IWorkService _workService;

                public WorkController(IWorkService workService)
                {
                        _workService = workService;
                }

                [HttpGet]
                public async Task<IEnumerable<WorkResource>> GetWorks()
                {
                        return await _workService.GetAsync();
                }

                [HttpGet("{id}")]
                public async Task<IActionResult> GetWorkById(int id)
                {
                        var work = await _workService.GetByIdAsync(id);

                        if (work == null)
                                return NotFound();

                        return Ok(work);
                }

                [HttpPut]
                public async Task<IActionResult> UpdateWork(int id, SaveWorkResource resource)
                {
                        var result = await _workService.UpdateAsync(id, resource);

                        if (!result.Success)
                                return NotFound(result.Message);

                        return Ok(result.Work);
                }
        }
}