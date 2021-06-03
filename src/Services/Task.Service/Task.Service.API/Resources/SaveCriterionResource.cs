using System.ComponentModel.DataAnnotations;

namespace Task.Service.API.Resources
{
        public record SaveCriterionResource
        {
                [Required]
                public string Description { get; set; }

                [Required]
                public int MaxPoints { get; set; }
        }
}