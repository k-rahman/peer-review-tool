namespace Review.Service.API.Resources
{

        public record CriterionResource
        {
                public int Id { get; set; }
                public string Description { get; set; }
                public int MaxPoints { get; set; }
        }
}