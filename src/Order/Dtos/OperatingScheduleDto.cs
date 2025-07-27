namespace WorkSchedule.Order.Dtos;

using System.ComponentModel.DataAnnotations;

using WorkSchedule.Enums;

public class OperatingScheduleDto
{
   [Required(ErrorMessage = "O campo \"Start\" é obrigatório e deve estar no formato HH:mm:ss")]
   public TimeSpan Start { get; set; }

   [Required(ErrorMessage = "O campo \"End\" é obrigatório e deve estar no formato HH:mm:ss")]
   public TimeSpan End { get; set; }

   [Required(ErrorMessage = "O campo \"DayOperating\" é obrigatório")]
   [Range(0, 7, ErrorMessage = "O \"DayOperating\" deve estar entre 0 e 7 dias semanais")]
   public DayOperating DayOperating { get; set; }
}