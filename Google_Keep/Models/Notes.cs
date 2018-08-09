using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Google_Keep.Models
{
    public class Note
    {
        public int id { get; set; }
        public string title { get; set; }
        public string plain_text { get; set; }
        public bool IsPinned { get; set; }
        public List<Label> labels { get; set; }
        public List<Checklist> checks { get; set; }
    }
    public class Checklist
    {
        public int id { get; set; }
        public string checklist { get; set; }
    }
    public class Label
    {
        public int id { get; set; }
        public string label { get; set; }
    }
}
