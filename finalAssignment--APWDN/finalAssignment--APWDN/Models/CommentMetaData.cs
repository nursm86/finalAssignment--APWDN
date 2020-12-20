using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace finalAssignment__APWDN.Models
{
    public class CommentMetaData
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string Comment1 { get; set; }
        [JsonIgnore, XmlIgnore]
        public virtual Post Post { get; set; }
        [JsonIgnore, XmlIgnore]
        public virtual User User { get; set; }
    }
}