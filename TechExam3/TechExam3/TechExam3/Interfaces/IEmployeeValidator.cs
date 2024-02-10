using TechExam3.Model;

namespace TechExam3.Interfaces
{
    public interface IEmployeeValidator
    {
        ValidatorResponse ValidateRequest(AddEmployeeRequest createEmployee);
        ValidatorResponse ValidateRequest(EditEmployeeRequest editEmployeeRequest);
    }
}
