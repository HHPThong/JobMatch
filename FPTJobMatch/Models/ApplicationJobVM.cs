using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FPTJobMatch.Models
{
    public class ApplicationJobVM
    {
        public ApplicationJob applicationJob { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Job {  get; set; }
        public IEnumerable<SelectListItem> Status { get; set; }
    }
}
