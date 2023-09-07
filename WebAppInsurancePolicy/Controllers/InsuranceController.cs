using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebAppInsurancePolicy.Models;
using WebAppInsurancePolicy.Models.Db;

namespace WebAppInsurancePolicy.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]

    public class InsuranceController : ControllerBase
    {
        public InsuranceService service;
        public InsuranceController(InsuranceService svc)
        {
            service = svc;
        }

        [HttpGet("policies")]
        public IActionResult GetPolicies()
        {
            var policies = service.GetPolicies();
            return Ok(policies);
        }

        [HttpGet("policies/{policyId}")]
        public IActionResult GetPolicyById(int policyId)
        {
            var policy = service.GetPolicyById(policyId);
            if(policy == null)
            {
                return NotFound();
            }
            return Ok(policy);

        }

        [HttpGet("policies/month/{month}")]
        public IActionResult GetPoliciesByMonth(int month)
        {
            var policies = service.GetPolicyByMonth(month);
            return Ok(policies);
        }

        [HttpGet("policies/policyholder/{policyHolderId}")]
        public  ActionResult<IEnumerable<Policy>> GetPoliciesByCustomerId(int policyHolderId)
        {
            var policies = service.GetPoliciesByCutomerId(policyHolderId);
            return Ok(policies);
        }

        [HttpGet("policies/customer/{customerName}")]
        public ActionResult<IEnumerable<Policy>> GetPoliciesByCustomerName(string customerName)
        {
            var policies = service.GetPoliciesByCutomerName(customerName);
            return Ok(policies);
        }

        //[HttpPut("policies/{policyId}")]
        //public ActionResult AddPolicy(int policyId,[FromBody] Policy updtaedPolicy)
        //{
        //    if(!service.UpdatePolicyDetails(policyId, updtaedPolicy))
        //    {
        //        return NotFound();
        //    }
        //    return NoContent();
        //}

        [HttpPost("customers")]
        public IActionResult AddCustomer(PolicyHolder newCustomer)
        {
            var result = service.AddCust(newCustomer);
            if (result == 1) return Ok();
            else
                return new StatusCodeResult(501); 

        }

        [HttpPut("customers/{customerId}")]
        public ActionResult UpdateCustomerDetails(int customerId, [FromBody] PolicyHolder updtaedCustomer)
        {
            if (!service.UpdateCustomerDetails( customerId, updtaedCustomer))
            {
                return NotFound();
            }
            return NoContent();
        }



        [HttpPost("admin/policies")]
        public IActionResult AddNewPolicy(Policy newPolicy)
        {
            var x = service.AddPolicy(newPolicy);
            if (x == 1) return Ok();
            else
                return new StatusCodeResult(501);

        }

        [HttpDelete("admin/Delete")]
        public IActionResult DeleteCustomer()
        {

        }
    }
}
