using System.Collections.Generic;
using HospitalSystem.Models;

namespace HospitalSystem.Readers
{
    public interface IFileReader
    {
        Queue<PatientRecord> Read(string filePath);
    }
}