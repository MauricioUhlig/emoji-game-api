using Microsoft.AspNetCore.Mvc;
using uhlig.game.domain.Interfaces.Services;
using uhlig.game.domain.Notifications;
using uhlig.game.domain.ViewModels.Round.Request;

namespace uhlig.game.api.Controllers
{
    public class RoundController : ApiController
    {
        private readonly IRoundService _roundService;
        public RoundController(IRoundService roundService, DomainNotification domainNotification) :base(domainNotification)
        {
            _roundService = roundService;
        }

        [HttpGet]
        public IActionResult GetLastRoundByRoomId([FromQuery] Guid roomId)
        {
            return Result(_roundService.GetLastRoundByRoomId(roomId));
        }

        [HttpGet("new")]
        public IActionResult CreateNewRoundByRoomId([FromQuery] Guid roomId)
        {
            return Result(_roundService.CreateNewRoundByRoomId(roomId));
        }

        [HttpGet("all")]
        public IActionResult GetAllRoundsByRoomId([FromQuery] Guid roomId)
        {
            return Result(_roundService.GetAllRoundsByRoomId(roomId));
        }
        [HttpGet("{id:guid}/join")]
        public IActionResult JoinRound(Guid id, [FromQuery] Guid playerId)
        {
            return Result(_roundService.JoinRound(id, playerId));
        }

        [HttpGet("{id:guid}/phrases")]
        public IActionResult RoundPhrases(Guid id)
        {
            return Result(_roundService.RoundPhrases(id));
        }

        [HttpGet("{id:guid}/result")]
        public IActionResult RoundResult(Guid id)
        {
            return Result(_roundService.RoundResult(id));
        }

        [HttpPost("{id:guid}/submit")]
        public IActionResult Submit(NewPhraseRequestViewModel submit)
        {
            return Result(_roundService.Submit(submit));
        }

        [HttpGet("{id:guid}/vote")]
        public IActionResult Vote([FromQuery] Guid phraseId)
        {
            // TODO: Pegar o id do player que está votando pelo token
            _roundService.Vote(Guid.Empty, phraseId);
            return Result();
        }
    }
}