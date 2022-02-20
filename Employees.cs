using EmployeesAssignment.Exceptions;
using EmployeesAssignment.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesAssignment
{
    public class Employees
    {
        private Employee manager = null;
        private Dictionary<string, HashSet<Employee>> managerToEmployeesAdjList = new();
        private HashSet<string> employeeIds = new();
        private Dictionary<string, Employee> employeesMap = new();

        public Employees(string csv)
        {
            List<Employee> employees = CsvUtil.GetEmployeesFromCsv(csv);
            foreach (var employee in employees)
            {
                // Validate if employee already has a manager 
                if (employeeIds.Contains(employee.EmployeeId))
                    throw new EmployeeHasMoreThanOneManagerException("Invalid data. Employee has more than one manager");

                employeeIds.Add(employee.EmployeeId);
                employeesMap.Add(employee.EmployeeId, employee);

                // Validate if a manager already exists
                if (string.IsNullOrEmpty(employee.ManagerId) && manager != null)
                    throw new ManagerAlreadyExistsException("Invalid data. Manager already exists");


                // Set the manager object
                if (string.IsNullOrEmpty(employee.ManagerId))
                {
                    manager = employee;
                    continue;
                }

                HashSet<Employee> employeeSet = null;

                // Build the adjacency matrix of manager and employees
                if (managerToEmployeesAdjList.ContainsKey(employee.ManagerId))
                {
                    employeeSet = managerToEmployeesAdjList[employee.ManagerId];
                    employeeSet.Add(employee);
                    managerToEmployeesAdjList[employee.ManagerId] = employeeSet;
                }
                else
                {
                    employeeSet = new(new List<Employee>() { employee });
                    managerToEmployeesAdjList[employee.ManagerId] = employeeSet;
                }

            }

            var managerIds = managerToEmployeesAdjList.Keys;
            foreach (string managerId in managerIds)
            {
                if (!employeeIds.Contains(managerId))
                    throw new ManagerNotAnEmployeeException("Invalid data. The manager does not exist as an employee");
            }

            if (GraphUtil.HasCircularReference(employees, managerToEmployeesAdjList))
                throw new CircularReferenceException("Invalid data. There is a circular reference in the data");

        }


        public long GetSalaryBudget(string employeeId)
        {
            long budget = 0;
            Queue<Employee> queue = new();
            Employee manager = employeesMap[employeeId];
            queue.Enqueue(manager);

            while(queue.Count > 0)
            {
                Employee current = queue.Dequeue();
                budget += current.Salary;

                HashSet<Employee> employees = managerToEmployeesAdjList.GetValueOrDefault(current.EmployeeId, null);
                if (employees == null || employees.Count == 0)
                    continue;
                foreach(var employee in employees)
                {
                    queue.Enqueue(employee);
                }
            }

            return budget;
        }
    }
}
