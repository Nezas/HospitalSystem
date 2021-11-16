using System.Collections.Generic;
using System.Linq;
using HospitalSystem.Models;

namespace HospitalSystem.Filters
{
    public class FilterName : IFilter
    {
        private string _name;

        public FilterName(string name)
        {
            _name = name;
        }

        public IEnumerable<PatientRecord> Filter(IEnumerable<PatientRecord> patientRecords)
        {
            return patientRecords.Where(p => (p.Name + " " + p.Surname).StartsWith(_name));
        }
    }
}
