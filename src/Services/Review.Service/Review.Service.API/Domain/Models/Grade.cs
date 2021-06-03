namespace Review.Service.API.Domain.Models
{
        public class Grade
        {
                public int? Points { get; set; }
                public string Feedback { get; set; }

                public int ReviewId { get; set; }
                public Review Review { get; set; }

                public int CriterionId { get; set; }
                public Criterion Criterion { get; set; }
        }
}