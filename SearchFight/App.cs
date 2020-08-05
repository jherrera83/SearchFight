using SearchFight.Core.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SearchFight
{
    public class App
    {
        private readonly IEngineService _engineService;

        /// <summary>
        /// App
        /// </summary>
        /// <param name="engineService"></param>
        public App(IEngineService engineService)
        {
            _engineService = engineService;
        }

        /// <summary>
        /// Run
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task Run(string[] args)
        {
            try
            {
                //Validate if user write one search value at least
                if (args.Length == 0)
                {
                    Console.Write("Please write one search value at least.");
                    return;
                }
                if (args.Length != args.Distinct().Count())
                {
                    Console.WriteLine("keyword duplicate");
                    return;
                }
                //start searching
                var result = await _engineService.SearchFight(args);
                foreach (var item in result.EngineDtoList)
                {
                    //for every result write a linea in this console
                    var textResult = $"{item.KeyWord }: {item.GoogleDto.SearchEngine} { item.GoogleDto.Total } { item.BingDto.SearchEngine } { item.BingDto.Total }";
                    Console.WriteLine(textResult);
                }
                //write winners
                Console.WriteLine(result.GoogleWinnerDto.Result);
                Console.WriteLine(result.BingWinnerDto.Result);
                Console.WriteLine(result.Winner);
            }
            catch (Exception ex)
            {
                //in case of error, cath the message error.
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }

        private async Task<string> ValidateRepeatWord(string[] args)
        {
            var result = string.Empty;



            return await Task.FromResult(result);
        }
    }
}