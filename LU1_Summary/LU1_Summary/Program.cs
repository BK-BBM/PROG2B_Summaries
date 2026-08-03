using System.Diagnostics;
using static LU1_Summary.CustomTypesLO4;
/*^ The reason everything in CustomTypesLO4.cs is public. 
 For access in this class since they're being accessed via a method*/

namespace LU1_Summary
{

    class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("THEME 1: OBJECT LIFETIME");
                Console.WriteLine("  1) LO1: Garbage Collection Mechanisms");
                Console.WriteLine("  2) LO2: Disposable Objects & Cleanup");
                Console.WriteLine("THEME 2: ADVANCED C# FEATURES");
                Console.WriteLine("  3) LO3: Operator Overloading");
                Console.WriteLine("  4) LO4: Custom Types (Records & Structural Logic)");
                Console.WriteLine("  5) LO5: Extension Methods");
                Console.WriteLine("  6) LO6: Anonymous Types");
                Console.WriteLine("THEME 3: LANGUAGE INTEGRATED QUERIES (LINQ)");
                Console.WriteLine("  7) LO7: Data Operations via LINQ Syntax");
                Console.WriteLine("  8) LO8: Reshaping State with Anonymous Types & LINQ");
                Console.WriteLine("THEME 4: PROCESSES & CONTEXTS");
                Console.WriteLine("  9) LO9: Windows Process Control");
                Console.WriteLine("THEME 5: CONCURRENCY ARCHITECTURES");
                Console.WriteLine("  10) LO10: Multithreading & Thread Synchronization");
                Console.WriteLine("  11) LO11: Parallel Execution");
                Console.WriteLine("  12) LO11: Async Execution");
                Console.WriteLine("0) Exit Application");
                Console.Write("Select an option to run: ");

                string choice = Console.ReadLine();
                Console.Clear();

