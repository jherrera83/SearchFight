using System.Collections.Generic;

namespace SearchFight.Core.DTOs
{
    public class SearchFightDto
    {
        public List<SearchEngineDto> EngineDtoList { get; set; }
        public GoogleWinnerDto GoogleWinnerDto { get;set;}
        public BingWinnerDto BingWinnerDto { get; set; }
        public string Winner { get; set; }
    }
}
