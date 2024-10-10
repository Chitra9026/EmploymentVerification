using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TestProject.Model;

namespace TestProject.Controllers
{
    [ApiController]
    [Route("api/")]
    public class VerifyEmploymentController : ControllerBase
    {
        public DbContext context { get; set; }
        public VerifyEmploymentController()
        {

            context = new DbContext();
            context.CreateTable();
        }
        [HttpPost]
        [Route("verify-employment")]
        public IActionResult Verify([FromBody] VerificationRequest request)
        {
            var result = context.VerifyEmployee(request.EmployeeId,request.CompanyName,request.VerificationCode);
            return Ok(new { Verified = result });
        }
        [HttpGet]
        [Route("employees")]
        public IActionResult GetEmployees()
        {
            var result = context.GetEmployees();
            return Ok(new { Employees = result });
        }
    }

    
}