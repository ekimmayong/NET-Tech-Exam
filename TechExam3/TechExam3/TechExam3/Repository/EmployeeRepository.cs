using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using TechExam3.Interfaces;
using TechExam3.Model;

namespace TechExam3.Repository
{
    public class EmployeeRepository: IEmployeeRepository
    {
        private readonly DatabaseContext _databaseContext;
        public EmployeeRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            InitData();
        }
        private void InitData()
        {
            if (!_databaseContext.Employee.Any())
            {
                foreach (var item in StaticEmployees.ResultList)
                {
                    _databaseContext.Employee.Add(item);
                }
                _databaseContext.SaveChanges();
            }
        }
        public List<EmployeeModel> GetEmployee(int id)
        {
            List<EmployeeModel> employees = new List<EmployeeModel>();
            if (id != 0)
            {
                employees = _databaseContext.Employee.Where(x => x.Id == id && x.IsActive == true).ToList();
            }
            else
            {
                employees = _databaseContext.Employee.Where(x => x.IsActive == true).ToList();
            }

            return employees;
        }
        public EmployeeModel EditEmployee(EditEmployeeRequest request)
        {
            var entity = _databaseContext.Employee.FirstOrDefault(item => item.Id == request.Id);
            if (entity != null)
            {
                entity.Email = request.Email;
                entity.PhoneNumber = request.PhoneNumber;
                entity.Address = request.Address;   
                entity.Name = request.Name;
                entity.Position = request.Position;
            }
            _databaseContext.SaveChanges();
            return entity;
        }
        public EmployeeModel AddEmployee(AddEmployeeRequest request)
        {
            EmployeeModel employeeModel = new EmployeeModel()
            {
                Name = request.Name,
                Address = request.Address,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Position = request.Position,
                IsActive = true
            };
            _databaseContext.Employee.Add(employeeModel);

            _databaseContext.SaveChanges();
            return employeeModel;
        }
        public EmployeeModel DeleteEmployee(int Id)
        {
            var entity = _databaseContext.Employee.FirstOrDefault(item => item.Id == Id);
            if (entity != null)
            {
                entity.IsActive= false;
            }
            _databaseContext.SaveChanges();
            return entity;
        }
    }
}
