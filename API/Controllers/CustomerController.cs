using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "CustomerAdmin,SuperAdmin")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerServices _service;

        public CustomerController(ICustomerServices service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAll()
        {
            var customers = await _service.GetAllAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetById(string id)
        {
            var customer = await _service.GetByIdAsync(id);
            if (customer == null)
                return NotFound();
            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateCustomerDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdId = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = createdId }, new { CustomerId = createdId });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] CustomerDto dto)
        {
            var ok = await _service.UpdateAsync(id, dto);
            return ok ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var ok = await _service.DeleteAsync(id);
            return ok ? NoContent() : NotFound();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> Search([FromQuery] string name)
        {
            var result = await _service.SearchByNameAsync(name);
            return Ok(result);
        }

        [HttpGet("searchByPhone")]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> SearchByPhone([FromQuery] string phone)
        {
            var result = await _service.SearchByPhoneAsync(phone);
            return Ok(result);
        }
    }
}
