namespace WorkSchedule.Order.Dtos;

using System.ComponentModel.DataAnnotations;

public class JobPositionDto
{
    [Required(ErrorMessage = "O campo \"Name\" é obrigatório")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O \"Name\" deve ter entre {2} e {100} caracteres")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo \"Workload\" é obrigatório")]
    [Range(1, 24, ErrorMessage = "O \"Workload\" deve estar entre 1 e 24 horas diarias")]
    public int Workload { get; set; }

    [Required(ErrorMessage = "O campo \"MaximumConsecutiveDays\" é obrigatório")]
    [Range(1, 7, ErrorMessage = "O \"MaximumConsecutiveDays\" deve estar entre 1 e 7 dias semanais")]
    public int MaximumConsecutiveDays { get; set; }
}
