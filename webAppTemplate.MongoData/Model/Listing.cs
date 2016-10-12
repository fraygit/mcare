using mcare.MongoData.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mcare.MongoData.Model
{
    public class Listing : MongoEntity
    {
        public string Title { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public bool IsNew { get; set; }
        public bool IsFree { get; set; }
        public int ShippingType { get; set; }
        public DateTime DatePosted { get; set; }
        public DateTime DateExpiry { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public decimal StartingPrice { get; set; }
        public decimal BuyNowPrice { get; set; }
        public List<string> Tags { get; set; }
    }
}
