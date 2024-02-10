using Microsoft.AspNetCore.Mvc;

namespace ServiceB.Controllers;
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly UserStorage _userStorage;

    public UserController(UserStorage userStorage)
    {
        _userStorage = userStorage;
    }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        return Ok(_userStorage.ReadAll());
    }

    [HttpPost("add-sync")]
    public IActionResult Post(CreateUserDto dto)
    {
        if (dto.name is null)
            return BadRequest(dto);

        if (dto.family is null)
            return BadRequest();

        _userStorage.AddUser(new(dto.name, dto.family));

        return NoContent();
    }
}

public record CreateUserDto(string name, string family);
