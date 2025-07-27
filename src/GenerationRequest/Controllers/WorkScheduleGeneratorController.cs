namespace WorkSchedule.GenerationRequest.Controllers;

using Microsoft.AspNetCore.Mvc;
using WorkSchedule.GenerationRequest.Dtos;
using WorkSchedule.QueueRabbitMQ;
using WorkSchedule.GenerationRequest.Validators;

[ApiController]
[Route("api/workschedule")]
public class WorkScheduleGeneratorController : ControllerBase
{
   private readonly ValidateOrderSchedule Validator;
   private readonly IQueuePubSub QueuePubSub;

   public WorkScheduleGeneratorController(ValidateOrderSchedule validator, IQueuePubSub queuePubSub)
   {
      this.Validator = validator;
      this.QueuePubSub = queuePubSub;
   }

   [HttpPost]
   public async Task<IActionResult> GenerateSchedule([FromBody] WorkScheduleRequestDto orderDto)
   {
      string validationResult = this.Validator.Run(orderDto);
      if (validationResult != "")
         return BadRequest(validationResult);

      bool produced = await this.QueuePubSub.ProduceMessage(orderDto, "GenerationRequest", "GenerationRequest");
      if (!produced)
      {
         return StatusCode(500, new { message = "Erro ao enviar mensagem para a fila" });
      }

      return Accepted(new { message = "Request accepted!" });
   }
}
