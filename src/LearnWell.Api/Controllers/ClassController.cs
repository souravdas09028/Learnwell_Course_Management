using LearnWell.Application.Common.DTOs;
using LearnWell.Application.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnWell.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Staff")]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;

        public ClassController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateClass(CreateClassDto dto)
        {
            await _classService.CreateAsync(dto);
            //return Ok(classEntity);

            return CreatedAtAction(nameof(GetClass), new { id = 0 }, dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClasses()
        {
            var classes = await _classService.GetAllAsync();
            return Ok(classes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClass(Guid id)
        {
            var classEntity = await _classService.GetAsync(id);
            return classEntity == null ? NotFound() : Ok(classEntity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClass(Guid id, CreateClassDto dto)
        {
            var classEntity = await _classService.GetAsync(id);
            if (classEntity == null) return NotFound();

            await _classService.UpdateAsync(id, dto);
            return Ok(classEntity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(Guid id)
        {
            var classEntity = await _classService.GetAsync(id);
            if (classEntity == null) return NotFound();

            await _classService.DeleteAsync(id);
            return Ok("Deleted");
        }
    }
}
