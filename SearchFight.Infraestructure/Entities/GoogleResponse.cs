namespace SearchFight.Infraestructure.Entities
{
    public class GoogleResponse
    {
        public searchInformation searchInformation { get; set; }
    }

    public class searchInformation
    {
        public float searchTime { get; set; }
        public string formattedSearchTime { get; set; }
        public long totalResults { get; set; }
        public string formattedTotalResults { get; set; }
    }
}