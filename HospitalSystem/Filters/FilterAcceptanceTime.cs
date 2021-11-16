using System;
using System.Collections.Generic;
using System.Linq;
using HospitalSystem.Models;

namespace HospitalSystem.Filters
{
    public class FilterAcceptanceTime : IFilter
    {
        private TimeSpan _acceptanceTime;

        public FilterAcceptanceTime(TimeSpan acceptanceTime)
        {
            _acceptanceTime = acceptanceTime;
        }

        public IEnumerable<PatientRecord> Filter(IEnumerable<PatientRecord> patientRecords)
        {
            return patientRecords.Where(p => p.AcceptanceTime == _acceptanceTime);
        }
    }
}
