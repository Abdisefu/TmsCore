// Models.cs
using System;

public class Student 
{ 
    public required string Id { get; init; } 
    public required string Name { get; set; } 
    public int Age { get; set; } 
    public decimal GPA { get; set; } 
}

public class Course 
{ 
    public required string Code { get; init; } 
    public required string Title { get; set; } 
    public int Capacity { get; set; } 
    public int EnrolledCount { get; set; } 
}

public record EnrollmentRecord(string StudentId, string CourseCode, DateTime EnrolledAt);