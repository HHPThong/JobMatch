using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FPTJobMatch.Models
{
    public class TimeWork
    {
        public int ID { get; set; }
        public string Type { get; set; }
    }
}
