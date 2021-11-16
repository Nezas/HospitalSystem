using System.Collections.Generic;
using HospitalSystem.Models;

namespace HospitalSystem.Filters
{
    public interface IFilter
    {
        IEnumerable<PatientRecord> Filter(IEnumerable<PatientRecord> patientRecords);
    }
}