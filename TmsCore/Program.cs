// Program.cs
// 1. Valid Student Case
var s = new Student { Id = "S1", Name = "Abeba", Age = 20, GPA = 3.8m }; 
Console.WriteLine($"Student: {s.Name}, GPA: {s.GPA}"); 

Console.WriteLine("\n--- Testing Validations ---");

// Test Case 2: Invalid Empty Name — should throw ArgumentException
try 
{ 
    var s2 = new Student { Id = "S2", Name = "", Age = 20, GPA = 3.0m };         
} 
catch (ArgumentException ex) 
{ 
    Console.WriteLine($"Caught Name Error: {ex.Message}"); 
}

// Test Case 3: Invalid Age — should throw ArgumentOutOfRangeException
try 
{ 
    var s3 = new Student { Id = "S3", Name = "Test", Age = 12, GPA = 3.0m };   
} 
catch (ArgumentOutOfRangeException ex) 
{ 
    Console.WriteLine($"Caught Age Error: {ex.Message}"); 
}

// Test Case 4: Invalid GPA — should throw ArgumentOutOfRangeException
try 
{ 
    var s4 = new Student { Id = "S4", Name = "Test", Age = 20, GPA = 5.0m };  
} 
catch (ArgumentOutOfRangeException ex) 
{ 
    Console.WriteLine($"Caught GPA Error: {ex.Message}"); 
}