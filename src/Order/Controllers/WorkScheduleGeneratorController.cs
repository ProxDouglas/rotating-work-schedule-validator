namespace WorkSchedule.Order.Controllers;

using Microsoft.AspNetCore.Mvc;
using WorkSchedule.Order.Dtos;
using WorkSchedule.Order.QueueProducer;
using WorkSchedule.Order.Validators;

[ApiController]
[Route("api/workschedule")]
public class WorkScheduleGeneratorController : ControllerBase
{
   private readonly ValidateOrderSchedule Validator;
   private readonly IQueueProducer QueueProducer;

   public WorkScheduleGeneratorController(ValidateOrderSchedule validator, IQueueProducer queueProducer)
   {
      this.Validator = validator;
      this.QueueProducer = queueProducer;
   }

   [HttpPost]
   public async Task<IActionResult> GenerateSchedule([FromBody] WorkScheduleOrderDto orderDto)
   {
      string validationResult = this.Validator.Run(orderDto);
      if (validationResult != "")
         return BadRequest(validationResult);

      bool produced = await this.QueueProducer.SendMessage(orderDto, "Order", "Order");
      if (!produced)
      {
         return StatusCode(500, new { message = "Erro ao enviar mensagem para a fila" });
      }

      return Accepted(new { message = "Request accepted!" });
   }
}
