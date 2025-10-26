using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static JobTracker.Program;

namespace JobTracker
{
    internal class JobManager
    {
        public List<JobApplication> Applications { get; private set; }

        // Konstruktor
        public JobManager()
        {
            Applications = new List<JobApplication>();
        }

        // Metod: lägger till en ny ansökan
        public void AddJob(JobApplication job)
        {
            Applications.Add(job);
            Console.WriteLine($"Added application for {job.CompanyName} - {job.PositionTitle}");
        }

        // Metod: uppdaterar status på en befintlig ansökan
        public void UpdateStatus(string companyName, string positionTitle, ApplicationStatus newStatus)
        {
            var job = Applications.Find(j => j.CompanyName == companyName && j.PositionTitle == positionTitle);
            if (job != null)
            {
                job.Status = newStatus;
                Console.WriteLine($"Updated status for {companyName} - {positionTitle} to {newStatus}");
            }
            else
            {
                Console.WriteLine("Job application not found.");
            }
        }

        // Metod: visar alla ansökningar
        public void ShowAll()
        {
            if (Applications.Count == 0)
            {
                Console.WriteLine("No applications to show.");
                return;
            }

            foreach (var job in Applications)
            {
                Console.WriteLine(job.GetSummary());
            }
        }
    }
}
