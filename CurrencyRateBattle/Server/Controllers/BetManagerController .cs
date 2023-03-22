
using Microsoft.AspNetCore.Mvc;
using Server.Interfaces;
using SharedModels.Models;

namespace Server.Controllers;

[ApiController]
public class BetManagerController : ControllerBase
{
    private readonly IBetRegistryService _betRegistryService;

    public BetManagerController(IBetRegistryService betRegistryService)
    {
        _betRegistryService = betRegistryService;
    }

    [Route("new_bet")]
    [HttpPost]
    public IActionResult AddNewBet([FromBody] KeyValuePair<string, Bet> roomIdNewBet)
    {
        //validate input bet
        var result = _betRegistryService.AddBet(roomIdNewBet.Value, roomIdNewBet.Key);

        return result.IsValid
            ? Ok()
            : BadRequest(result.Errors.Select(x => x.ErrorMessage).ToList());
    }
}
