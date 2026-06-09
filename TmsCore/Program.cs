var enrollment = new EnrollmentRecord("STU-001", "CS-401", DateTime.UtcNow); 
Console.WriteLine(enrollment); 
// Try to mutate it — uncomment this line and see the compiler error: 
// enrollment.CourseCode = "HACKED";  // ERROR: init-only property 
// Non-destructive copy — creates a NEW record with one field changed 
var corrected = enrollment with { CourseCode = "CS-402" }; 
Console.WriteLine(corrected); 
// Value equality — two records with the same data are equal 
var duplicate = new EnrollmentRecord("STU-001", "CS-401", enrollment.EnrolledAt); 
Console.WriteLine($"Same data? {enrollment == duplicate}");  // True