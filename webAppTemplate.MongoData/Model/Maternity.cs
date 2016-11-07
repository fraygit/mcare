using mcare.MongoData.Entities.Base;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mcare.MongoData.Model
{
    public class Maternity : MongoEntity
    {
        public string Email { get; set; }
        public DateTime LastPeriod { get; set; }
        public DateTime EDD { get; set; }
        public DateTime ConceptionDate { get; set; }
        public string Status { get; set; }
        public List<PrenatalVisit> PrenatalVisits { get; set; }
        public DateTime DateRegistered { get; set; }
    }

    public class PrenatalVisit
    {
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public DateTime DateVisit { get; set; }
        public int Week { get; set; }
        public decimal Weight { get; set; }
        public string Protien { get; set; }
        public string Glucose { get; set; }
        public decimal Pulse { get; set; }
        public decimal FundalHeight { get; set; }
        public decimal FHT { get; set; }
        public string FetalPosition { get; set; }
        public string Edema { get; set; }
        public string Notes { get; set; }
    }
}
