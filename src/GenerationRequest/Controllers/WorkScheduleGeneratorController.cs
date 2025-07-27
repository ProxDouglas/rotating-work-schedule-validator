namespace WorkSchedule.GenerationRequest.Controllers;

using Microsoft.AspNetCore.Mvc;
using WorkSchedule.GenerationRequest.Dtos;
using WorkSchedule.QueueRabbitMQ;
using WorkSchedule.GenerationRequest.Validators;
using WorkSchedule.GenerationRequest.Mapper;

[ApiController]
[Route("api/workschedule")]
public class WorkScheduleGeneratorController : ControllerBase
{
   private readonly ValidateOrderSchedule Validator;
   private readonly IQueuePubSub QueuePubSub;
   private readonly WorkScheduleRequestMapper Mapper;

   public WorkScheduleGeneratorController(ValidateOrderSchedule validator, IQueuePubSub queuePubSub, WorkScheduleRequestMapper mapper)
   {
      this.Validator = validator;
      this.QueuePubSub = queuePubSub;
      this.Mapper = mapper;
   }

   [HttpPost]
   public async Task<IActionResult> GenerateSchedule([FromBody] WorkScheduleRequestDto generationRequestDto)
   {
      string validationResult = this.Validator.Run(generationRequestDto);
      if (validationResult != "")
         return BadRequest(validationResult);
      
      var generationRequest = this.Mapper.toDto(this.Mapper.toEntity(generationRequestDto));

      bool produced = await this.QueuePubSub.ProduceMessage(generationRequest, "GenerationRequest", "GenerationRequest");
      if (!produced)
      {
         return StatusCode(500, new { message = "Erro ao enviar mensagem para a fila" });
      }

      return Accepted(new { message = "Request accepted!" });
   }
}
