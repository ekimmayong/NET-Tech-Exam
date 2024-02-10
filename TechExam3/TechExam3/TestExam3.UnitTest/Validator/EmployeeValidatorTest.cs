using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TechExam3.Model;
using TechExam3.Validator;
using Xunit;

namespace TestExam3.UnitTest.Validator
{
    public class EmployeeValidatorTest
    {
        private readonly EmployeeValidator _validator;
        public EmployeeValidatorTest() 
        {
            _validator = new EmployeeValidator();
        }

        [Fact]
        public void TestRegexPattern_ShouldReturnTrue() 
        {
            //Arrange
            string pattern = "^[A-Z][a-z]*(\\s[A-Z][a-z]*)+$";
            string input = "John Doe";

            //Act
            // interchange pattern and input from the original code in the EmployeeValidator
            bool response = Regex.IsMatch(input, pattern);

            //Assert
            Assert.True(response);
        }


        [Fact]
        public void ValidateRequest_AddName_ShouldPassNameValidations()
        {
            //Arrange
            var data = new AddEmployeeRequest()
            {
                Name = "Test Unit",
            };

            //Act
            var response = _validator.ValidateRequest(data);

            //Assert
            Assert.False(response.HasError);
        }

        [Fact]
        public void ValidateRequest_NameDoesNotPassRegexValidation_ShouldHaveError()
        {
            //Arrange
            var data = new AddEmployeeRequest()
            {
                Name = "test unit"
            };

            //Act
            var response = _validator.ValidateRequest(data);

            //Assert
            Assert.True(response.HasError);
            Assert.Equal("Name contains invalid inputs.", response.ErrorMessage);
        }

        [Fact]
        public void ValidateRequest_NameIsNullOrEmpty_ShouldHaveError()
        {
            //Arrange
            var data = new AddEmployeeRequest();

            //Act
            var response = _validator.ValidateRequest(data);

            //Assert
            Assert.True(response.HasError);
            Assert.Equal("Name can't be null or empty!", response.ErrorMessage);
        }

        [Fact]
        public void ValidateRequest_NameContainsMorethan100Character_ShouldHaveError()
        {
            //Arrange
            var randomString = "Jovial Children Playfully Laugh Amidst Sunlit Meadows Dancing Gracefully Under Azure Skies Embracing Joyful Moments Together";

            var data = new AddEmployeeRequest()
            {

                Name = randomString
            };

            //Act
            var response = _validator.ValidateRequest(data);

            //Assert
            Assert.True(response.HasError);
            Assert.Equal("Name lenght must be less than 100.", response.ErrorMessage);
            
        }
    }
}
