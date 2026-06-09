// Fixed implementation — exact financial math 
decimal grantPerStudent = 1999.99m; 
decimal totalAllocation = grantPerStudent * 100_000m; 
Console.WriteLine($"Total allocated (decimal): {totalAllocation}"); 
Console.WriteLine($"Total allocated (formatted): {totalAllocation:F2}");