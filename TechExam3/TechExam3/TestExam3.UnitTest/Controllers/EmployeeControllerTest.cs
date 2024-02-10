using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TechExam3.Controllers;
using TechExam3.Interfaces;
using TechExam3.Model;
using TechExam3.Repository;
using TechExam3.Validator;
using Xunit;

namespace TestExam3.UnitTest.Controllers
{
    public class EmployeeControllerTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;
        private readonly Mock<IEmployeeValidator> _mockValidator;
        private readonly EmployeeController _employeeController;
        public EmployeeControllerTest()
        {
            _fixture = new Fixture();
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            _mockValidator = new Mock<IEmployeeValidator>();
            _employeeController = new EmployeeController(_mockEmployeeRepository.Object, _mockValidator.Object);
        }

        [Fact]
        public void GetEmployeeList_Should_ReturnListOfData_ValidId()
        {
            //Arrange
            int validId = 1;
            var data = _fixture.Create<IEnumerable<EmployeeModel>>();

            _mockEmployeeRepository.Setup(x => x.GetEmployee(validId)).Returns(data.ToList());

            //Act
            var response = _employeeController.GetEmployeeList(validId);

            //Assert
            Assert.NotNull(response);
            response.Equals(data);
            _mockEmployeeRepository.Verify(x => x.GetEmployee(validId), Times.Once);
        }

        [Fact]
        public void GetEmployeeList_Should_ReturnError()
        {
            //Arrange
            int validId = 1;
            _mockEmployeeRepository.Setup(x => x.GetEmployee(validId)).Throws(new Exception());

            //Act
            var response = _employeeController.GetEmployeeList(validId);

            //Assert
            var statusCodeResult = Assert.IsType<StatusCodeResult>(response);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public void GetEmployeeList_Should_ReturnEmptyArray()
        {
            //Arrange
            int invalidId = -1;
            var emptyEmployeeList = new List<EmployeeModel>();
            _mockEmployeeRepository.Setup(x => x.GetEmployee(invalidId)).Returns(emptyEmployeeList);

            //Act
            var response = _employeeController.GetEmployeeList(invalidId);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var model = Assert.IsAssignableFrom<List<EmployeeModel>>(okResult.Value);
            Assert.NotNull(model);
            Assert.Empty(model);
            Assert.Equal(emptyEmployeeList.Count, model.Count);
        }

        [Fact]
        public void CreateEmployee_ShouldBeAbleCreateNewEmployee_ReturnOkResult()
        {
            //Arrange
            var data = _fixture.Create<AddEmployeeRequest>();

            var employeeModel = new EmployeeModel()
            {
                Name = data.Name,
                Address = data.Address,
                Email = data.Email,
                PhoneNumber = data.PhoneNumber,
                Position = data.Position,
            };

            _mockValidator.Setup(x => x.ValidateRequest(data)).Returns(new ValidatorResponse() { HasError = false });

            _mockEmployeeRepository.Setup(x => x.AddEmployee(data)).Returns(employeeModel);

            //Act
            var response = _employeeController.CreateEmployee(data);

            //Assert
            var okresult = Assert.IsType<OkObjectResult>(response);
            var model = Assert.IsAssignableFrom<EmployeeModel>(okresult.Value);
            Assert.NotNull(model);
            Assert.Equal(200, okresult.StatusCode);
        }

        [Fact]
        public void CreateEmployee_ReturnError500()
        {
            //Arrange
            var data = _fixture.Create<AddEmployeeRequest>();

            _mockValidator.Setup(x => x.ValidateRequest(data)).Returns(new ValidatorResponse());

            _mockEmployeeRepository.Setup(x => x.AddEmployee(data)).Throws(new Exception());

            //Act
            var response = _employeeController.CreateEmployee(data);

            //Assert
            var statusCodeResult = Assert.IsType<StatusCodeResult>(response);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public void CreateEmployee_InvalidRequest_ReturnBadRequest()
        {
            //Arrange
            var data = _fixture.Create<AddEmployeeRequest>();
            var validation = new ValidatorResponse() { HasError = true };

            _mockValidator.Setup(x => x.ValidateRequest(data)).Returns(validation);

            //Act
            var response = _employeeController.CreateEmployee(data);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task EditEmployee_ShouldEditEmployee_ReturOkResult()
        {
            //Arrange
            var data = _fixture.Create<EditEmployeeRequest>();
            data.Id = 1;
            var employeeModel = new EmployeeModel()
            {
                Name = data.Name,
                Address = data.Address,
                Email = data.Email,
                Id = 1,
                PhoneNumber = data.PhoneNumber,
                Position = data.Position,
            };

            var emptyEmployeeList = new List<EmployeeModel>() { employeeModel};

            _mockValidator.Setup(x => x.ValidateRequest(data)).Returns(new ValidatorResponse() { HasError = false});

            _mockEmployeeRepository.Setup(x => x.GetEmployee(1)).Returns(emptyEmployeeList.ToList());
            
            _mockEmployeeRepository.Setup(x => x.EditEmployee(data)).Returns(employeeModel);

            //Act
            var response = await _employeeController.EditEmployee(data);

            //Assert
            var okresult = Assert.IsType<OkObjectResult>(response);
            var model = Assert.IsAssignableFrom<EmployeeModel>(okresult.Value);
            Assert.NotNull(model);
            Assert.Equal(200, okresult.StatusCode);
        }

        [Fact]
        public async Task EditEmployee_ReturnBadRequest()
        {
            //Arrange
            var data = _fixture.Create<EditEmployeeRequest>();
            var validation = new ValidatorResponse() { HasError = true };

            _mockValidator.Setup(x => x.ValidateRequest(data)).Returns(validation);

            //Act
            var response = await _employeeController.EditEmployee(data);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task EditEmployee_ReturnNotFound()
        {
            //Arrange
            var data = _fixture.Create<EditEmployeeRequest>();
            var validation = new ValidatorResponse() { HasError = false };
            var emptyEmployeeList = new List<EmployeeModel>();

            _mockValidator.Setup(x => x.ValidateRequest(data)).Returns(validation);

            _mockEmployeeRepository.Setup(x => x.GetEmployee(1)).Returns(emptyEmployeeList);

            //Act
            var response = await _employeeController.EditEmployee(data);

            //Assert
            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public void DeletedEmployee_ShouldDeleteEmployee_ReturOkResult()
        {
            //Arrange
            var id = 1;
            _mockEmployeeRepository.Setup(x => x.DeleteEmployee(id)).Returns(new EmployeeModel());

            //Act
            var response = _employeeController.DeleteEmployee(id);

            //Assert
            var okresult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal("Employee Deleted!", okresult.Value);
            Assert.Equal(200, okresult.StatusCode);
        }

        [Fact]
        public void DeleteEmployee_ReturnError()
        {
            //Arrange
            var id = 1;

            _mockEmployeeRepository.Setup(x => x.DeleteEmployee(id)).Throws(new Exception());

            //Act
            var response = _employeeController.DeleteEmployee(id);

            //Assert
            var statusCodeResult = Assert.IsType<StatusCodeResult>(response);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}
