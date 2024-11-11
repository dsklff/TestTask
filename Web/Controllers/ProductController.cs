using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadProducts(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file provided");

            var extension = Path.GetExtension(file.FileName);

            if (extension != ".xlsx")
                return BadRequest("Invalid file format. Please upload an .xlsx file.");

            using var stream = file.OpenReadStream();
            var result = await _productService.ImportProductsFromExcel(stream);

            if (result)
                return Ok("Products imported successfully");

            return StatusCode(500, "Something went wrong");
        }
    }
}
