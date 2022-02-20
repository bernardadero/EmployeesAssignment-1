using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesAssignment.Exceptions
{
    public class InvalidSalaryException : Exception
    {
        public InvalidSalaryException()
        {

        }

        public InvalidSalaryException(string message) : base(message)
        {

        }
    }
}
