using Microsoft.AspNetCore.Mvc;

namespace BuberBreakfast.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorsController : ApiController {
        public IActionResult Error() {
            return Problem();
        }
    }
}
