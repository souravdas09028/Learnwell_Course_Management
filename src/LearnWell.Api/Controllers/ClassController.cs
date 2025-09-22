using LearnWell.Application.Common.DTOs;
using LearnWell.Application.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            var userIdClaim = User.FindFirst("id") ?? User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized("User ID claim not found.");

            Guid createdBy = Guid.Parse(userIdClaim.Value);

            var createdClass = await _classService.CreateAsync(dto, createdBy);

            return CreatedAtAction(nameof(GetClass), new { id = createdClass.Id }, createdClass);
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
        public async Task<IActionResult> UpdateClass(Guid id, ClassDto classDto)
        {
            var classEntity = await _classService.GetAsync(id);
            if (classEntity == null) return NotFound();

            await _classService.UpdateAsync(id, classDto);
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
