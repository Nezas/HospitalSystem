using System;
using HospitalSystem.Readers;
using HospitalSystem.Services;
using HospitalSystem.UI;
using HospitalSystem.Writers;

namespace HospitalSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileReader = new CsvFileReader();
            var today = DateTime.UtcNow.ToString("yyyy-MM-dd");
            var schedule = fileReader.Read($@"..\..\..\Data\{today}.csv");
            var scheduleService = new ScheduleService(schedule, new CsvFileWriter());
            var menu = new Menu(scheduleService);
            menu.MainMenu();
        }
    }
}
