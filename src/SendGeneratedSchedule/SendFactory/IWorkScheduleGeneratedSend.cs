using WorkSchedule.SendGeneratedSchedule.Entities;

namespace WorkSchedule.SendGeneratedSchedule.Send;

public interface IWorkScheduleGeneratedSend
{
    Task Send(WorkScheduleGenerated schedule);
}

