using System.Collections.Generic;

namespace JsonData {
    public class  Group {
        public Group(){Members = new List<Artist>();}
        public int Id;
        public string GroupName;
        public List<Artist> Members;

        // public List<Artist> Members { get => members; set => members = value; }
    }
}