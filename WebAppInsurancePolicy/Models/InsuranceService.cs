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
        public IEnumerable<Policy> GetPolicyByMonth(int month)
        {
            return Context.Policies.Where(p => Context.PolicySolds.Any(ps => ps.PolicyId == p.PolicyId && ps.StartDate.Month == month)).ToList();

        }

        public List<pholder_psold> GetPoliciesByCutomerId(int PolicyHolderId)
        {
            //return Context.Policies.Where(p => Context.PolicyHolders.Any(p => p.PolicyHolderId == customerId)).ToList();
            var result = (from p in Context.PolicyHolders
                          join ps in Context.PolicySolds on p.PolicyHolderId equals
                       ps.PolicyHolderId
                          where ps.PolicyHolderId == PolicyHolderId
                          select new pholder_psold() {
                              PolicyHolderId = ps.PolicyHolderId,
                              Name = p.Name,
                              PolicyName = ps.PolicyName,
                              StartDate = ps.StartDate,
                              EndDate= ps.EndDate
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
                              PolicyHolderId = ps.PolicyHolderId,
                              Name = p.Name,
                              PolicyName = ps.PolicyName,
                              StartDate = ps.StartDate,
                              EndDate = ps.EndDate
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

        public bool UpdateCustomerDetails(int customerId, PolicyHolder updatedCustomer)
        {
            var existingCustomer = Context.PolicyHolders.FirstOrDefault(c => c.PolicyHolderId == customerId);
            if (existingCustomer == null)
            {
                return false; // Customer not found
            }// Update customer details
            existingCustomer.Name = updatedCustomer.Name;
            existingCustomer.Dob = updatedCustomer.Dob;
            existingCustomer.Gender = updatedCustomer.Gender;
            existingCustomer.Contact = updatedCustomer.Contact;
            existingCustomer.Email = updatedCustomer.Email;
            existingCustomer.Address = updatedCustomer.Address;
            Context.SaveChanges(); return true;
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
}
