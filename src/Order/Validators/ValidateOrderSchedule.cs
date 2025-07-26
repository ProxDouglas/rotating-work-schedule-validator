using WorkSchedule.Order.Dtos;

namespace WorkSchedule.Order.Validators;

public class ValidateOrderSchedule
{
   public string Run(WorkScheduleOrderDto workScheduleOrderDto)
   {
      string errorMessage = "";

      // Validate employee names
      if (!this.AreEmployeeNamesUnique(workScheduleOrderDto.Employees))
         errorMessage += " Duplicate employee names found.";

      // Validate job position names
      if (!this.AreJobPositionNamesUnique(workScheduleOrderDto.JobPositions))
         errorMessage += " Duplicate job position names found.";

      // Validate operating schedule days
      if (!this.AreOperatingScheduleDaysUnique(workScheduleOrderDto.OperatingSchedule))
         errorMessage += " Duplicate operating schedule days found.";

      // Validate work day dates
      if (!this.AreWorkDayDatesUnique(workScheduleOrderDto.WorkDay))
         errorMessage += " Duplicate work day dates found.";

      return errorMessage;
   }

   public bool AreEmployeeNamesUnique(List<EmployeeDto> employees)
   {
      if (employees == null || employees.Count == 0)
         return true;

      var names = employees.Select(e => e.Name).Where(name => !string.IsNullOrWhiteSpace(name)).ToList();
      return names.Count == names.Distinct(StringComparer.OrdinalIgnoreCase).Count();
   }

   public bool AreJobPositionNamesUnique(List<JobPositionDto> jobPositions)
   {
      if (jobPositions == null || jobPositions.Count == 0)
         return true;

      var names = jobPositions.Select(jp => jp.Name).Where(name => !string.IsNullOrWhiteSpace(name)).ToList();
      return names.Count == names.Distinct(StringComparer.OrdinalIgnoreCase).Count();
   }

   public bool AreOperatingScheduleDaysUnique(List<OperatingScheduleDto> operatingSchedule)
   {
      if (operatingSchedule == null || operatingSchedule.Count == 0)
         return true;

      var days = operatingSchedule
          .Select(os => os.DayOperating)
          .Where(day => day is < 0 or > (Enums.DayOperating)7)
          .ToList();

      return days.Count == days.Distinct().Count();
   }

   public bool AreWorkDayDatesUnique(List<WorkDayDto> workDays)
   {
      if (workDays == null || workDays.Count == 0)
         return true;

      var dates = workDays.Select(wd => wd.Date).ToList();
      return dates.Count == dates.Distinct().Count();
   }
}
