using System;
using System.Collections.Generic;
using System.Linq;
using Spectre.Console;
using HospitalSystem.Enums;
using HospitalSystem.Models;
using HospitalSystem.Services;
using HospitalSystem.Filters;

namespace HospitalSystem.UI
{
    public class Menu
    {
        private readonly IService _service;

        public Menu(IService service)
        {
            _service = service;
        }

        public void MainMenu()
        {
            Console.Clear();
            DisplayCalendar();
            if(_service.GetPatientRecords().Count == 0)
            {
                AnsiConsole.MarkupLine("The schedule is [red bold]empty![/]\n");
            }
            else
            {
                _service.DisplayCurrentPatient();
            }
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("[green bold]Options:[/]")
                .PageSize(7)
                .AddChoices(new[] {
                    "Add new record",
                    "Search for records",
                    "Add diagnosis",
                    "Display records",
                    "Clear schedule",
                    "Exit"
                }));
            ValidateUserChoice(choice);
            ContinueToMainMenu();
        }

        private void ValidateUserChoice(string choice)
        {
            Console.Clear();
            switch(choice)
            {
                case "Add new record":
                    {
                        var patientRecord = CreatePatientRecord();
                        _service.AddNewRecord(patientRecord);
                        break;
                    }
                case "Search for records":
                    {
                        var search = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                            .Title("Search records by:")
                            .PageSize(4)
                            .AddChoices(new[] {
                                "Name",
                                "Id",
                                "Acceptance time"
                            }));
                        ValidateSearchMethod(search);
                        break;
                    }
                case "Add diagnosis":
                    {
                        if(_service.GetPatientRecords().Count == 0)
                        {
                            AnsiConsole.MarkupLine("The schedule is [red bold]empty![/]");
                        }
                        else
                        {
                            var diagnosis = AnsiConsole.Ask<string>("Enter patient's [green]diagnosis[/]: ");
                            _service.AddDiagnosis(diagnosis);
                        }
                        break;
                    }
                case "Display records":
                    {
                        _service.DisplayPatientRecords();
                        break;
                    }
                case "Clear schedule":
                    {
                        _service.ClearSchedule();
                        AnsiConsole.MarkupLine("Schedule was [green bold]successfully[/] cleared!");
                        break;
                    }
                case "Exit":
                    {
                        AnsiConsole.WriteLine("Goodbye!");
                        Environment.Exit(0);
                        break;
                    }
                default:
                    break;
            }
        }

        private PatientRecord CreatePatientRecord()
        {
            PatientRecord patientRecord = new();
            patientRecord.Name = AnsiConsole.Ask<string>("Enter patient's [green]name[/]: ");
            patientRecord.Surname = AnsiConsole.Ask<string>("Enter patient's [green]surname[/]: ");
            patientRecord.Id = AnsiConsole.Ask<long>("Enter patient's [green]id[/]: ");
            patientRecord.Gender = AnsiConsole.Prompt(
                new TextPrompt<Gender>("Enter patient's [green]gender[/]:")
                .InvalidChoiceMessage("[red]That's not a valid gender![/]")
                .AddChoice(Gender.Male)
                .AddChoice(Gender.Female));
            patientRecord.Age = AnsiConsole.Ask<int>("Enter patient's [green]age[/]: ");
            patientRecord.AcceptanceTime = AnsiConsole.Ask<TimeSpan>("Enter [green]acceptance time[/] (e.g. 10:00):");
            patientRecord.Symptoms = AnsiConsole.Ask<string>("Enter [green]symptoms[/]:");
            return patientRecord;
        }

        private void ValidateSearchMethod(string search)
        {
            switch(search)
            {
                case "Name":
                    {
                        var name = AnsiConsole.Ask<string>("Enter patient's [green]name[/]: ");
                        var searchResult = _service.FilterPatientRecords(new FilterName(name));
                        ValidateSearchResult(searchResult);
                        break;
                    }
                case "Id":
                    {
                        var id = AnsiConsole.Ask<long>("Enter patient's [green]id[/]: ");
                        var searchResult = _service.FilterPatientRecords(new FilterId(id));
                        ValidateSearchResult(searchResult);
                        break;
                    }
                case "Acceptance time":
                    {
                        var acceptanceTime = AnsiConsole.Ask<TimeSpan>("Enter patient's [green]acceptance time[/]: ");
                        var searchResult = _service.FilterPatientRecords(new FilterAcceptanceTime(acceptanceTime));
                        ValidateSearchResult(searchResult);
                        break;
                    }
                default:
                    break;
            }
        }

        private void ContinueToMainMenu()
        {
            Console.WriteLine("\n Press any key to continue.");
            Console.ReadKey();
            MainMenu();
        }

        private void DisplayCalendar()
        {
            var calendar = new Calendar(DateTime.Now.Year, DateTime.Now.Month);
            calendar.AddCalendarEvent(DateTime.Now);
            calendar.HighlightStyle(Style.Parse("yellow bold"));
            AnsiConsole.Write(calendar);
        }

        private void ValidateSearchResult(IEnumerable<PatientRecord> searchResult)
        {
            if(searchResult.Count() == 0)
            {
                AnsiConsole.MarkupLine("\n[bold red]No patients found![/]");
            }
            else
            {
                _service.DisplayPatientRecords(searchResult);
            }
        }
    }
}
