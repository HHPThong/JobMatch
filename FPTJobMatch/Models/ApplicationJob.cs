using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FPTJobMatch.Models
{
    public class ApplicationJob
    {
        [Key]
        public int Id { get; set; }
        [Required]
		[ValidateNever]
		public string? Email { get; set; }
		public int JobID { get; set; }
        [ForeignKey(nameof(JobID))]
        [ValidateNever]
        public Job Job { get; set; }
		public DateTime DayApply { get; set; }
		[ValidateNever]
		public bool Status { get; set; }
	}
}
