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


        /// <summary>
        /// Get a list of all contractors who's name matches the search term
        /// </summary>
        /// <param name="name">The search string</param>
        /// <returns></returns>
        public Contractor Search(string name)
        {
            return contractors.Find(x => x.FirstName.ToLower().Contains(name.ToLower()) || x.LastName.ToLower().Contains(name.ToLower()));
        }

        /// <summary>
        /// Get a list of all jobs within the cost range
        /// </summary>
        /// <param name="minCost">The minimum cost to search</param>
        /// <param name="maxCost">The maximum cost to search</param>
        /// <returns></returns>
        public Job Search(float minCost, float maxCost)
        {
            return jobs.Find(x => x.Cost >= minCost && x.Cost <= maxCost);
        }

        /// <summary>
        /// Add a new contractor to the system
        /// </summary>
        /// <param name="contractor">The details for the new contractor</param>
        public void AddContractor(Contractor contractor)
        {
            contractors.Add(contractor);
        }

        /// <summary>
        /// Add a new job to the system
        /// </summary>
        /// <param name="job">Details for the new job</param>
        public void AddJob(Job job)
        {
            jobs.Add(job);
        }

        /// <summary>
        /// Remove a contractor from the system
        /// </summary>
        /// <param name="contractor">The contractor to remove</param>
        public void RemoveContractor(Contractor contractor)
        {
            contractors.Remove(contractor);
        }


        /// <summary>
        ///  Remove a job from the system
        /// </summary>
        /// <param name="job">The job to remove</param>
        public void RemoveJob(Job job)
        {
            jobs.Remove(job);
        }

        /// <summary>
        /// Get a list of all the contractors in the system
        /// </summary>
        /// <returns></returns>
        public List<Contractor> GetContractors()
        {
            return contractors.ToList();
        }


        /// <summary>
        /// Get a list of all contractors that are available (not assigned to any incomplete jobs)
        /// </summary>
        /// <returns></returns>
        public List<Contractor> GetAvailableContractors()
        {

          return contractors.FindAll(x => jobs.FindAll(j => j.Completed == false && j.AssignedContractor == x).Count < 1).ToList();

        }

        /// <summary>
        /// Get a list of all contractors that are busy (assigned to a job)
        /// </summary>
        /// <returns></returns>
        public List<Contractor> GetBusyContractors()
        {

            return contractors.FindAll(x => jobs.FindAll(j => j.Completed == false && j.AssignedContractor == x).Count >= 1).ToList();

        }



        /// <summary>
        ///  Get a list of all jobs in the system
        /// </summary>
        /// <returns></returns>
        public List<Job> GetJobs()
        {
            return jobs.ToList();
        }

        /// <summary>
        ///  Get a list of all jobs which are not assigned to contractors
        /// </summary>
        /// <returns></returns>
        public List<Job> GetAvailableJobs()
        {
            return jobs.FindAll(x => x.Completed == false && x.AssignedContractor == null).ToList();

        }

        
        /// <summary>
        /// Get a list of all jobs which are assigned to contractors
        /// </summary>
        /// <returns></returns>
        public List<Job> GetJobsInProgress()
        {
            return jobs.FindAll(x => x.Completed == false && x.AssignedContractor != null).ToList();

        }

        /// <summary>
        /// Get a list of all jobs based on cost range
        /// </summary>
        /// <param name="max">The maximum cost to search</param>
        /// <param name="min">The minimum cost to search</param>
        /// <returns></returns>
        public List<Job> GetJobsByCost(float max, float min)
        {
            return jobs.FindAll(x => (max == 0 || (max > 0 && x.Cost <= max)) && x.Cost >= min).ToList();
        }
        

    }
}
