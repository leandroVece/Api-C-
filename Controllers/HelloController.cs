using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class HelloController : ControllerBase
{
    IHelloService helloWorldService;

    public HelloController(IHelloService helloWorld)
    {
        helloWorldService = helloWorld;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(helloWorldService.GetHelloWorld());
    }

}