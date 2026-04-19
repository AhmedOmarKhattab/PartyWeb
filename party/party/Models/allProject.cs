using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace party.Models
{
    public class allProject
    {
        public List<coordinator> coordinators { get; set; }
        public List<decoration> decorations { get; set; }
        public decoration decoration { get; set; }
        public Request Requests { get; set; }
        public int decorationId { get; set; }


    }
}