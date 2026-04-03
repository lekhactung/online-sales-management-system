using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL.Services;
using Model.DTOs;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _service;

        // Dependency Injection — .NET tự truyền service vào
        public ProductController(IProductServices service)
        {
            _service = service;
        }

        // GET /api/products
        [HttpGet]
        [Produces("application/json", "application/xml")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            var products = await _service.GetAllAsync();
            return Ok(products);  // HTTP 200
        }

        // GET /api/products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(string id)
        {
            var product = await _service.GetByIdAsync(id);
            if (product == null)
                return NotFound();  // HTTP 404
            return Ok(product);
        }

        // GET /api/products/search?keyword=samsung
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Search([FromQuery] string keyword)
        {
            var result = await _service.SearchAsync(keyword);
            return Ok(result);
        }

        // POST /api/products
        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create([FromBody] CreateProductDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);  // HTTP 400
            var created = await _service.CreateAsync(dto);
            // HTTP 201 + Location header
            return CreatedAtAction(nameof(GetById), new { id = created.ProductId }, created);
        }

        // PUT /api/products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] ProductDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var ok = await _service.UpdateAsync(id, dto);
            if (!ok) return BadRequest("Lỗi khi cập nhật sản phẩm. Vui lòng kiểm tra lại ID, dữ liệu, hoặc thử load lại!");
            return NoContent();
        }

        // DELETE /api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
{
            var ok = await _service.DeleteAsync(id);
            return ok ? NoContent() : NotFound();  // 204 hoặc 404
        }

    }
}
