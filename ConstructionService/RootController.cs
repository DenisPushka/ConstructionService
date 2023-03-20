using Microsoft.AspNetCore.Mvc;

namespace ConstructionService;

public class RootController : Microsoft.AspNetCore.Mvc.Controller
{
    [HttpGet("/")]
    public IActionResult Result()
    {
        return View("Index");
    }
}