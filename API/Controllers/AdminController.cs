using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "SuperAdmin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminServices _adminServices;

        public AdminController(IAdminServices adminServices)
        {
            _adminServices = adminServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminAccount>>> GetAll()
        {
            return Ok(await _adminServices.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdminAccount>> GetById(int id)
        {
            var admin = await _adminServices.GetByIdAsync(id);
            if (admin == null) return NotFound();
            return Ok(admin);
        }

        [HttpPost]
        public async Task<ActionResult<AdminAccount>> Create(AdminAccount admin)
        {
            var created = await _adminServices.CreateAsync(admin);
            return CreatedAtAction(nameof(GetById), new { id = created.AdminId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AdminAccount admin)
        {
            var success = await _adminServices.UpdateAsync(id, admin);
            if (!success) return BadRequest();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _adminServices.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
