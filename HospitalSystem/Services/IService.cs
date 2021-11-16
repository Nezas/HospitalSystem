using System.Collections.Generic;
using HospitalSystem.Models;
using HospitalSystem.Filters;

namespace HospitalSystem.Services
{
    public interface IService
    {
        void AddNewRecord(PatientRecord patientRecord);
        IEnumerable<PatientRecord> FilterPatientRecords(IFilter filter);
        void AddDiagnosis(string diagnosis);
        void DisplayPatientRecords(IEnumerable<PatientRecord> patientRecords = null);
        void DisplayCurrentPatient();
        void ClearSchedule();
        Queue<PatientRecord> GetPatientRecords();
    }
}