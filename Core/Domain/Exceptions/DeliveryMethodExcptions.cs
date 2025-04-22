using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class DeliveryMethodExcptions(int id):NotFoundException($"The Delivery Method With Id {id} Not Found")
    {
    }
}
