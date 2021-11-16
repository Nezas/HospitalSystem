using HospitalSystem.Models;

namespace HospitalSystem.Writers
{
    public interface IFileWriter
    {
        void Write(PatientRecord patientRecord);
    }
}