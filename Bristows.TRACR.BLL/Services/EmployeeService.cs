using Bristows.TRACR.Model.Models.Entities.Employees;
using Bristows.TRACR.DAL.Repositories;
using Bristows.TRACR.BLL.Services.Interfaces;
using Bristows.TRACR.Model.Contexts;
using System.Linq.Expressions;
using Bristows.TRACR.DAL.Infrastructure;

namespace Bristows.TRACR.BLL.Services
{
    public sealed class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly UnitOfWork<TRACRContext> TRACRUnitOfWork;

        public EmployeeService(IEmployeeRepository employeeRepository, UnitOfWork<TRACRContext> TRACRUnitOfWork)
        {
            this.employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            this.TRACRUnitOfWork = TRACRUnitOfWork ?? throw new ArgumentNullException(nameof(TRACRUnitOfWork));
        }
        public async Task<IEnumerable<Employee?>> GetEmployeesAsync() => await employeeRepository.GetAllAsync();
        public IEnumerable<Employee?> GetEmployees() => employeeRepository.GetAll();
        public async Task<Employee?> GetEmployeeByIdAsync(Guid id) => await employeeRepository.GetByIdAsync(id);
        public Employee UpdateEmployee(Employee employee, bool commit=true)
        {
            employeeRepository.Update(employee);
            TRACRUnitOfWork.Commit();
            return employee;
        }
        public void DeleteEmployee(Employee employee, bool commit=true)
        {
            employeeRepository.Delete(employee);
            TRACRUnitOfWork.Commit();
        }
        public void DeleteEmployee(Expression<Func<Employee, bool>> predicate, bool commit=true)
        {
            employeeRepository.Delete(predicate);
            TRACRUnitOfWork.Commit();
        }
        public Employee CreateEmployee(Employee employee, bool commit=true)
        {
            employeeRepository.Add(employee);
            TRACRUnitOfWork.Commit();
            return employee;
        }
    }
}