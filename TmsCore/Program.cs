// This is how the legacy system declared region — no indication it could be empty 
string region = null;  //  Compiler warning CS8600 
Console.WriteLine(region.ToUpper());  // Compiler warning CS8602 
