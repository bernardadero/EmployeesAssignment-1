using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesAssignment.Utils
{
    public class GraphUtil
    {


        public static bool HasCircularReference(List<Employee> employees, Dictionary<string, HashSet<Employee>> adjList)
        {
            HashSet<string> visited = new HashSet<string>();
            HashSet<string> inStack = new HashSet<string>();

            foreach(var employee in employees)
            {
                if (CheckCycle(employee.EmployeeId, visited, inStack, adjList))
                    return true;
            }

            return false;
        }



        private static bool CheckCycle(string root, HashSet<string> visited, HashSet<string> inStack,
            Dictionary<string, HashSet<Employee>> adjList)
        {
            if (inStack.Contains(root))
                return true;

            if (visited.Contains(root))
                return false;

            visited.Add(root);
            inStack.Add(root);

            HashSet<Employee> employees = adjList.GetValueOrDefault(root, null);
            if (employees != null)
            {
                foreach (var employee in employees)
                {
                    if (CheckCycle(employee.EmployeeId, visited, inStack, adjList))
                        return true;
                }
            }

            inStack.Remove(root);

            return false;
        }
    }
}
