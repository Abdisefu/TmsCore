// Program.cs
var course = new Course { Code = "CS-401", Title = "Advanced C#", Capacity = 30 }; 
Console.WriteLine($"Course: {course.Title} (Capacity: {course.Capacity})"); 
 
// Invalid capacity — should throw 
try 
{ 
    course.Capacity = -5; 
} 
catch (ArgumentOutOfRangeException ex) 
{ 
    Console.WriteLine($"Caught: {ex.Message}"); 
} 

// Invalid title — should throw 
try 
{ 
    course.Title = ""; // Fixed: Now correctly placed inside the try block
} 
catch (ArgumentException ex) 
{ 
    Console.WriteLine($"Caught: {ex.Message}");
}