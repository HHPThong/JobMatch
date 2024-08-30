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
        public string Description { get; set; }
        public int JobID { get; set; }
        [ForeignKey(nameof(JobID))]
        [ValidateNever]
        public Job Job { get; set; }
        public string? CV {  get; set; }
        public int StatusID { get; set; }
        [ForeignKey(nameof(StatusID))]
        [ValidateNever]
        public Status Status { get; set; }
    }
}
