// Program.cs
using System;

var service = new EnrollmentService();

// 1. Create the exact test profiles requested
var student = new Student { Id = "S1", Name = "Abeba", Age = 20, GPA = 3.8m };
var course = new Course { Code = "CS-401", Title = "Advanced C#", Capacity = 1, EnrolledCount = 0 };

Console.WriteLine("--- Verification Test Suite ---");

// Scenario 1 & 2: Valid registration & Standing classification ("Abeba is in Honors")
try
{
    var receipt = service.ProcessRegistration(student, course);
    course.EnrolledCount++; // Track that Abeba took the 1 available spot
    Console.WriteLine($"● Enrolled: {receipt.StudentId} in {receipt.CourseCode} for a valid registration");
}
catch (Exception ex)
{
    Console.WriteLine($"Failed valid registration: {ex.Message}");
}

// Scenario 3: Guard caught when passing null student
try
{
    service.ProcessRegistration(null, course);
}
catch (ArgumentNullException)
{
    Console.WriteLine($"● Guard caught: student when passing null");
}

// Scenario 4: Business rule when the course is full
try
{
    // The course capacity is 1, and Abeba already took it in Scenario 1.
    // Trying to register another student profile now will trigger the full course rule.
    var student2 = new Student { Id = "S2", Name = "Chaltu", Age = 21, GPA = 3.2m };
    service.ProcessRegistration(student2, course);
}
catch (InvalidOperationException)
{
    Console.WriteLine($"● Business rule: Registration failed because the course is full.");
}