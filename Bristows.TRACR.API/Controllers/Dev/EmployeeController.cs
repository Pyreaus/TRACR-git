using Bristows.TRACR.Model.Models.Entities.Employees;
using Bristows.TRACR.BLL.Services.Interfaces;
using Bristows.TRACR.Model.Models.ViewModels;
using Bristows.TRACR.Model.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using AutoMapper;

namespace Bristows.TRACR.API.Controllers.Dev;
 // [Authorize(Policy="tracr-default",AuthenticationSchemes=NegotiateDefaults.AuthenticationScheme)]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[Route("api/v1/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ILogger<EmployeeController> _logger;
    private readonly IEmployeeService _employeeService;
    private static T NullArg<T>(T arg) => throw new ArgumentNullException(nameof(arg));
    public EmployeeController(IMapper mapper, ILogger<EmployeeController> logger, IEmployeeService empService)
    {
        (_employeeService, _mapper, _logger) = (empService ?? NullArg<IEmployeeService>(empService!), mapper, logger);
    }

    /// <summary>
    /// GET: api/{version}/Employee/GetEmployees
    /// </summary>
    /// <response code="200">{employee view objects}</response>
    /// <response code="404">missing employee objects</response>
    [Obsolete("Maintenance")]
    //--------------------------
    // [Authorize(Policy="tracr-admin")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(IEnumerable<EmployeeViewModel>))]
    [ActionName("GetEmployees"),HttpGet("[action]")]
    public async Task<ActionResult<IEnumerable<EmployeeViewModel>?>> GetEmployees()
    {
        IEnumerable<Employee?> employees = await _employeeService.GetEmployeesAsync();
        IEnumerable<EmployeeViewModel>  employeesVM = _mapper.Map<IEnumerable<Employee?>,IEnumerable<EmployeeViewModel>>(employees!);
        return (employees != null) && (employees.GetType() == typeof(List<Employee>)) ? Ok(employeesVM) : StatusCode(404);
    }

    /// <summary>
    /// PUT: api/{version}/Employee/EditEmployee/{id}
    /// </summary>
    /// <param name="id">Guid of employee</param>
    /// <param name="modifyReq">AddModifyEmpReq DTO</param>
    /// <response code="200">{employee view object}</response>
    /// <response code="204">invlaid id</response>
    [Obsolete("Maintenance")]
    //--------------------------
    [Consumes(MediaTypeNames.Application.Json)]
    // [Authorize(Policy="tracr-admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(EmployeeViewModel))]
    [ActionName("EditEmployee"),HttpPut("[action]/{id:guid}")]
    public async Task<ActionResult<EmployeeViewModel?>> EditEmployee([FromRoute] Guid id, [FromBody] AddModifyEmpReq modifyReq)
    {
        Employee? empEntry = await _employeeService.GetEmployeeByIdAsync(id);
        if ((empEntry is null)||(modifyReq is null)) return StatusCode(204);
        EmployeeViewModel employeeVM = _mapper.Map<Employee,EmployeeViewModel>(empEntry!);
        _mapper.Map(modifyReq, empEntry);
        this._employeeService.UpdateEmployee(empEntry);
        return Ok(employeeVM);
    }

    /// <summary>
    /// POST: api/{version}/Employee/AddEmployee
    /// </summary>
    /// <param name="employeeReq">AddModifyEmpReq DTO</param>
    /// <response code="201">{ new employee object }</response>
    /// <response code="400">not created</response>
    [Obsolete("Maintenance")]
    //--------------------------
    [Consumes(MediaTypeNames.Application.Json)]
    // [Authorize(Policy="tracr-admin")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created,Type=typeof(EmployeeViewModel))]
    [ActionName("AddEmployee"),HttpPost("[action]")]
    public ActionResult<EmployeeViewModel?> AddEmployee([FromBody] AddModifyEmpReq employeeReq)
    {
        if (employeeReq is null) return BadRequest(employeeReq);
        Employee createdEmployee = _employeeService.CreateEmployee(_mapper.Map<AddModifyEmpReq,Employee>(employeeReq));
        EmployeeViewModel employeeVM = _mapper.Map<Employee,EmployeeViewModel>(createdEmployee);
        return CreatedAtAction(nameof(GetEmployee), new { id = createdEmployee.Id }, employeeVM);
    }

    /// <summary>
    /// DELETE: api/{version}/Employee/DeleteEmployee/{id}
    /// </summary>
    /// <param name="id">Guid of employee</param>
    /// <response code="204">invlaid id</response>
    /// <response code="200">object deleted</response>
    [Obsolete("Maintenance")]
    //--------------------------
    // [Authorize(Policy="tracr-admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ActionName("DeleteEmployee"),HttpDelete("[action]/{id:guid}")]
    public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
    {
        if (await _employeeService.GetEmployeeByIdAsync(id) is null) return StatusCode(204);
        Employee? empToDelete = await _employeeService.GetEmployeeByIdAsync(id);
        _employeeService.DeleteEmployee(empToDelete!);
        return Ok(200);
    }

    /// <summary>
    /// GET: api/{version}/Employee/GetEmployee/{id}
    /// </summary>
    /// <param name="id">Guid of employee</param>
    /// <response code="200">{employee view object}</response>
    /// <response code="204">invlaid id</response>
    [Obsolete("Maintenance")]
    //--------------------------
    // [Authorize(Policy="tracr-admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(EmployeeViewModel))]
    [ActionName("GetEmployee"),HttpGet("[action]/{id:guid}")]
    public async Task<ActionResult<EmployeeViewModel?>> GetEmployee([FromRoute] Guid id)
    {
        Employee? employee = await _employeeService.GetEmployeeByIdAsync(id);
        EmployeeViewModel employeeVM = _mapper.Map<Employee,EmployeeViewModel>(employee!);
        return (employee != null) && (employee.GetType() == typeof(Employee)) ? Ok(employeeVM) : StatusCode(204);
    }
}       //[..]