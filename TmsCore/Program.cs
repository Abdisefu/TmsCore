// Program.cs

// Test Data — one array holds two completely different types matching the interface
IGradable[] cohortAssessments = [ 
    new Quiz { Title = "C# Basics", CorrectAnswers = 18, TotalQuestions = 20 }, 
    new LabAssignment { Title = "Registration API", FunctionalityScore = 90m, CodeQualityScore = 85m } 
]; 
 
PrintGradeReport(cohortAssessments); 

// Report Method
void PrintGradeReport(IEnumerable<IGradable> assessments) 
{ 
    Console.WriteLine("--- Grade Report ---"); 
    foreach (var item in assessments) 
    { 
        Console.WriteLine($"{item.Title}: {item.CalculateGrade():F2}%"); 
    } 
}