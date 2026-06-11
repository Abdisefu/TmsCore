// program.cs
using System;
using System.Collections.Generic; 
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyticsDashboard
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== Starting TMS Parallel Data Fetcher ===\n");
            var sw = Stopwatch.StartNew(); 
 
            // Start all fetches simultaneously - students AND courses 
            string[] studentIds = ["S1", "S2", "S3", "S4", "S5"]; 
            string[] courseCodes = ["CRS-101", "CRS-201", "CRS-301"]; 
 
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
            // Exercise 6 Part B: The TMS Enrollment Engine
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

            // ======================================================================
            // ADDED: EXERCISE 7B - THE ENROLLMENT REPORT INTEGRATION
            // ======================================================================
            // Stop the timer 
            sw.Stop(); 
            
            // Calculate class average GPA from loaded students 
            decimal classAverage = students.Length > 0 
                ? students.Average(s => s.GPA) 
                : 0m; 
            
            // Print the final report 
            Console.WriteLine("\n========== ENROLLMENT SUMMARY =========="); 
            Console.WriteLine($"Total students loaded:      {students.Length}"); 
            Console.WriteLine($"Successful enrollments:     {enrollments.Count}"); 
            Console.WriteLine($"Failed enrollments:         {failures.Count}"); 
            Console.WriteLine($"Class average GPA:          {classAverage:F2}"); 
            Console.WriteLine($"Total elapsed time:         {sw.ElapsedMilliseconds}ms"); 
             
            if (failures.Count > 0) 
            { 
                Console.WriteLine("\n--- Failure Details ---"); 
                foreach (var failure in failures) 
                { 
                    Console.WriteLine($"  {failure}"); 
                } 
            } 
            Console.WriteLine("========================================"); 


            // ======================================================================
            // EXERCISE 7 STEP 3: Catch Domain Exceptions
            // ======================================================================
            try 
            { 
                var overflowCourse = new Course { Code = "CRS-999", Title = "Overflow Test", Capacity = 0 }; 
                enrollService.ProcessRegistration( 
                    new Student { Id = "S99", Name = "Test", Age = 20, GPA = 3.0m }, 
                    overflowCourse 
                ); 
            } 
            catch (CapacityReachedException ex) 
            { 
                Console.WriteLine($"Domain exception caught:"); 
                Console.WriteLine($"  Course: {ex.CourseCode}"); 
                Console.WriteLine($"  Message: {ex.Message}"); 
            }
        }

        // ======================================================================
        // STEP 2 METHODS
        // ======================================================================
        static async Task<Student> FetchStudentAsync(string id) 
        { 
            Console.WriteLine($"  Fetching student {id}..."); 
            await Task.Delay(300);  
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
 
        static async Task<Course> FetchCourseAsync(string code) 
        { 
            Console.WriteLine($"  Fetching course {code}..."); 
            await Task.Delay(200);  
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