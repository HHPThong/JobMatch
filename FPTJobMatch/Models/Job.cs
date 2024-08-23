using System.ComponentModel.DataAnnotations;

namespace FPTJobMatch.Models
{
    public class Job
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Description { get; set; }
        public string Request {  get; set; }
        
    }
}
