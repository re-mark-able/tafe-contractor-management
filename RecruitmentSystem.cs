using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contractor_Management
{
    class RecruitmentSystem
    {

        private readonly List<Contractor> contractors = new List<Contractor>();
        private readonly List<Job> jobs = new List<Job>();


        public Contractor Search(string name)
        {
            return contractors.Find(x => x.FirstName.ToLower().Contains(name.ToLower()) || x.LastName.ToLower().Contains(name.ToLower()));
        }

        public Job Search(float minCost, float maxCost)
        {
            return jobs.Find(x => x.Cost >= minCost && x.Cost <= maxCost);
        }

        public void AddContractor(Contractor contractor)
        {
            contractors.Add(contractor);
        }

        public void AddJob(Job job)
        {
            jobs.Add(job);
        }

        public void RemoveContractor(Contractor contractor)
        {
            contractors.Remove(contractor);
        }

        public void RemoveJob(Job job)
        {
            jobs.Remove(job);
        }

        public List<Contractor> GetContractors()
        {
            return contractors.ToList();
        }


        public List<Contractor> GetAvailableContractors()
        {

          return contractors.FindAll(x => jobs.FindAll(j => j.Completed == false && j.AssignedContractor == x).Count < 1).ToList();

        }

        public List<Contractor> GetBusyContractors()
        {

            return contractors.FindAll(x => jobs.FindAll(j => j.Completed == false && j.AssignedContractor == x).Count >= 1).ToList();

        }


        public List<Job> GetJobs()
        {
            return jobs.ToList();
        }

        public List<Job> GetAvailableJobs()
        {
            return jobs.FindAll(x => x.Completed == false && x.AssignedContractor == null).ToList();

        }

        public List<Job> GetJobsInProgress()
        {
            return jobs.FindAll(x => x.Completed == false && x.AssignedContractor != null).ToList();

        }

        public List<Job> GetJobsByCost(float max, float min)
        {
            return jobs.FindAll(x => (max == 0 || (max > 0 && x.Cost <= max)) && x.Cost >= min).ToList();
        }
        

    }
}
