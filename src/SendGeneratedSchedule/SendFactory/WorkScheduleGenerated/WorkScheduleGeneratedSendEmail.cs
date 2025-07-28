using WorkSchedule.SendGeneratedSchedule.Entities;
using System.Net.Mail;
using System.Text;

namespace WorkSchedule.SendGeneratedSchedule.Send;

public class WorkScheduleGeneratedSendEmail : IWorkScheduleGeneratedSend
{
    private string SmtpServer { get; }
    private int SmtpPort { get; }
    private string SmtpUser { get; }
    private string SmtpPass { get; }
    private bool EnableSsl { get; }
    public WorkScheduleGeneratedSendEmail()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
            .Build();

        var smtpSection = config.GetSection("SMTP");
        SmtpServer = smtpSection["Server"] ?? "";
        SmtpPort = int.TryParse(smtpSection["Port"], out var port) ? port : 0;
        SmtpUser = smtpSection["Username"] ?? "";
        SmtpPass = smtpSection["Password"] ?? "";
        EnableSsl = bool.TryParse(smtpSection["EnableSsl"], out var enableSsl) && enableSsl;
    }
    public async Task Send(WorkScheduleGenerated schedule)
    {
        var csv = GenerateCsv(schedule);

        using var mail = new MailMessage();
        mail.To.Add(schedule.Email);
        mail.Subject = "Work Schedule";
        mail.Body = "Segue em anexo o cronograma em CSV.";
        mail.Attachments.Add(new Attachment(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(csv)), "schedule.csv"));

        using var smtp = new SmtpClient(SmtpServer)
        {
            Port = SmtpPort,
            Credentials = new System.Net.NetworkCredential(SmtpUser, SmtpPass),
            EnableSsl = EnableSsl
        };
        await smtp.SendMailAsync(mail);
    }

    private string GenerateCsv(WorkScheduleGenerated schedule)
    {
        var sb = new StringBuilder();
        // Obter todos os dias únicos
        var allDays = schedule.EmployeeSchedule
            .SelectMany(e => e.WorkDays)
            .Select(wd => wd.Date)
            .Distinct()
            .OrderBy(d => d)
            .ToList();

        // Cabeçalho: Nome, Dia1, Dia2, ...
        sb.Append("Nome");
        foreach (var day in allDays)
        {
            sb.Append($",{day:yyyy-MM-dd}");
        }
        sb.AppendLine();

        // Linhas dos colaboradores
        foreach (var emp in schedule.EmployeeSchedule)
        {
            sb.Append(emp.Name);
            foreach (var day in allDays)
            {
                // Encontrar todos os turnos do colaborador neste dia
                var shifts = emp.WorkDays
                    .Where(wd => wd.Date == day)
                    .Select(wd => $"{wd.Start:hh\\:mm}-{wd.End:hh\\:mm}")
                    .ToList();
                var cell = shifts.Count > 0 ? string.Join("/", shifts) : "";
                sb.Append($",{cell}");
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }
}

