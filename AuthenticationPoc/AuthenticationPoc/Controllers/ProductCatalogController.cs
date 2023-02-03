using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationPoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductCatalogController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<string>> ListProducts()
        {
            return Ok(new List<string>()
            {
                "Produto 1",
                "Produto 2"
            });
        }
    }
}
