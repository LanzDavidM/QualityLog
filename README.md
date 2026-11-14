# Quality Log

A console application for recording manufacturing inspections and summarizing the results. Built with C# and .NET 10.

## Features

- **Add inspections.** Each record has a part number, date, shift, inspector, result (Pass, Fail, or Rework), and a defect type. A defect type is required for Fail and Rework and is not allowed on a Pass.
- **View all records** in an aligned table.
- **Reports:** pass rate, defect totals by type, results by shift, and the most common defect (ties are all listed).
- **Automatic saving.** Every new inspection is written to disk right away, and the data is loaded again on the next start.

## Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Developed and tested on Windows

## Run

From the solution folder:

```
dotnet run --project QualityLog
```

Or open the solution in Visual Studio and press Ctrl+F5.

Main menu:

```
1. Add inspection
2. View all records
3. Display reports
9. Exit program
```

Input rules: dates are entered as `MM/DD/YYYY` and cannot be in the future. Part numbers can be up to 15 characters, and inspector and defect type up to 20.

## Example output

View all records, with three inspections entered:

```
-----------------------------------------------------------------------------------
Part Number     Date       Shift  Inspector            Result Defect Type          
-----------------------------------------------------------------------------------
P-100           01/15/2026 First  Tester               Pass   
Prick-E-5       03/25/2026 Second Ian P. Freely        Rework Missing Stripe
Summer Air      04/30/2026 Third  John Jacob Schmidt   Fail   Cold
-----------------------------------------------------------------------------------
```

Reports for the same data:

```
Percentage of tests that passed: 33.3%

All defects totaled:
Cold: 1
Missing Stripe: 1

Results by shift:
First  1-Passed 0-Failed 0-Reworked
Second 0-Passed 0-Failed 1-Reworked
Third  0-Passed 1-Failed 0-Reworked

The most common defect(s):
Cold
Missing Stripe
```

## Where the data is stored

`%APPDATA%\QualityLog\inspections.json`, a readable JSON file:

```json
[
  {
    "PartNumber": "P-100",
    "Date": "2026-01-15",
    "Shift": "First",
    "Inspector": "Tester",
    "Result": "Pass",
    "DefectType": null
  }
]
```

If the file is damaged, or contains a record that breaks the rules above, the program shows an error and exits without changing the file. Fix or delete the file to continue.

## Tests

```
dotnet test
```

The xUnit tests cover the report calculations, record validation, the inspection log, and saving and loading, including missing, empty, damaged, and invalid files.

## Design decisions

- **Safe saving.** Data is written to a temporary file, which then replaces the real file, so a crash during a save cannot leave a half-written file.
- **Validation twice.** The same rules apply when entering a record and when loading one from disk.
- **Separation of concerns.** Storage, console display, and report calculations are separate classes. Report methods return data and never print, which is what makes them directly testable.
- **Defect types are counted case-insensitively**, so "Missing Tab" and "missing tab" are the same defect.

## Planned

An ASP.NET Core API with a SQL database, reusing the same data model.
