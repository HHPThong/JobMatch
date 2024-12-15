using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FPTJobMatch.Models
{
    public class Job
    {
        [Key]
        public int ID { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string requiredQualification { get; set; }
		public DateTime Deadline { get; set; }

		[ValidateNever]
		public int CategoryId { get; set; }
		[ForeignKey("CategoryId")]

		[ValidateNever]
		public Category Category { get; set; }

		[ValidateNever]
		public string? UserId { get; set; }
		[ForeignKey(nameof(UserId))]

		[ValidateNever]
		public ApplicationUser ApplicationUser { get; set; }
        public double Salary { get; set; }
        public string Request {  get; set; }
        
    }
}
