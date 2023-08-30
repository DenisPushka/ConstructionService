using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionService.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TopController : ControllerBase
    {
        [HttpGet]
        public async Task GetT ()
        {
            var t = 0;
        } 
    }
}
