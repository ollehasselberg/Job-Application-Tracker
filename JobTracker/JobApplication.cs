using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static JobTracker.Program;

namespace JobTracker
{
    
    internal class JobApplication
    {
        public string CompanyName { get; set; }
        public string PositionTitle { get; set; }
        public ApplicationStatus Status { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime? ResponseDate { get; set; }
        public int SalaryExpectation { get; set; }

        public JobApplication(string companyName, string positionTitle, ApplicationStatus status, DateTime applicationDate, int salaryExpectation)
        {
            CompanyName = companyName;
            PositionTitle = positionTitle;
            Status = status;
            ApplicationDate = applicationDate;
            SalaryExpectation = salaryExpectation;
            ResponseDate = null; // värdet ändras när företaget svarat på ansökan, därav "null"
        }

        public int GetDaysSinceApplied()
        {
            return (DateTime.Now - ApplicationDate).Days; //Returnerar värdet för antal dagar sedan ansökan gjordes (ApplicationDate) och idag (DateTime.Now)
        }

        // Här skapar vi en metod som sammanfattar ansökan, den visar företagsnamn, position, datum och lön.
        public string GetSummary()
        {
            string responseDateText = ResponseDate.HasValue ? ResponseDate.Value.ToShortDateString() : "No response yet";
            return $"{CompanyName} - {PositionTitle} | Status: {Status} | Applied: {ApplicationDate.ToShortDateString()} | Response: {responseDateText} | Salary Expectation: {SalaryExpectation} kr";
        }
    }
}


