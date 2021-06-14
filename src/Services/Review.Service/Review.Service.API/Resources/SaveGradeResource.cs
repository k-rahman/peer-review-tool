using System.ComponentModel.DataAnnotations;

namespace Review.Service.API.Resources
{
        public record SaveGradeResource
        {
                [Required]
                public int ReviewId { get; set; }

                [Required]
                public int CriterionId { get; set; }

                [Required]
                public int Points { get; set; }

                [Required]
                public string Feedback { get; set; }
        }
}