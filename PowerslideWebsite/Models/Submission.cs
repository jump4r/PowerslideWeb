using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerslideWebsite.Models
{
    public class Submission
    {
        public int ID { get; set; }
        public string User { get; set; }
        public string Song { get; set; }
        public string Artist { get; set; }
        public bool Featured { get; set; }
    }
}
