using System.Text.RegularExpressions;
using TechExam3.Interfaces;
using TechExam3.Model;

namespace TechExam3.Validator
{
    public class EmployeeValidator: IEmployeeValidator
    {
        public EmployeeValidator()
        {

        }

        public ValidatorResponse ValidateRequest(AddEmployeeRequest createEmployee)
        {
            ValidatorResponse response = new ValidatorResponse();
            try
            {
                if (string.IsNullOrEmpty(createEmployee.Name))
                {
                    response.HasError = true;
                    response.ErrorMessage = "Name can't be null or empty!";
                    return response;
                }
                if (!Regex.IsMatch(createEmployee.Name, "^[A-Z][a-z]*(\\s[A-Z][a-z]*)+$"))
                {
                    response.HasError = true;
                    response.ErrorMessage = "Name contains invalid inputs.";
                    return response;
                }
                if (createEmployee.Name.Length >100)
                {
                    response.HasError = true;
                    response.ErrorMessage = "Name lenght must be less than 100.";
                    return response;
                }
                response.HasError = false;
            }
            catch (Exception e)
            {
                response.HasError = true;
                response.ErrorMessage = e.Message;
            }
            return response;
        }
        public ValidatorResponse ValidateRequest(EditEmployeeRequest editEmployeeRequest)
        {
            ValidatorResponse response = new ValidatorResponse();
            try
            {

                if (string.IsNullOrEmpty(editEmployeeRequest.Name))
                {
                    response.HasError = true;
                    response.ErrorMessage = "Name can't be null or empty!";
                    return response;
                }
                if (!Regex.IsMatch( editEmployeeRequest.Name, "^[A-Z][a-z]*(\\s[A-Z][a-z]*)+$"))
                {
                    response.HasError = true;
                    response.ErrorMessage = "Name contains invalid inputs.";
                    return response;
                }
                if (editEmployeeRequest.Name.Length > 100)
                {
                    response.HasError = true;
                    response.ErrorMessage = "Name lenght must be less than 100.";
                    return response;
                }
                response.HasError = false;
            }
            catch (Exception e)
            {
                response.HasError = true;
                response.ErrorMessage = e.Message;
            }
            return response;
        }
    }
}
