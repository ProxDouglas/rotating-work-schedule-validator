using WorkSchedule.SendGeneratedSchedule.Entities;

namespace WorkSchedule.SendGeneratedSchedule.Send;

public class WorkScheduleSendFactory
{
   public IWorkScheduleGeneratedSend Build(WorkScheduleGenerated schedule)
   {
      if (!string.IsNullOrEmpty(schedule.WebHookUrl))
      {
         return new WorkScheduleGeneratedSendHttp();
      }
      else if (!string.IsNullOrEmpty(schedule.Email))
      {
         return new WorkScheduleGeneratedSendEmail();
      }
      throw new ArgumentException("Invalid schedule");
   }
}
