using System;
using System.IO;
using System.Globalization;
using CsvHelper;
using HospitalSystem.Models;

namespace HospitalSystem.Writers
{
    public class CsvFileWriter : IFileWriter
    {
        public void Write(PatientRecord patientRecord)
        {
            var today = DateTime.UtcNow.ToString("yyyy-MM-dd");
            using(var stream = File.Open($@"..\..\..\Data\{today}_Serviced.csv", FileMode.Append))
            using(var writer = new StreamWriter(stream))
            using(var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecord(patientRecord);
                writer.WriteLine();
            }
        }
    }
}
