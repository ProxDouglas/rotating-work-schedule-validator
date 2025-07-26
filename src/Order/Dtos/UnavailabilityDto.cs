namespace WorkSchedule.Order.Dtos;

using System.ComponentModel.DataAnnotations;
public class UnavailabilityDto
{
    [Required(ErrorMessage = "O campo \"Start\" é obrigatório")]
    public DateTime Start { get; set; }

    [Required(ErrorMessage = "O campo \"End\" é obrigatório")]
    public DateTime End { get; set; }
}