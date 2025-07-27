namespace WorkSchedule.GenerationRequest.Dtos;

using System.ComponentModel.DataAnnotations;

public class WorkScheduleRequestDto
{
    [MinLength(1, ErrorMessage = "A lista de funcionários não pode estar vazia.")]
    public List<EmployeeDto> Employees { get; set; } = new List<EmployeeDto>();

    [MinLength(1, ErrorMessage = "A lista de cargos não pode estar vazia.")]
    public List<JobPositionDto> JobPositions { get; set; } = new List<JobPositionDto>();

    [MinLength(1, ErrorMessage = "A lista de horários de operação não pode estar vazia.")]
    public List<OperatingScheduleDto> OperatingSchedule { get; set; } = new List<OperatingScheduleDto>();

    [MinLength(1, ErrorMessage = "A lista de dias de trabalho não pode estar vazia.")]
    public List<WorkDayDto> WorkDay { get; set; } = new List<WorkDayDto>();
}