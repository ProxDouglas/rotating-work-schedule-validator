using WorkSchedule.GenerationRequest.Dtos;
using WorkSchedule.GenerationRequest.Entities;

namespace WorkSchedule.GenerationRequest.Mapper;

public class WorkScheduleRequestMapper
{
   public WorkScheduleRequestDto toDto(WorkScheduleRequest entity)
   {
      return new WorkScheduleRequestDto
      {
         Employees = entity.Employees.Select(e => new EmployeeDto 
         {
            Name = e.Name,
            JobPosition = e.JobPosition?.Name ?? string.Empty,
            Unavailabilities = e.Unavailabilities?.Select(u => new UnavailabilityDto
            {
               // TODO: Map Unavailability properties to UnavailabilityDto when the class is available
            }).ToList() ?? new List<UnavailabilityDto>()
         }).ToList(),
         JobPositions = entity.JobPositions.Select(j => new JobPositionDto
         {
            Name = j.Name,
            Workload = j.Workload,
            MaximumConsecutiveDays = j.MaximumConsecutiveDays
         }).ToList(),
         OperatingSchedule = entity.OperatingSchedule.Select(o => new OperatingScheduleDto
         {
            Start = o.Start,
            End = o.End,
            DayOperating = o.DayOperating
         }).ToList(),
         WorkDay = entity.WorkDay.Select(w => new WorkDayDto
         {
            Date = w.EffectiveDate,
            DayOperating = w.DayOperating
         }).ToList()
      };
   }

   public WorkScheduleRequest toEntity(WorkScheduleRequestDto dto)
   {
      var jobPositions = dto.JobPositions.Select(j => new JobPosition
      {
         Name = j.Name,
         Workload = j.Workload,
         MaximumConsecutiveDays = j.MaximumConsecutiveDays
      }).ToList();

      return new WorkScheduleRequest
      {
         Id = Guid.NewGuid(),
         Employees = dto.Employees.Select(e => new Employee
         {
            Name = e.Name,
            JobPosition = jobPositions.FirstOrDefault(j => j.Name == e.JobPosition),
            Unavailabilities = e.Unavailabilities?.Select(u => new Unavailability
            {
               // TODO: Map UnavailabilityDto properties to Unavailability when the class is available
            }).ToList()
         }).ToList(),
         JobPositions = jobPositions,
         OperatingSchedule = dto.OperatingSchedule.Select(o => new OperatingSchedule
         {
            Start = o.Start,
            End = o.End,
            DayOperating = o.DayOperating
         }).ToList(),
         WorkDay = dto.WorkDay.Select(w => new WorkDay
         {
            EffectiveDate = w.Date,
            DayOperating = w.DayOperating
         }).ToList()
      };
   }
}