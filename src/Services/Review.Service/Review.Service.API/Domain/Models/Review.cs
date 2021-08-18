using System;
using System.Collections.Generic;

namespace Review.Service.API.Domain.Models
{
	public class Review
	{
		public int Id { get; set; }
		public DateTimeOffset? Created { get; set; }
		public DateTimeOffset? Modified { get; set; }

		public string ReviewerId { get; set; }
		public string Reviewer { get; set; }

		public int SubmissionId { get; set; }
		public Submission Submission { get; set; }

		public ICollection<Criterion> Criteria { get; set; } = new List<Criterion>();
		public List<Grade> Grades { get; set; } = new List<Grade>();
	}
}