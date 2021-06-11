using System.ComponentModel.DataAnnotations;

namespace Workshop.Service.API.Resources
{
        public record SaveCriterionResource
        {
                [Required]
                public string Description { get; set; }

                [Required]
                public int MaxPoints { get; set; }
        }
}