
namespace SearchFight.Core.DTOs
{
    public class SearchEngineDto
    {
        public string KeyWord { get; set; }
        public GoogleDto GoogleDto { get; set; }
        public BingDto BingDto { get; set; }
    }
}