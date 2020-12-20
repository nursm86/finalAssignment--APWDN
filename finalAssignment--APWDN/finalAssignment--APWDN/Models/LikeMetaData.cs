using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace finalAssignment__APWDN.Models
{
    public class LikeMetaData
    {
        public int likeId { get; set; }
        public int postId { get; set; }
        public int userId { get; set; }
        [JsonIgnore, XmlIgnore]
        public virtual Post Post { get; set; }
        [JsonIgnore, XmlIgnore]
        public virtual User User { get; set; }
    }
}