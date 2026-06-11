// EnrollmentService.cs
using System;

public class EnrollmentService 
{ 
    // TODO 2: Create a property that holds the delegate 'listener'
    // Using modern built-in Action delegate that takes a Student model
    public Action<Student>? OnEnrollmentCompleted { get; set; }

    public EnrollmentRecord ProcessRegistration(Student? student, Course? course) 
    { 
        if (student is null) 
            throw new ArgumentNullException(nameof(student));
            
        if (course is null) 
            throw new ArgumentNullException(nameof(course));
            
        if (course.EnrolledCount >= course.Capacity)
            throw new CapacityReachedException(course.Code);

        string standing = student.GPA switch 
        { 
            >= 3.5m => "Honors", 
            >= 2.5m => "Good Standing", 
            _       => "Academic Warning" 
        }; 
 
        Console.WriteLine($"● {student.Name} is in {standing}."); 
 
        return new EnrollmentRecord(student.Id, course.Code, DateTime.UtcNow); 
    } 

    // MODULAR AUDIT PATH EXTENSION METHOD
    public void FinalizeEnrollment(Student s) 
    { 
        Console.WriteLine("Persisting to database..."); 
 
        // TODO 3: Check if the delegate listener is 'not null' and invoke it
        // The ?. operator safely checks for null and invokes the delegate seamlessly
        OnEnrollmentCompleted?.Invoke(s); 
    } 
}