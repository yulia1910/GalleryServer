using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gallery1.Models
{
    public class PictureModel
    {
        public string id { get; set; }
        public string author { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string url { get; set; }
        public string dounlowd_url { get; set; }
    }
}
