namespace Contractor_Management;

[TestClass]
public class RecruitmentSystemTests
{
    [TestMethod]
    public void TestAddJob()
    {
        RecruitmentSystem system = new RecruitmentSystem();

        Job job = new Job("Test Job", 1, DateTime.Now);
      
        system.AddJob(job);
        
        Assert.AreEqual(system.GetJobs().Count, 1);
    }

    [TestMethod]
    public void TestAddContractor()
    {
        RecruitmentSystem system = new RecruitmentSystem();

        Contractor contractor = new Contractor("Mark", "Test", DateTime.Now, 10);

        system.AddContractor(contractor);

        Assert.AreEqual(system.GetContractors().Count, 1);
    }

    [TestMethod]
    public void TestRemoveJob()
    {
        RecruitmentSystem system = new RecruitmentSystem();

        Job job = new Job("Test Job", 1, DateTime.Now);

        system.RemoveJob(job);

        Assert.AreEqual(system.GetJobs().Count, 0);
    }

    [TestMethod]
    public void TestRemoveContractor()
    {
        RecruitmentSystem system = new RecruitmentSystem();

        Contractor contractor = new Contractor("Mark", "Test", DateTime.Now, 10);

        system.RemoveContractor(contractor);

        Assert.AreEqual(system.GetContractors().Count,0);
    }

}