                try
                {
                    switch (choice)
                    {
                        case "1": RunLO1(); break;
                        case "2": RunLO2(); break;
                        case "3": RunLO3(); break;
                        case "4": RunLO4(); break;
                        case "5": RunLO5(); break;
                        case "6": RunLO6(); break;
                        case "7": RunLO7(); break;
                        case "8": RunLO8(); break;
                        case "9": RunLO9(); break;
                        case "10": await RunLO10(); break;
                        case "11":RunParallelDemo();  ; break;
                        case "12": await RunAsyncDemo() ; break; 
                        case "0": return;
                        default: Console.WriteLine("Invalid selection. Try again."); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n[Runtime Exception Raised]: {ex.Message}");
                    Console.ResetColor();
                }

                Console.WriteLine("\nPress any key to return to the master menu...");
                Console.ReadKey();
            }
        }

        #region Theme 1: Object Lifetime (LO1, LO2)
        static void RunLO1()
        {
            Console.WriteLine("--- LO1: Garbage Collection Mechanisms ---");
            object shortLivedObj = new object();
            Console.WriteLine($"[GC Status] Newly created object is in Generation: {GC.GetGeneration(shortLivedObj)}");

            Console.WriteLine("[GC Status] Triggering forced collection run for Generation 0...");
            GC.Collect(0, GCCollectionMode.Forced);

            Console.WriteLine($"[GC Status] Post-Collection object has been promoted to Generation: {GC.GetGeneration(shortLivedObj)}");
        }

        static void RunLO2()
        {
            using (ResourceManager rm = new ResourceManager("C:\\Users\\tamolefe\\Downloads\\Test.txt"))
            {
                rm.WritetoFile("This will write a message then get disposed");
            }
        }
        #endregion

        #region Theme 2: Advanced C# Features (LO3 - LO6)
        static void RunLO3()
        {
            Console.WriteLine("LO3: Operator Overloading easy");
            /* Additional Material 
            https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/operator-overloading?redirectedfrom=MSDN#overloadable-operators 
            
            + The Fraction Exercise
             */
            Points pointsA = new Points(1, 3);
            Points pointsB = new Points(4, 7);

            Points pointsC = pointsA + pointsB;

            /* X values = 1 & 4, Y values = 3 & 7. 
             * expected output = 5,10
             * 
             */

            pointsC.display();
        }

        static void RunLO4()
        {
            Console.WriteLine("LO4: Custom Types");
            var game = new Game
            {
                gameId = 2026,

                level = GameLevel.Medium,
                gamePrice = new GamePrice { Amount = 1000.00m }

            };

            Console.WriteLine($"Game: {game.gameId} | Game Difficulty: " +
                $"{game.level} | Cost: {game.gamePrice.Amount}");
        }

        static void RunLO5()
        {
            Console.WriteLine("LO5: Extension Methods");

            string sentence = "The quick brown fox jumsp over the lazy dog.";
            int words = sentence.WordCount();
            Console.WriteLine(words + " Words found!");

            Console.WriteLine("\n");
            Console.WriteLine("Extension Methods for collections");
            var items = new List<string> { "Eleven", null, null, null, null, "Twelve", null, "Thirteen", null, "Fourteen" };

            foreach (var item in items)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();

            var itemsCleaned = items.WhereNotNull();

            foreach (var item in itemsCleaned)
            {
                Console.Write(item + " ");
            }
        }

        static void RunLO6()
        {
            Console.WriteLine("LO6: Anonymous Types");
            /* Anonymous types -> unnamed class the compiler 
                * generates for us. Great for storing temporary data
                */

            var student = new { Name = "Shreha", Age = 22 };

            Console.WriteLine(student.Name);
        }
        #endregion

        #region Theme 3: Language Integrated Queries (LO7, LO8)
        static void RunLO7()
        {
            Console.WriteLine("LO7:LINQ");

            //LINQ -> Language Integrated Queries 
            int[] numbers = { 1, 3, 5, 7, 9, 11, 13, 15 };

            /*get numbers greater than 5 
             * below is method logic. Works but LINQ needs to 
             * be in the form of queries. 
             * Query logic for the same thing is demo-ed
             * they both achieve the same thing, but 
             * always strive for query logic
            */
            var greater = numbers
                .Where(n => n > 5);


            //query logic 
            var query =
                from n in numbers
                where n > 5
                select n;

            foreach (int n in greater)
            {
                Console.Write(n + " ");
            }
            Console.WriteLine();
            foreach (int n in query)
            {
                Console.Write(n + " ");
            }
        }

        static void RunLO8()
        {
            Console.WriteLine("LO8: Anonymous Types with LINQ");
            var employees = new List<Employee>
            {
                new Employee{EmployeeName="Taelo", Age= 20, Salary = 10000},
                new Employee{EmployeeName="Joe", Age= 22, Salary = 12000},
                new Employee{EmployeeName="Mike", Age= 25, Salary = 15000},

            };

            var highEarner =
                from emp in employees
                where emp.Salary > 11500
                select new { emp.EmployeeName, TopEarner = emp.Salary > 12000 };
            //select emp;

            foreach (var emp in highEarner)
            {
                //Console.WriteLine(emp.EmployeeName);
                Console.WriteLine($"{emp.EmployeeName} - Top Earner: {emp.TopEarner}");
            }
        }
        #endregion

        #region Theme 4: Processes, AppDomains and Contexts (LO9)
        static void RunLO9()
        {
            Console.WriteLine("LO9: Windows Processes");
            Console.WriteLine("Attempting to open notepad...");

            Process notepad = Process.Start("notepad.exe");
            Console.WriteLine($"Notepad process online with Process ID: {notepad.Id}");

            Thread.Sleep(2000); // Allow it to remain visible to students briefly

            Console.WriteLine("Safely terminating external process");
            //notepad.Kill(); -> This doesn't work (because of Win11) 

            Process[] localByName = Process.GetProcessesByName("Notepad");

            foreach (Process p in localByName)
            {
                p.Kill();
                p.WaitForExit(); 
            }
        }
        #endregion

        #region Theme 5: Multithreaded, Parallel and Asynchronous Programming (LO10 - LO12)
        private static int _syncCounter = 0;
        private static readonly object _lockToken = new object();

        static async Task RunLO10()
        {
            Console.WriteLine("Main thread started");

            Task task1 = Task.Run(() => PerformWork("Task A", 300));
            Task task2 = Task.Run(() => PerformWork("Task B", 500));

            await Task.WhenAll(task1, task2);

            Console.WriteLine("All concurrent tasks finished");


        }

        static void PerformWork(string workerName, int delay) 
        {
            for (int i = 1; i <= 10; i++) {
                Console.WriteLine($"{workerName} executing step {i}");
                Task.Delay(delay).Wait();
            }
        }
      
        static void RunParallelDemo()
        {
            Console.WriteLine("[PARALLEL] Starting heavy CPU computations...");
            Stopwatch sw = Stopwatch.StartNew();

            // Performs 4 massive calculations simultaneously using multiple threads
            Parallel.For(1, 5, i =>
            {
                Console.WriteLine($"[Parallel] Thread {Environment.CurrentManagedThreadId} started Task #{i}");

                double result = 0;
                for (int x = 0; x < 50_000_000; x++)
                {
                    result += Math.Sqrt(x) * Math.Sin(x); // Heavy math simulation
                }

                Console.WriteLine($"[Parallel] Thread {Environment.CurrentManagedThreadId} finished Task #{i}");
            });

            sw.Stop();
            Console.WriteLine($"All CPU tasks finished in: {sw.ElapsedMilliseconds}ms");
        }
        static async Task RunAsyncDemo()
        {
            Console.WriteLine("[Async] Starting web downloads (I/O bound)...");
            Stopwatch sw = Stopwatch.StartNew();

            using HttpClient client = new HttpClient();

            // Start 3 web requests concurrently without blocking the app
            Task<string> download1 = client.GetStringAsync("https://microsoft.com");
            Task<string> download2 = client.GetStringAsync("https://github.com");
            Task<string> download3 = client.GetStringAsync("https://microsoft.com");

            Console.WriteLine($"[Async] Thread {Environment.CurrentManagedThreadId} initiated downloads and is free to do other work.");

            // Wait for all downloads to finish asynchronously
            string[] pages = await Task.WhenAll(download1, download2, download3);

            Console.WriteLine($"[Async] Downloaded {pages.Length} sites. Thread {Environment.CurrentManagedThreadId} caught the final result.");
            Console.WriteLine($"[Async] All I/O tasks finished in: {sw.ElapsedMilliseconds}ms");
        }

        #endregion
    }
}
