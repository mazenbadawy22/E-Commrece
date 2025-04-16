using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class UnAuthorizedExcptions(string message="InValid Email Or Password"):Exception(message)
    {
    }
}
