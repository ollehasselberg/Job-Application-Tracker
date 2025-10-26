namespace JobTracker
{
    internal class Program
    {
        //Vi börjar med att lägga in enumeration som visar olika status på jobbansökan
        public enum ApplicationStatus
        {
            Applied,
            Interview,
            Offer,
            Rejected
        }

        static void Main(string[] args)
        {
            //Vi kallar på JobManager klassen och skapar en instans av den

            JobManager manager = new JobManager();

            //Här kör vi en bool som styr om while-loopen ska fortsätta eller inte. (running = true)

            bool running = true;

            while (running)
            {
                Console.WriteLine("\n=== Job Application Tracker ===");
                Console.WriteLine("1. Lägg till ny ansökan");
                Console.WriteLine("2. Visa alla ansökningar");
                Console.WriteLine("3. Uppdatera status");
                Console.WriteLine("4. Visa filtrerade/sorterade statistik");
                Console.WriteLine("5. Avsluta");
                Console.Write("Välj ett alternativ: ");

                string choice = Console.ReadLine();

                //Nu skapar vi vår meny med hjälp av en switch-sats och olika alternativ.

                switch (choice)
                {
                    case "1":
                        AddJob(manager); 
                        break;
                    case "2":
                        manager.ShowAll(); 
                        break;
                    case "3":
                        UpdateStatus(manager); 
                        break;
                    case "4":
                        ShowStatistics(manager); 
                        break;
                    case "5":
                        running = false;
                        Console.WriteLine("Avslutar programmet...");
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val, försök igen.");
                        break;
                }
            }
        }

        //Nu är det dags att skapa metoderna för att göra en ny jobbansökan, där programmet ber om input från användaren

        static void AddJob(JobManager manager)
        {
            Console.Write("Företagsnamn: ");
            string company = Console.ReadLine();

            Console.Write("Position: ");
            string position = Console.ReadLine();

            Console.Write("Önskad lön (kr): ");

            //Användaren skriver in en siffra i programmet, och vi behöver konvertera lönen från string till int. (text till siffra)

            if (!int.TryParse(Console.ReadLine(), out int salary))
            {
                Console.WriteLine("Ogiltigt löneformat.");
                return;
            }

            JobApplication job = new JobApplication(company, position, ApplicationStatus.Applied, DateTime.Now, salary);
            manager.AddJob(job);
        }

        //Metoder för uppdatera status i Job Manager

        static void UpdateStatus(JobManager manager)
        {
            Console.Write("Företagsnamn: ");
            string company = Console.ReadLine();

            Console.Write("Position: ");
            string position = Console.ReadLine();

            Console.WriteLine("Välj ny status:");

            //Nu vill vi hämta alla värden från vår enumerationslista längst upp i Program, för att sedan returnera en lista med aktiv status (applied, interview, offer, rejected)

            foreach (var status in Enum.GetValues(typeof(ApplicationStatus)))
            {
                Console.WriteLine($"{(int)status} - {status}");
            }

            if (!int.TryParse(Console.ReadLine(), out int statusChoice) || !Enum.IsDefined(typeof(ApplicationStatus), statusChoice))
            {
                Console.WriteLine("Ogiltigt val.");
                return;
            }

            manager.UpdateStatus(company, position, (ApplicationStatus)statusChoice);
        }

        
        static void ShowStatistics(JobManager manager)
        {

            if (manager.Applications.Count == 0) //Om inga ansökningar finns aktiva visas nedanstående meddelande
            {
                Console.WriteLine("Inga ansökningar finns.");
                return;
            }

            //---LINQ-OPERATIONER---//

            // 1. Filtrera: alla med status Applied
            var appliedJobs = manager.Applications.Where(j => j.Status == ApplicationStatus.Applied).ToList();
            Console.WriteLine($"\nAnsökningar med status 'Applied': {appliedJobs.Count}");
            foreach (var job in appliedJobs)
                Console.WriteLine(job.GetSummary());

            // 2. Sortera: nyaste ansökningarna först
            var sortedJobs = manager.Applications.OrderByDescending(j => j.ApplicationDate).ToList(); //Sortera ansökta jobb i fallande ordning (descending)
            Console.WriteLine("\nAlla ansökningar sorterade efter ansökningsdatum (nyast först):");
            foreach (var job in sortedJobs)
                Console.WriteLine(job.GetSummary());

            // 3. Beräkna: genomsnittlig lön
            var avgSalary = manager.Applications.Average(j => j.SalaryExpectation);  //Räknar ut medelvärdet av salary expectations och sorterar därefter (average)
            Console.WriteLine($"\nGenomsnittlig önskad lön: {avgSalary:F0} kr");
        }
    }
}
        
       

