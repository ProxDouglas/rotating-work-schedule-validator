namespace rotating_work_schedule.Entities;

using System.ComponentModel.DataAnnotations;
using RotatingWorkSchedule.Enums;

public class WorkDay
{
   [Required]
   public DateTime EffectiveDate { get; set; }

   [Required] // Campo obrigatório
   public DayOperating DayOperating { get; set; }
}
