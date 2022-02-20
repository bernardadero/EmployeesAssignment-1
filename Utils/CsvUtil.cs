using CsvHelper;
using EmployeesAssignment.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesAssignment
{
    public class CsvUtil
    {
        public static List<Employee> GetEmployeesFromCsv(string csv)
        {
            var employees = new List<Employee>();
            byte[] csvByteArray = Encoding.ASCII.GetBytes(csv);
            MemoryStream stream = new MemoryStream(csvByteArray);
            using (var reader = new StreamReader(stream))
            using(var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                while(csvReader.Read())
                {
                    string employeeId = csvReader.GetField(0);
                    string managerId = csvReader.GetField(1);
                    long salary;
                    bool validSalary = long.TryParse(csvReader.GetField(2), out salary);
                    if(!validSalary)
                    {
                        throw new InvalidSalaryException("The salary provided is an invalid value");
                    }
                    employees.Add(new Employee()
                    {
                        EmployeeId = employeeId,
                        Salary = salary,
                        ManagerId = managerId
                    });
                }
            }

            return employees;
        }
    }
}
