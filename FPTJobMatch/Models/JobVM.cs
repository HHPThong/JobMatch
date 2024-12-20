﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FPTJobMatch.Models
{
	public class JobVM
	{
		[ValidateNever]
		public Job Job { get; set; }
		[ValidateNever]
		public IEnumerable<SelectListItem> Categories { get; set; }
		[ValidateNever]
		public ApplicationJob apply { get; set; }

	}
}
