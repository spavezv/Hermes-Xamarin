using System;

namespace Hermes.Models
{
    public class Court
    {
        public int id { set; get; }
        public string name { set; get; }
        public string sport { set; get; }
        public string createdAt { set; get; }
        public string updatedAt { set; get; }
        public Branches branchId { set; get; }
    }
}

