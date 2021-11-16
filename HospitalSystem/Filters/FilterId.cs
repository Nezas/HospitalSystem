using System.Collections.Generic;
using System.Linq;
using HospitalSystem.Models;

namespace HospitalSystem.Filters
{
    public class FilterId : IFilter
    {
        private long _id;

        public FilterId(long id)
        {
            _id = id;
        }

        public IEnumerable<PatientRecord> Filter(IEnumerable<PatientRecord> patientRecords)
        {
            return patientRecords.Where(p => p.Id == _id);
        }
    }
}
