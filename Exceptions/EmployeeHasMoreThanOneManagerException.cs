using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesAssignment.Exceptions
{
    public class EmployeeHasMoreThanOneManagerException : Exception
    {
        public EmployeeHasMoreThanOneManagerException()
        {
                
        }

        public EmployeeHasMoreThanOneManagerException(string message) : base(message)
        {

        }
    }
}
