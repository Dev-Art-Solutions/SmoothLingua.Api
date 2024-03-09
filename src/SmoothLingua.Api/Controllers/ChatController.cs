using Microsoft.AspNetCore.Mvc;
using SmoothLingua.Abstractions;
using SmoothLingua.Api.Models;
using SmoothLingua.Api.Services;

namespace SmoothLingua.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        private readonly ILogger<ChatController> _logger;

        public ChatController(IChatService chatService, ILogger<ChatController> logger)
        {
            _chatService = chatService;
            _logger = logger;
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> Train(int id, Domain domain, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Training {id} scenario!");
            await _chatService.Train(id, domain, cancellationToken);

            return Ok();
        }

        [HttpPost("[action]/{id}")]
        public IActionResult Handle(int id, Chat chat, CancellationToken cancellationToken)
        {
            return Ok(_chatService.Handle(id, chat));
        }
    }
}
