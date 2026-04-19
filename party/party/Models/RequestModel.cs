using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace party.Models
{
    public class RequestModel
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string ClientPhone { get; set; }
        public string ClientAdress { get; set; }
        public Nullable<int> decoreId { get; set; }
        public string date { get; set; }
        public string monsba { get; set; }

    }

}