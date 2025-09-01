using Application.Services;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatCalculatorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ChatService _chatService;

        public ChatController(ChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost("calculate")]
        public IActionResult Calculate([FromBody] ChatRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Expression))
                return BadRequest(new { message = "Expression is required" });

            var result = _chatService.ProcessExpression(request.UserId, request.Expression);
            return Ok(result);
        }
    }

}
