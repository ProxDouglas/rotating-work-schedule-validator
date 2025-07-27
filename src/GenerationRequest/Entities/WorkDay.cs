namespace WorkSchedule.GenerationRequest.Entities;

using System.ComponentModel.DataAnnotations;
using WorkSchedule.Enums;

public class WorkDay
{
   [Required]
   public DateOnly EffectiveDate { get; set; }

   [Required] // Campo obrigatório
   public DayOperating DayOperating { get; set; }
}
