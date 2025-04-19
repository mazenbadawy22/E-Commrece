using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OrderEntites
{
    public class ProductInOrderItem
    {
        public ProductInOrderItem()
        {

        }
        public ProductInOrderItem(int _ProductID, string _ProductName, string _PictureUrl)
        {
            ProductID = _ProductID;
            ProductName = _ProductName;
            PictureUrl = _PictureUrl;
        }

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
    }
}
