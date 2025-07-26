namespace rotating_work_schedule.Entities;

using System.ComponentModel.DataAnnotations;

public class Unavailability
{
   [Required]
   public DateTime Start { get; set; }

   [Required]
   public DateTime End { get; set; }
}
