using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workshop.Service.API.Utils;

namespace Workshop.Service.API.Resources
{
	public record SaveWorkshopResource
	{
		[Required]
		public string Name { get; set; }

		[Required]
		public string Description { get; set; }

		[Required]
		public int NumberOfReviews { get; set; }

		[Required]
		public DateTimeOffset SubmissionStart { get; set; }

		[Required]
		public DateTimeOffset SubmissionEnd { get; set; }

		[Required]
		public DateTimeOffset ReviewStart { get; set; }

		[Required]
		public DateTimeOffset ReviewEnd { get; set; }

		[Required]
		public DateTimeOffset Published { get; set; }

		[Required]
		// serialize the criteria array of json objects, coming from form data, into an c# object
		[ModelBinder(BinderType = typeof(FormDataJsonBinder))]
		public IEnumerable<SaveCriterionResource> Criteria { get; set; }

		[Required]
		public IFormFile ParticipantsEmails { get; set; }

		[Required]
		public string Instructor { get; set; }

	}
}