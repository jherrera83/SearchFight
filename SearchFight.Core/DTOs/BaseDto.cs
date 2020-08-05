namespace SearchFight.Core.DTOs
{
    public class BaseDto
    {
        public string KeyWord { get; set; }
        public string Result { get; set; }
        public string SearchEngine { get; set; }
        public long Total { get; set; }
    }
}
