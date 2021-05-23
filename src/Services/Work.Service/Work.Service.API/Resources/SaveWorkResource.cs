using System.ComponentModel.DataAnnotations;

namespace Work.Service.API.Resources
{
        public record SaveWorkResource
        {
                [Required]
                public string Content { get; set; }
        }
}