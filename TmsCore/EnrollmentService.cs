// EnrollmentService.cs
using System;

public class EnrollmentService 
{ 
    public EnrollmentRecord ProcessRegistration(Student? student, Course? course) 
    { 
        // TODO 1: Add guard clauses
        if (student is null) 
            throw new ArgumentNullException(nameof(student));
            
        if (course is null) 
            throw new ArgumentNullException(nameof(course));
            
        if (course.EnrolledCount >= course.Capacity)
            throw new InvalidOperationException("The course is full.");

        // TODO 2: Switch expression for academic standing
        string standing = student.GPA switch 
        { 
            >= 3.5m => "Honors", 
            >= 2.5m => "Good Standing", 
            _       => "Academic Warning" 
        }; 
 
        // ● A standing classification like Abeba is in Honors.
        Console.WriteLine($"● {student.Name} is in {standing}."); 
 
        // TODO 3: Return a new EnrollmentRecord
        return new EnrollmentRecord(student.Id, course.Code, DateTime.UtcNow); 
    } 
}