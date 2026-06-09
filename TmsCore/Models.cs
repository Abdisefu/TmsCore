// Models.cs

// 1. Define the Interface Contract
public interface IGradable
{
    string Title { get; }
    decimal CalculateGrade();
}

// 2. Implement it in the Quiz Class
public class Quiz : IGradable 
{ 
    public required string Title { get; init; } 
    public required int CorrectAnswers { get; init; } 
    public required int TotalQuestions { get; init; } 
 
    public decimal CalculateGrade() 
    { 
        if (TotalQuestions == 0) return 0m; 
        return (decimal)CorrectAnswers / TotalQuestions * 100m; 
    } 
} 
 
// 3. Implement it in the LabAssignment Class
public class LabAssignment : IGradable 
{ 
    public required string Title { get; init; } 
    public required decimal FunctionalityScore { get; init; } 
    public required decimal CodeQualityScore { get; init; } 
 
    public decimal CalculateGrade() 
    { 
        // 70% functionality, 30% code quality 
        return (FunctionalityScore * 0.7m) + (CodeQualityScore * 0.3m); 
    } 
}