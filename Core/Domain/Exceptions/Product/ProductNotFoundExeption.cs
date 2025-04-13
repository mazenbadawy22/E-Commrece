using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.Product
{
    public class ProductNotFoundExeption:NotFoundException
    {
        public ProductNotFoundExeption(int id) :base($"Product With Id {id} Not Found")
        {

        }
    }
}
