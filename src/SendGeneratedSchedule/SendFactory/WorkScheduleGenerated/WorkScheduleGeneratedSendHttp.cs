using WorkSchedule.SendGeneratedSchedule.Entities;
using System.Net.Mail;
using System.Text;

namespace WorkSchedule.SendGeneratedSchedule.Send;

public class WorkScheduleGeneratedSendHttp : IWorkScheduleGeneratedSend
{
    public async Task Send(WorkScheduleGenerated schedule)
    {
        using var client = new HttpClient();
        var json = System.Text.Json.JsonSerializer.Serialize(schedule);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        await client.PostAsync(schedule.WebHookUrl, content);
    }
}

