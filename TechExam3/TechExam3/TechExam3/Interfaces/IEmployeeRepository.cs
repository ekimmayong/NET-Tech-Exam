using TechExam3.Model;
using TechExam3.Repository;

namespace TechExam3.Interfaces
{
    public interface IEmployeeRepository
    {
        List<EmployeeModel> GetEmployee(int id);
        EmployeeModel EditEmployee(EditEmployeeRequest request);
        EmployeeModel AddEmployee(AddEmployeeRequest request);
        EmployeeModel DeleteEmployee(int Id);
    }
}
