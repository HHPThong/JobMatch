using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FPTJobMatch.Models
{
    public class ApplicationJobVM
    {
        public ApplicationJob applicationjob { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Job {  get; set; }
    }
}
