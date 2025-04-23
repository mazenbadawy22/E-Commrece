using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class UserNotFoundExcption(string email):NotFoundException($"No User With Email {email} Was Found")
    {
    }
}
