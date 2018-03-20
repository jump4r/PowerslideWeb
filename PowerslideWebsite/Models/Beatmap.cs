using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PowerslideWebsite.Models
{
    public class Beatmap
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int SumbissionID { get; set; }

    }
}
