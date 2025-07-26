namespace WorkSchedule.Order.Controllers;

using Microsoft.AspNetCore.Mvc;
using WorkSchedule.Order.Dtos;
using WorkSchedule.Order.Validators;

[ApiController]
[Route("api/workschedule")]
public class WorkScheduleGeneratorController : ControllerBase
{
   private readonly ValidateOrderSchedule Validator;

   public WorkScheduleGeneratorController(ValidateOrderSchedule validator)
   {
      this.Validator = validator;
   }

   [HttpPost]
   public async Task<IActionResult> GenerateSchedule([FromBody] WorkScheduleOrderDto orderDto)
   {
      string validationResult = this.Validator.Run(orderDto);
      if (validationResult != "")
         return BadRequest(validationResult);

      return Accepted(new { message = "Request accepted!" });
   }
}
