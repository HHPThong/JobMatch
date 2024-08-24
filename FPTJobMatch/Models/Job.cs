using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FPTJobMatch.Models
{
    public class Job
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int TimeWorkID { get; set; }
        [ForeignKey(nameof(TimeWorkID))]
        [ValidateNever]
        public string Company { get; set; }
        public double Salary { get; set; }
        public string Description { get; set; }
        public string Request {  get; set; }
        
    }
}
