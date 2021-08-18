namespace Review.Service.API.Resources
{
	public record GradeResource
	{
		public int CriterionId { get; set; }
		public string Description { get; set; }
		public int MaxPoints { get; set; }
		public string Feedback { get; set; }
		public int? Points { get; set; }
	}
}