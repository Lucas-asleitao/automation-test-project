# Junior Software Test Engineer Technical Test

## Introduction

This test involves a simple mock library management system. The system allows users to track which books are currently owned by the library, they can add books, remove books, or list all books currently owned by the library.

The test will focus on writing tests for the system rather than the system itself.

## System Requirements

You must have .NET 8.0 installed on your machine to run the tests.
This test is written in C# and uses the NUnit testing framework.

.NET 8.0 can be downloaded from [here](https://dotnet.microsoft.com/download/dotnet/8.0) -> We recommend downloading the latest SDK version.

You will also need an IDE to run the tests, we recommend Visual Studio.

You may need to restart your machine after installing .NET 8.0.

## Instructions

1. Clone this repository to your local machine
2. Open the solution in your preferred IDE (we recommend Visual Studio)
3. If you would like to run the application you can do so by running the LibraryManagementConsole project from the command line (change directory to the project folder and run `dotnet run`), the application will prompt you with a list of commands you can run
4. To run the tests, open the test explorer in Visual Studio and run all tests, alternatively, you can run the tests from the command line by changing directory to the LibraryManagerConsoleTests project and running `dotnet test`

## Tasks

In the LibraryManagerConsoleTests project there is a class called `LibraryManagerTests.cs`. This class contains a number of tests that are currently failing. Your tasks are to:

1. Update the failing tests so that they pass. You should not need to change the implementation of the system to make the tests pass. Tests 4 and 5 may be more complex to resolve, do not worry about having an exact correct solution, we are more interested in how you approach the problem.
2. Update test case 2 to be structured in the same Arrange/Act/Assert format as the other test cases
3. Add an appropriate description for test case 3 to describe what the test is doing

On the day of the interview, you will be asked to explain your thought process and how you approached the tasks.
We will also ask you to write some additional tests for the system.
