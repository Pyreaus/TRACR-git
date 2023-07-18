using System.Linq.Expressions;
using Bristows.TRACR.Model.Models.Entities.Employees;

namespace Bristows.TRACR.BLL.Services.Interfaces
{
    public interface IEmployeeService
    {
        // public IEnumerable<Employee> GetByEmployeeNumber(int pfId);
        public IEnumerable<Employee?> GetEmployees();
        public Task<IEnumerable<Employee?>> GetEmployeesAsync();
        public Employee UpdateEmployee(Employee employee, bool commit=true);
        public Employee CreateEmployee(Employee employee, bool commit=true);

        public Task<Employee?> GetEmployeeByIdAsync(Guid id);

        public void DeleteEmployee(Employee employee, bool commit=true);
        public void DeleteEmployee(Expression<Func<Employee, bool>> predicate, bool commit=true);
    }
}