using System;
using CsvHelper.Configuration.Attributes;
using HospitalSystem.Enums;

namespace HospitalSystem.Models
{
    public class PatientRecord
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public long Id { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        [Format(@"hh\:mm")]
        public TimeSpan AcceptanceTime { get; set; }
        public string Symptoms { get; set; }
        public string Diagnosis { get; set; } = "";
    }
}
