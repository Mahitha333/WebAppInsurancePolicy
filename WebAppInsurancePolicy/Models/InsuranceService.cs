using Microsoft.EntityFrameworkCore;
using System;
using WebAppInsurancePolicy.Models.Db;

namespace WebAppInsurancePolicy.Models
{
    public class InsuranceService
    {
        InsuranceDbContext Context;
        public InsuranceService(InsuranceDbContext Context)
        {
            this.Context = Context;
        }
        
        public IEnumerable<Policy> GetPolicies()
        {
            return Context.Policies.ToList();
        }

        public Policy GetPolicyById(int policyId)
        {
          return Context.Policies.FirstOrDefault(p => p.PolicyId == policyId)!;
        }
        public IEnumerable<pholder_psold> GetPolicyByMonth(int month)
        {
            //return Context.Policies.Where(p => Context.PolicySolds.Any
            //(ps => ps.PolicyId == p.PolicyId && ps.StartDate.Month == month)).ToList();
            var result = (from p in Context.PolicyHolders
                          join ps in Context.PolicySolds on p.PolicyHolderId equals
                       ps.PolicyHolderId
                          where ps.StartDate.HasValue && ps.StartDate.Value.Month == month

                          select new pholder_psold()
                          {
                              PolicyHolderId = (int)ps.PolicyHolderId,
                              Name = p.Name,
                              PolicyName = ps.PolicyName,
                              StartDate = (DateTime)ps.StartDate,
                              
                          }).ToList();
            return result;
        }

        public string MName(int i) {
            string m = "";
            switch (i)
            {
                case 1: m = "January"; break;
                case 2: m = "February"; break;
                case 3: m = "March"; break;
                case 4: m = "April"; break;
                case 5: m = "May"; break;
                case 6: m = "June"; break;
                case 7: m = "July"; break;
                case 8: m = "August"; break;
                case 9: m = "September"; break;
                case 10: m = "October"; break;
                case 11: m = "November"; break;
                case 12: m = "December"; break;
            }
            return m;
        }
        public List<ChartData> GetPoliciesSoldByMonth()
        {
            var policiesSoldByMonth = (from p in Context.PolicySolds group p.Id by p.StartDate.Value.Month into temp
                                       select new ChartData { label =temp.Key.ToString() , y = temp.Count() }).ToList();

            for(var i=0; i<policiesSoldByMonth.Count;i++)
            {
                policiesSoldByMonth[i].label = MName(int.Parse(policiesSoldByMonth[i].label));
            }

            //var policiesSoldByMonth = Context.PolicySolds
            //            .GroupBy(ps => ps.StartDate.Value.Month)
            //            .OrderBy(group => group.Key)
            //            .Select(group => new ChartData { label = group.Key.ToString(), x = group.Count() })
            //            .ToList();

           // var data = new int[12]; // Initialize an array to store data for each month

            //foreach (var item in policiesSoldByMonth)
            //{
            //    data[item.Month - 1] = item.TotalPoliciesSold;
            //}

            return policiesSoldByMonth;
        }
     
        public List<pholder_psold> GetPoliciesByCutomerId(int PolicyHolderId)
        {
            //return Context.Policies.Where(p => Context.PolicyHolders.Any(p => p.PolicyHolderId == customerId)).ToList();
            var result = (from p in Context.PolicyHolders
                          join ps in Context.PolicySolds on p.PolicyHolderId equals
                       ps.PolicyHolderId
                          where ps.PolicyHolderId == PolicyHolderId
                          select new pholder_psold() {
                              PolicyHolderId = (int)ps.PolicyHolderId,
                              Name = p.Name,
                              PolicyName = ps.PolicyName,
                              StartDate = (DateTime)ps.StartDate,
                              EndDate= (DateTime)ps.EndDate
                          }).ToList();
            return result;
            //return Context.PolicySolds.Include(p => p.PolicyHolder).Include(p1 => p1.Policy).Where(c => c.PolicyHolder.PolicyHolderId == customerId && c.PolicyId == c.Policy.PolicyId).ToList();
        
        }

        public List<pholder_psold> GetPoliciesByCutomerName(string Name)
        {
            var result = (from p in Context.PolicyHolders
                          join ps in Context.PolicySolds on p.PolicyHolderId equals
                       ps.PolicyHolderId
                          where p.Name == Name
                          select new pholder_psold()
                          {
                              PolicyHolderId = (int)ps.PolicyHolderId,
                              Name = p.Name,
                              PolicyName = ps.PolicyName,
                              StartDate = (DateTime)ps.StartDate,
                              EndDate = (DateTime)ps.EndDate
                          }).ToList();
            return result;
        }

        public int AddNewPolicy(Policy policy )
        {
            //var existingPolicy = Context.Policies.SingleOrDefault(p => p.PolicyId == policyId);
            //if(existingPolicy != null)
            //{
            //    return false;
            //}
            //existingPolicy.PolicyName = updatedPolicy.PolicyName;
            // existingPolicy.CoverageAmount = updatedPolicy.CoverageAmount;
            //existingPolicy.PremiumAmount = updatedPolicy.PremiumAmount;
            //existingPolicy.Where(p=>p.StartDate = updatedPolicy.StartDate);
            // existingPolicy.EndDate = updatedPolicy.EndDate;
            Context.Policies.Add(policy);
            int x = Context.SaveChanges();
            return x;
           
            //return true;



        }

        public int AddCust(PolicyHolder newCustomer)
        {
            Context.PolicyHolders.Add(newCustomer);
            int x= Context.SaveChanges();
            return x;
        }

        public bool UpdateCustomerDetails( PolicyHolder uc)
        {
            var cust = Context.PolicyHolders.SingleOrDefault(c => c.PolicyHolderId == uc.PolicyHolderId);
            if (cust != null)
            {
                cust.Contact = uc.Contact;
                cust.Email = uc.Email;
                cust.Address = uc.Address;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }// Add more methods for login and admin-only functionality

        public int AddPolicy(Policy newPolicy)
        {
            Context.Policies.Add(newPolicy);
            return Context.SaveChanges();
            
        }

    }

    public class pholder_psold
    {
        public int PolicyHolderId { get; set; }
        public string? Name { get; set; }
        public string? PolicyName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }



    }
    public class ChartData { public string? label { get; set; } 
        public int y { get; set; } }
}
