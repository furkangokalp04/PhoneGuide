using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PhoneGuide.Shared.Enums;

namespace PhoneGuide.Reports.Entities
{
    public class Report
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string RequestedDate { get; set; }
        public string CreatedDate { get; set; }
        public ReportState ReportState { get; set; }
    }
}
