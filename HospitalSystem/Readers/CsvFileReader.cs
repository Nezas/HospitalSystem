using System;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using HospitalSystem.Models;

namespace HospitalSystem.Readers
{
    public class CsvFileReader : IFileReader
    {
        public Queue<PatientRecord> Read(string filePath)
        {
            try
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture);
                config.HasHeaderRecord = false;
                config.MissingFieldFound = null;
                using(var reader = new StreamReader(filePath))
                using(var csv = new CsvReader(reader, config))
                {
                    var patientRecords = csv.GetRecords<PatientRecord>();
                    var schedule = new Queue<PatientRecord>(patientRecords);
                    return schedule;
                }
            }
            catch(CsvHelper.TypeConversion.TypeConverterException)
            {
                Console.WriteLine($"File cannot be uploaded! Check for spelling errors in the file.");
                throw new Exception("File cannot be uploaded! Check for spelling errors in the file.");
            }
            catch(FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                throw new FileNotFoundException();
            }
        }
    }
}
