using System.ComponentModel.DataAnnotations;

namespace FPTJobMatch.Models
{
    public class Status
    {
        [Key]
        public int Id { get; set; }
        public string NameStatus { get; set; }
    }
}
