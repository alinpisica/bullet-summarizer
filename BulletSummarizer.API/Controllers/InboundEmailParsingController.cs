using BulletSummarizer.Core.DTOs;
using BulletSummarizer.Core.Models;
using BulletSummarizer.Infrastructure.Repository.Interfaces;
using BulletSummarizer.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BulletSummarizer.API.Controllers
{
    [ApiController]
    [Route("inbound-email-parsing")]
    public class InboundEmailParsingController : ControllerBase
    {
        private readonly ILogger<InboundEmailParsingController> _logger;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IReasoningService _reasoningService;

        public InboundEmailParsingController(ILogger<InboundEmailParsingController> logger, IUnitOfWork unitOfWork, IReasoningService reasoningService)
        {
            _logger = logger;

            _unitOfWork = unitOfWork;
            _reasoningService = reasoningService;
        }

        [HttpPost]
        public IActionResult Get(InboundPostmarkPayload payload)
        {
            _unitOfWork.InboundEmailRepository.AddInboundEmail(new()
            {
                Content = JsonSerializer.Serialize(payload)
            }); ;

            _reasoningService.SummarizeContent(payload.TextBody);

            _unitOfWork.Commit();

            return Ok();
        }

        [HttpPost("summarize")]
        public async Task<IActionResult> Summarize([FromBody] string content)
        {
            Summarization? summarization = _reasoningService.SummarizeContent(content);

            if (summarization == null)
            {
                return BadRequest();
            }

            int summarizationId = await _unitOfWork.SummarizationRepository.AddSummarization(summarization);

            async Task AddEntitiesAsync<T>(IEnumerable<T>? entities, Func<T, Task> addFunc) where T : SummarizationEntityBase
            {
                if (entities == null)
                {
                    return;
                }

                foreach (var entity in entities)
                {
                    entity.SummarizationId = summarizationId;

                    await addFunc(entity);
                }
            }

            await AddEntitiesAsync(summarization.Participants, _unitOfWork.SummarizationRepository.AddParticipant);
            await AddEntitiesAsync(summarization.ActionItems, _unitOfWork.SummarizationRepository.AddActionItem);
            await AddEntitiesAsync(summarization.Decisions, _unitOfWork.SummarizationRepository.AddDecision);
            await AddEntitiesAsync(summarization.KeyDatesAndDeadlines, _unitOfWork.SummarizationRepository.AddKeyDate);
            await AddEntitiesAsync(summarization.Bullets, _unitOfWork.SummarizationRepository.AddBullet);

            _unitOfWork.Commit();

            return Ok(content);
        }
    }
}
