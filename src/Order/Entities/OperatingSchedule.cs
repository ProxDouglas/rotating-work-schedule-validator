using System.ComponentModel.DataAnnotations;
using WorkSchedule.Enums;

namespace WorkSchedule.Order.Entities
{
   public class OperatingSchedule
   {
      [Required] // Campo obrigatório
      public TimeSpan Start { get; set; }

      [Required] // Campo obrigatório
      public TimeSpan End { get; set; }

      [Required] // Campo obrigatório
      public DayOperating DayOperating { get; set; }
   }
}