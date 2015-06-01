

namespace Hermes.Models
{
    public class Branches
    {
        public int id { set; get; }
        public Business businessId { set; get; }
        public string city { set; get; }
        public string commune { set; get; }
        public string createAt { set; get; }
        public int number { set; get; }
        public string street { set; get; }
        public string updatedAt { set; get; }
    }
}