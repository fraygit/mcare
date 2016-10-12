using mcare.MongoData.Entities.Base;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mcare.MongoData.Model
{
    public class Category : MongoEntity
    {
        public string Name { get; set; }
        public List<SubCategory> SubCategories { get; set; }
        public bool IsActive { get; set; }
        public string Icon { get; set; }
    }

    public class SubCategory
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public List<SubCategory> SubCategories { get; set; }
        public bool IsActive { get; set; }
        public string Icon { get; set; }
    }
}
