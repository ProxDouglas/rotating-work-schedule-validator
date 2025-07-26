namespace rotating_work_schedule.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/workschedule")]
public class WorkScheduleGeneratorController() : ControllerBase
{

   [HttpPost]
   public async Task<IActionResult> GenerateSchedule()
   {


      return Ok(new
      {
         Message = "Work schedule accepted successfully.",
      });
   }
}
