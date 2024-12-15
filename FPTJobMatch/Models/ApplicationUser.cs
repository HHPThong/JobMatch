using Microsoft.AspNetCore.Identity;

namespace FPTJobMatch.Models
{
    public class ApplicationUser : IdentityUser
    {
		public string Name { get; set; }
		public string? Address { get; set; }
		public string? Introduction { get; set; }
		public string? City { get; set; }
		public bool Status { get; set; }
		public string? CV { get; set; }
	}
}
