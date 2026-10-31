


/*
Next: the quality-check log
This is the project that carries forward into month two. Here are the stages,
using the same checkpoint approach as before.

Model. 
An InspectionResult enum (Pass, Fail, Rework) and an Inspection record with
part number, date, shift, inspector, result, and defect type. Your PPAP and
quality experience helps here, since you know what a real inspection record 
needs.

Collect.
A console menu to add inspections to a List<Inspection> and view them. Use 
Console.ReadLine with TryParse, as in your guessing game, and DateOnly.TryParse
for dates.

Report.
LINQ summaries in methods that return values: pass rate, defects by type 
(Dictionary), results by shift, and the most common defect.

Save and load. 
JSON with System.Text.Json, plus try/catch for a missing or malformed file.
This is the stage 7 piece we deferred.

Structure. 
Separate files for the model, storage, and reporting classes, so Program.cs
stays short.

Tests. 
A small xUnit project that checks the report calculations against hand-built
data, the same idea as your known test hand.

README. 
What it does, how to run it, and example output.

Design questions to settle first
Before writing code, decide what a single inspection contains and what you’ll
want to ask of the data. For example, should defect type be required only on a 
Fail? Should shift be an enum? Answering these up front is a normal part of 
design, and you can take your time. It’s also worth sketching a few example
records on paper or in Notepad++ to see what the data looks like.

First steps

Create a Console App named QualityLog in C:\Dev.

Create the Git repository, push it to your LanzDavidM account, and make your
first commit.

Create a branch: git switch -c inspection-model.

Write the InspectionResult enum and the Inspection record in their own files,
and print one hand-built inspection.

I won’t write the code for you this time, so try the model first. Paste what
you have, or any questions about the design, when you’re ready.
*/