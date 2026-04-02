using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryServices _service;

        public ProductCategoryController(IProductCategoryServices service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCategoryDto dto)
        {
            var id = await _service.CreateAsync(dto);
            return Ok(new { CreatedId = id });
        }
    }
}
