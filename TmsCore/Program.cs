using System;
using System.Collections.Generic; // Added to support Lists
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyticsDashboard
{
    internal class Program
    {
        // STEP 3 EXECUTION: Runs inside the Main method
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== Starting TMS Parallel Data Fetcher ===\n");
            var sw = Stopwatch.StartNew(); 
 
            // Start all fetches simultaneously - students AND courses 
            string[] studentIds = ["S1", "S2", "S3", "S4", "S5"]; 
            string[] courseCodes = ["CRS-101", "CRS-201", "CRS-301"]; 
 
            // This kicks off the tasks but does NOT wait for them yet
            var studentTasks = studentIds.Select(id => FetchStudentAsync(id)); 
            var courseTasks = courseCodes.Select(code => FetchCourseAsync(code)); 
 
            // Both arrays load concurrently in the background
            Student[] students = await Task.WhenAll(studentTasks); 
            Course[] courses = await Task.WhenAll(courseCodes.Select(code => FetchCourseAsync(code))); 
 
            Console.WriteLine($"\nLoaded {students.Length} students and {courses.Length} courses in {sw.ElapsedMilliseconds}ms"); 
            
            // Print out our fetched data results
            foreach (var s in students) 
            { 
                Console.WriteLine($"  {s.Name} | GPA: {s.GPA}"); 
            } 

            // ======================================================================
            // ADDED: Exercise 6 Part B: The TMS Enrollment Engine
            // ======================================================================
            Console.WriteLine("\n=== Starting TMS Enrollment Engine ===");
            
            var enrollCourse = new Course { Code = "CRS-101", Title = "C# Mastery", Capacity = 2 }; 
            var enrollService = new EnrollmentService(); 
            var enrollments = new List<EnrollmentRecord>(); 
            var failures = new List<string>(); 
             
            sw.Restart(); 
             
            foreach (var student in students) 
            { 
                try 
                { 
                    var record = enrollService.ProcessRegistration(student, enrollCourse); 
                    enrollCourse.EnrolledCount++; 
                    enrollments.Add(record); 
                    Console.WriteLine($"  Enrolled: {student.Name}"); 
                } 
                catch (InvalidOperationException ex) 
                { 
                    failures.Add($"{student.Name}: {ex.Message}"); 
                    Console.WriteLine($"  Rejected: {student.Name}  {ex.Message}"); 
                } 
            }
        }

        // ======================================================================
        // STEP 2 METHODS: These sit outside Main, but inside the Program class
        // ======================================================================
        
        // Simulates loading a student from a database asynchronously
        static async Task<Student> FetchStudentAsync(string id) 
        { 
            Console.WriteLine($"  Fetching student {id}..."); 
            await Task.Delay(300);  // Simulate database latency 
            return new Student 
            { 
                Id = id, 
                Name = $"Student-{id}", 
                Age = 20, 
                GPA = id switch 
                { 
                    "S1" => 3.8m, 
                    "S2" => 2.4m, 
                    "S3" => 3.5m, 
                    "S4" => 1.9m, 
                    "S5" => 3.2m, 
                    _ => 2.5m 
                } 
            }; 
        } 
 
        // Simulates loading a course from a database asynchronously
        static async Task<Course> FetchCourseAsync(string code) 
        { 
            Console.WriteLine($"  Fetching course {code}..."); 
            await Task.Delay(200);  // Simulate database latency 
            return new Course 
            { 
                Code = code, 
                Title = $"Course-{code}", 
                Capacity = code switch 
                { 
                    "CRS-101" => 2, 
                    "CRS-201" => 30, 
                    "CRS-301" => 15, 
                    _ => 25 
                } 
            }; 
        } 
    }
}