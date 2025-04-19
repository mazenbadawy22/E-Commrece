using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class RegisterValidationExcption:Exception
    {
        public IEnumerable<string> Errors { get; set; }
        public RegisterValidationExcption(IEnumerable<string> errors):base("Resgister Validation Errors")
        {
            Errors=errors;
        }
    }
}
