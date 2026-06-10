using System.Diagnostics; 
// Simulate 5 database calls, each taking 300ms 
// THE WRONG WAY: Blocking with Thread.Sleep 
var sw = Stopwatch.StartNew(); 
for (int i = 0; i < 5; i++) 
{ 
    Thread.Sleep(300);  // Thread is HELD for 300ms  cannot serve anyone else 
} 
Console.WriteLine($"Blocking sequential: {sw.ElapsedMilliseconds}ms"); 
 
// ASYNC BUT STILL SEQUENTIAL: Thread released, but calls are one-at-a-time 
sw.Restart(); 
for (int i = 0; i < 5; i++) 
{ 
    await Task.Delay(300);  // Thread released while waiting  but still sequential 
} 
Console.WriteLine($"Async sequential:   {sw.ElapsedMilliseconds}ms"); 
 
// THE RIGHT WAY: Async parallel  all 5 start simultaneously 
sw.Restart(); 
var tasks = Enumerable.Range(0, 5).Select(_ => Task.Delay(300)); 
await Task.WhenAll(tasks); 
Console.WriteLine($"Async parallel:     {sw.ElapsedMilliseconds}ms");