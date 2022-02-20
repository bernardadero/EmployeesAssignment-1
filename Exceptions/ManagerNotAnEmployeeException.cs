using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesAssignment.Exceptions
{
    public class ManagerNotAnEmployeeException : Exception
    {
        public ManagerNotAnEmployeeException()
        {

        }

        public ManagerNotAnEmployeeException(string message) : base(message)
        {

        }
    }
}
