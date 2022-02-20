using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesAssignment.Exceptions
{
    public class CircularReferenceException : Exception
    {
        public CircularReferenceException()
        {

        }

        public CircularReferenceException(string message) : base(message)
        {

        }
    }
}
