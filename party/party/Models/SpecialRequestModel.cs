using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace party.Models
{
    public class SpecialRequestModel
    {
        public int Id { get; set; }
        public Nullable<int> CoorId { get; set; }
        public List<coordinator> coordinatorsList { get; set; }
        public coordinator coordinatorObject { get; set; }
        public string ClientName { get; set; }
        public string ClientPhone { get; set; }
        public string ClientAdress { get; set; }
        public string monsba { get; set; }
        public string image { get; set; }
        public string des { get; set; }
        public string date { get; set; }
    }
}