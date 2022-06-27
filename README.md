# TODO List Application

## Purpose
This time, you will create a reusable library of classes that represents a domain model for a real TODO list application. While completing the task, you will discover how to check the functionality of the domain models with the help of unit tests and how to demonstrate it to customers by providing an application with a user interface. As an application with a user interface, a simple console application will be used. 


## Description

Create a `TODO console application` and a class library with a `TODO List domain model`.   

- Student needs to create a solution with 3 projects:
    - The class library project which contains classes that represent a domain model for the TODO List application.
    - The client console application that demonstrates the usage of the class library.
- A user of the TODO List application should be able to:
    - create multiple TODO lists
    - remove a list
    - assign TODO entries to a list
    - decide whether to hide the selected TODO list from the list view or remove it completely from the TODO list database
    - give a title, a description, and a due date to each TODO entry + other properties if a student wants to increase the available functionality
    - modify a TODO list or a TODO entry
    - set a TODO entry status to "completed" or "uncompleted"
    - change the default status of a newly created TODO entry which is "uncompleted" to "completed" if the task is finished.

- Configuration requirements:
   - make sure that all the TODO list data is stored in the database
   - use the EF Core framework or ADO.NET to save and read data from the database
   - add the console application that demonstrates the basic functionality of the TODO List application: the console application should cover all the above-mentioned use cases: from #1 to #8.

 
