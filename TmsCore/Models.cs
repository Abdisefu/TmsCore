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

// ======================================================================
// EXERCISE 7 CUSTOM EXCEPTIONS
// ======================================================================
public class TmsDatabaseException : Exception 
{ 
    public string Operation { get; } 
 
    public TmsDatabaseException(string operation, string message) 
        : base(message) 
    { 
        Operation = operation; 
    } 
 
    public TmsDatabaseException(string operation, string message, Exception innerException) 
        : base(message, innerException) 
    { 
        Operation = operation; 
    } 
} 
 
public class CapacityReachedException : InvalidOperationException 
{ 
    public string CourseCode { get; } 
 
    public CapacityReachedException(string courseCode) 
        : base($"Course {courseCode} has reached maximum capacity.") 
    { 
        CourseCode = courseCode; 
    } 
 
    public CapacityReachedException(string courseCode, Exception innerException) 
        : base($"Course {courseCode} has reached maximum capacity.", innerException) 
    { 
        CourseCode = courseCode; 
    } 
}