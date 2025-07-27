namespace WorkSchedule.Order.Controllers;

using Microsoft.AspNetCore.Mvc;
using WorkSchedule.Order.Dtos;
using WorkSchedule.Order.QueueRabbitMQ;
using WorkSchedule.Order.Validators;

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
   public async Task<IActionResult> GenerateSchedule([FromBody] WorkScheduleOrderDto orderDto)
   {
      string validationResult = this.Validator.Run(orderDto);
      if (validationResult != "")
         return BadRequest(validationResult);

      bool produced = await this.QueuePubSub.ProduceMessage(orderDto, "Order", "Order");
      if (!produced)
      {
         return StatusCode(500, new { message = "Erro ao enviar mensagem para a fila" });
      }

      return Accepted(new { message = "Request accepted!" });
   }
}
