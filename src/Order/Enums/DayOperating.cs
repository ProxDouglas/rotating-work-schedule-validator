using System.ComponentModel;

namespace WorkSchedule.Enums;

public enum DayOperating
{
   [Description("Domingo")]
   Sunday = DayOfWeek.Sunday,
   
   [Description("Segunda-feira")]
   Monday = DayOfWeek.Monday,
   
   [Description("Terça-feira")]
   Tuesday = DayOfWeek.Tuesday,
   
   [Description("Quarta-feira")]
   Wednesday = DayOfWeek.Wednesday,
   
   [Description("Quinta-feira")]
   Thursday = DayOfWeek.Thursday,
   
   [Description("Sexta-feira")]
   Friday = DayOfWeek.Friday,
   
   [Description("Sábado")]
   Saturday = DayOfWeek.Saturday,
   
   [Description("Feriado")]
   Holiday = 7,
}
