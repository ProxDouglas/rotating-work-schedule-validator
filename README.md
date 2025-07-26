# rotating-work-schedule-validator


## Api de Geração de escalas

**URL:** /api/workschedule

Metodo: HTTP POST

```json
{
  "Employees": [
    {
      "Name": "John Doe",
      "JobPosition": "Software Engineer",
      "Unavailabilities": [
        {
          "Start": "2025-07-09",
          "End": "2025-07-09"
        },
        {
          "Start": "2025-07-26",
          "End": "2025-07-26"
        }
      ]
    },
    {
      "Name": "Max Scott",
      "JobPosition": "Software Engineer",
      "Unavailabilities": [
        {
          "Start": "2025-07-20",
          "End": "2025-07-20"
        }
      ]
    },
    {
      "Name": "Jane Smith",
      "JobPosition": "Project Manager",
      "Unavailabilities": [
        {
          "Start": "2025-07-10",
          "End": "2025-07-10"
        },
        {
          "Start": "2025-07-27",
          "End": "2025-07-27"
        }
      ]
    }
  ],
  "JobPositions": [
    {
      "Name": "Software Engineer",
      "Workload": 8,
      "MaximumConsecutiveDays": 6
    },
    {
      "Name": "Project Manager",
      "Workload": 6,
      "MaximumConsecutiveDays": 6
    }
  ],
  "OperatingSchedule": [
    {
      "Start": "08:00:00",
      "End": "17:00:00",
      "DayOperating": 0
    },
    {
      "Start": "08:00:00",
      "End": "17:00:00",
      "DayOperating": 1
    },
    {
      "Start": "08:00:00",
      "End": "17:00:00",
      "DayOperating": 2
    },
    {
      "Start": "08:00:00",
      "End": "17:00:00",
      "DayOperating": 3
    },
    {
      "Start": "08:00:00",
      "End": "17:00:00",
      "DayOperating": 4
    },
    {
      "Start": "08:00:00",
      "End": "17:00:00",
      "DayOperating": 5
    },
    {
      "Start": "08:00:00",
      "End": "17:00:00",
      "DayOperating": 6
    },
    {
      "Start": "08:00:00",
      "End": "17:00:00",
      "DayOperating": 7
    }
  ],
  "WorkDay": [
    {
      "Date": "2025-07-01",
      "DayOperating": 0
    },
    {
      "Date": "2025-07-02",
      "DayOperating": 1
    },
    {
      "Date": "2025-07-03",
      "DayOperating": 2
    },
    {
      "Date": "2025-07-04",
      "DayOperating": 3
    },
    {
      "Date": "2025-07-05",
      "DayOperating": 4
    },
    {
      "Date": "2025-07-06",
      "DayOperating": 5
    },
    {
      "Date": "2025-07-07",
      "DayOperating": 6
    }
  ]
}

```