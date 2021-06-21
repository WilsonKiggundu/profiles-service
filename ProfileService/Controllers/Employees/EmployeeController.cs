using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProfileService.Contracts.Employee;
using ProfileService.Controllers.Common;
using ProfileService.Models.Employees;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Employees
{
    /// <summary>
    /// Employee controller
    /// </summary>
    [Route("api/employee")]
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        /// <summary>
        /// SEARCH employees
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Employee>> Get([FromQuery] SearchEmployeeRequest request)
        {
            return await _employeeService.SearchAsync(request);
        }
        
        /// <summary>
        /// GET employee by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<Employee> GetOne(Guid id)
        {
            return await _employeeService.GetByIdAsync(id);
        }

        /// <summary>
        /// CREATE a employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Employee> Create(Employee employee)
        {
            try
            {
                return await _employeeService.InsertAsync(employee);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE a employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<Employee> Update(Employee employee)
        {
            try
            {
                return await _employeeService.UpdateAsync(employee);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// DELETE employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _employeeService.DeleteAsync(id);
                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        
        // Get dashboard
        [HttpGet("dashboard")]
        public async Task<Dashboard> GetDashboard()
        {
            return await _employeeService.GetDashboardAsync();
        }
    }
}