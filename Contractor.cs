using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contractor_Management
{
    public class Contractor
    {

        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime StartDate { get; set; }
        public float HourlyWage { get; set; }
        
   

        public Contractor(string InputFirstName, string InputLastName, DateTime InputStartDate, float InputHourlyWage)
        {
            StartDate = InputStartDate;
            FirstName = InputFirstName;
            LastName = InputLastName;
            HourlyWage = InputHourlyWage;
            
        }


        public override string ToString()
        {
            return $"[${HourlyWage}] {LastName}, {FirstName}";
        }




    }
}
