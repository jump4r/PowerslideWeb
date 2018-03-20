using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PowerslideWebsite.Models
{
    public class FileUpload
    {
        public string User { get; set; }
        public string Artist { get; set; }
        public string Song { get; set; }

        public IFormFile BeatmapSong { get; set; }

        public IList<IFormFile> BeatmapFiles { get; set; }
    }
}
