namespace Task.Service.API.Resources
{
        public record SaveCriterionResource
        {
                public string Description { get; set; }
                public int MaxPoints { get; set; }
        }
}