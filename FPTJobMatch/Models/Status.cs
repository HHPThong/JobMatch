using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FPTJobMatch.Models
{
    public class Status
    {
        [Key]
        public int Id { get; set; }
        public string NameStatus { get; set; }
    }
}
