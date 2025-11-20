using Contractor_Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contractor_Management
{
    public class Job
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public float Cost { get; set; }
        public bool Completed { get; set; }

        // Contractor is optional, if no contractor is assigned, it will be an available job
        public Contractor AssignedContractor { get; set; }
        public Job(string title, float cost, DateTime date, Contractor? newContractor = null, bool completed = false)
        {
            Date = date;
            Completed = completed;
            AssignedContractor = newContractor;
            Title = title;
            Cost = cost;
        }
        public override string ToString()
        {
            return $"{(Completed ? "✔️" : "✖️")} [{(AssignedContractor != null ? AssignedContractor.FirstName : "unassigned")}] [${Cost}] {Title}";
        }
    }
}
