using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechExam3.Model;

namespace TechExam3.Repository
{
    public static class StaticEmployees
    {
        public static List<EmployeeModel> ResultList = new()
        {
            new EmployeeModel
            {
               Id =1,
               Address ="Quezon City, Philippines" ,
               Email = "employee1@gmail.com",
               Name ="John Doe",
               PhoneNumber = "0911111111",
               Position = "CEO",
               IsActive = true
            },
            new EmployeeModel
            {
               Id =2,
               Address ="Quezon City, Philippines" ,
               Email = "employee2@gmail.com",
               Name ="Mary",
               PhoneNumber = "09222222222",
               Position = "Director",
               IsActive = true
            }
        };
    }
}
