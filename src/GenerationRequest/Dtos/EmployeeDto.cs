namespace WorkSchedule.GenerationRequest.Dtos;

using System.ComponentModel.DataAnnotations;

public class EmployeeDto
{
   [Required(ErrorMessage = "O campo \"Nome\" é obrigatório")]
   [StringLength(100, MinimumLength = 2, ErrorMessage = "O Nome deve ter entre {2} e {100} caracteres")]
   public string Name { get; set; } = string.Empty;

   [Required(ErrorMessage = "O campo \"JobPosition\" é obrigatório")]
   [StringLength(100, MinimumLength = 2, ErrorMessage = "O \"JobPosition\" deve ter entre {2} e {100} caracteres")]
   public string JobPosition { get; set; } = string.Empty;
   public List<UnavailabilityDto> Unavailabilities { get; set; } = new List<UnavailabilityDto>();
}