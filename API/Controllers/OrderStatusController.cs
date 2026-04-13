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
    [Authorize(Roles = "OrderAdmin,SuperAdmin")]
    public class OrderStatusController : ControllerBase
    {
        private readonly IOrderStatusServices _service;

        public OrderStatusController(IOrderStatusServices service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderStatusDto>>> GetAll()
        {
            var statuses = await _service.GetAllAsync();
            return Ok(statuses);
        }
    }
}
