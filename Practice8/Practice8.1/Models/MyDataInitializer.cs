using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Practice8._1.Models
{
    public class MyDataInitializer : DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            base.Seed(context);

            context.Projects.AddRange(new List<Project>() { 
                new Project() { ProjectName = "SSO - Single Sign On", ProjectStartDate = new DateTime(2021,03,31), ProjectEndDate = new DateTime(2021,07,25)},
                new Project() { ProjectName = "IDS - Distribution data", ProjectStartDate = new DateTime(2021,02,03), ProjectEndDate = new DateTime(2021,05,17)},
                new Project() { ProjectName = "Iboard - Stock price board", ProjectStartDate = new DateTime(2021,08,07), ProjectEndDate = new DateTime(2021,11,21)},
                new Project() { ProjectName = "Future - Derivatives", ProjectStartDate = new DateTime(2022,03,02)},
                new Project() { ProjectName = "GIFY - Gift for you", ProjectStartDate = new DateTime(2021,09,13), ProjectEndDate = new DateTime(2021,02,18)}
            });

            context.Employees.AddRange(new List<Employee>() { 
                new Employee() { EmployeeName = "Nguyễn Thanh Tùng", EmployeeDOB = new DateTime(1993,02,23), EmployeeDepartment = "IT"},
                new Employee() { EmployeeName = "Quách Thu Dung", EmployeeDOB = new DateTime(1998,06,19), EmployeeDepartment = "BA"},
                new Employee() { EmployeeName = "Thu Huệ Dân", EmployeeDOB = new DateTime(1995,08,08), EmployeeDepartment = "QA"},
                new Employee() { EmployeeName = "Lương Đức Nam", EmployeeDOB = new DateTime(1992,11,16), EmployeeDepartment = "IT"},
                new Employee() { EmployeeName = "Tạ Đình Cường", EmployeeDOB = new DateTime(1996,03,31), EmployeeDepartment = "IT"}
            });

            context.SaveChanges();
        }
    }
}