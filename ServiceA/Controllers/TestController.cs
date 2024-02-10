using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using ServiceA.Publisher;

namespace ServiceA.Controllers;
[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly PingPublisher _publisher;
    private readonly IBus _bus;
    public TestController(PingPublisher publisher, IBus bus)
    {
        _publisher = publisher;
        _bus = bus;
    }

    [HttpPost]
    public IActionResult Ping(string name)
    {
        _publisher.Publish(name);
        return NoContent();
    }

    [HttpPost("add-sync")]
    public async Task<IActionResult> AddUserSync(string name, string family)
    {
        if (string.IsNullOrEmpty(name)) return BadRequest("please enter name");

        if (string.IsNullOrEmpty(family)) return BadRequest("please enter family");

        var client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:5035");

        var response = await client.PostAsJsonAsync("user/add-sync", new { name, family });

        response.EnsureSuccessStatusCode();

        return NoContent();
    }

    [HttpPost("add-async")]
    public async Task<IActionResult> AddUser(string name, string family)
    {
        if (string.IsNullOrEmpty(name)) return BadRequest("please enter name");

        if (string.IsNullOrEmpty(family)) return BadRequest("please enter family");

        await _bus.Publish(new UserAdded(name, family));

        return NoContent();
    }

}
