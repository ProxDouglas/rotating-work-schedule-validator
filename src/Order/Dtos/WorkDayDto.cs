namespace WorkSchedule.Order.Dtos;

using System.ComponentModel.DataAnnotations;

using WorkSchedule.Enums;

public class WorkDayDto
{
   [Required(ErrorMessage = "O campo \"Date\" é obrigatório")]
   public DateOnly Date { get; set; }

   public DayOperating DayOperating { get; set; } = DayOperating.Sunday;
}