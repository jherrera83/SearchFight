namespace SearchFight.Infraestructure.Entities
{
    public class BingResponse
    {
        public Webpages webPages { get; set; }
    }

    public class Webpages
    {
        public int totalEstimatedMatches { get; set; }
    }
}