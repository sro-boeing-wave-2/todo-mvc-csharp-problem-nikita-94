using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using MongoDB.Bson;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Google_Keep.Models
{
    [Table("Notes")]
    [BsonIgnoreExtraElements]
    public class Note
    {

        [BsonId]
        //[NotMapped]
        public ObjectId id { get; set; }
        [BsonElement("title")]
        public string title { get; set; }
        [BsonElement("plain_text")]
        public string plain_text { get; set; }
        [BsonElement("isPinned")]
        public bool isPinned { get; set; }
        [BsonElement("label")]
        public List<Label> label { get; set; }
        [BsonElement("checks")]
        public List<Checklist> checks { get; set; }
    }
    [BsonIgnoreExtraElements]
    public class Checklist
    {
        [BsonId]
        //[NotMapped]
        public int id { get; set; }
        [BsonElement("checklist")]
        public string checklist { get; set; }
    }
    [BsonIgnoreExtraElements]
    public class Label
    {
        [BsonId]
        //[NotMapped]
        public int id { get; set; }
        [BsonElement("label")]
        public string label { get; set; }
    }
}
