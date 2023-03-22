using Microsoft.AspNetCore.Mvc;
using Server.Interfaces;
using SharedModels.Models;

namespace Server.Controllers;

[ApiController]
public class AuthorizationController : ControllerBase
{
    private readonly IRegisterService _registerService;
    private readonly ISignInService _signInService;

    public AuthorizationController(IRegisterService registerService, ISignInService signInService)
    {
        _registerService = registerService;
        _signInService = signInService;
    }

    [Route("signin")]
    [HttpPost]
    public IActionResult SignIn([FromBody] User user)
    {
        return _signInService.IsValidLoginPass(user)
            ? Ok()
            : BadRequest("Invalid login or pass");
    }

    [Route("registry")]
    [HttpPost]
    public IActionResult Registry([FromBody] User user)
    {
        var result = _registerService.WriteNewUserAsync(user);

        return result.IsValid
            ? Ok()
            : BadRequest(result.Errors.Select(x => x.ErrorMessage).ToList());
    }
}
