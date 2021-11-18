using System.Collections.Generic;
using System.Linq;
using Spectre.Console;
using HospitalSystem.Models;
using HospitalSystem.Writers;
using HospitalSystem.Filters;

namespace HospitalSystem.Services
{
    public class ScheduleService : IService
    {
        private Queue<PatientRecord> _patientRecords;
        private readonly IFileWriter _fileWriter;

        public ScheduleService(Queue<PatientRecord> patientRecords, IFileWriter fileWriter)
        {
            _patientRecords = patientRecords;
            _fileWriter = fileWriter;
        }

        public void AddNewRecord(PatientRecord patientRecord)
        {
            _patientRecords.Enqueue(patientRecord);
        }

        public IEnumerable<PatientRecord> FilterPatientRecords(IFilter filter)
        {
            return filter.Filter(_patientRecords);
        }

        public void AddDiagnosis(string diagnosis)
        {
            _patientRecords.Peek().Diagnosis = diagnosis;
            _fileWriter.Write(_patientRecords.Peek());
            _patientRecords.Dequeue();
        }

        public void DisplayPatientRecords(IEnumerable<PatientRecord> patientRecords = null)
        {
            var table = new Table();
            table.AddColumns("Name", "Surname", "Id", "Gender", "Age", "Acceptance Time", "Symptoms");

            foreach(var patientRecord in patientRecords ?? _patientRecords)
            {
                table.AddRow(patientRecord.Name, patientRecord.Surname, patientRecord.Id.ToString(), patientRecord.Gender.ToString(), patientRecord.Age.ToString(), patientRecord.AcceptanceTime.ToString(@"hh\:mm"), patientRecord.Symptoms);
            }
            AnsiConsole.Write(table);
        }

        public void DisplayCurrentPatient()
        {
            var currentPatient = _patientRecords.Peek();
            var table = new Table();
            table.AddColumns("Feature", "Current Patient");
            table.AddRow("Name", currentPatient.Name);
            table.AddRow("Surname", currentPatient.Surname);
            table.AddRow("Id", currentPatient.Id.ToString());
            table.AddRow("Gender", currentPatient.Gender.ToString());
            table.AddRow("Age", currentPatient.Age.ToString());
            table.AddRow("Acceptance Time", currentPatient.AcceptanceTime.ToString(@"hh\:mm"));
            table.AddRow("Symptoms", currentPatient.Symptoms);
            AnsiConsole.Write(table);
        }

        public void ClearSchedule()
        {
            _patientRecords.Clear();
        }

        public Queue<PatientRecord> GetPatientRecords()
        {
            return _patientRecords;
        }
    }
}
