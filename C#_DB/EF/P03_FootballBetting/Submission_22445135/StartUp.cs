using P03_FootballBetting.Data.Models;
using System;
using System.Linq;
using System.Text;

namespace P03_FootballBetting
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var context = new FootballBettingContext();
            Console.WriteLine(GetTeamInfo(context));
        }
        public static string GetTeamInfo(FootballBettingContext context)
        {
            StringBuilder sb = new StringBuilder();


            var players = context.Players
               .Where(x => !x.IsInjured)
               .Select(p => new 
               {
                   Name=p.Name,
                   PrimaryKitColor = p.Team.PrimaryKitColor.Name,
                   SecondaryKitColor = p.Team.SecondaryKitColor.Name,
                   Position = p.Position.Name,
                   
                   Games=p.PlayerStatistics
                   .Select(c => new
                   {
                       GameDate = c.Game.DateTime,
                       HomeTeam=c.Game.HomeTeam.Name,
                       AwayTeam=c.Game.AwayTeam.Name
                   }),

               }
               )
               .OrderBy(x => x.Name)

               .ToList();

            foreach (var p in players)
            {
                sb.AppendLine($"{p.Name} - {p.Position}");
                sb.AppendLine($"{p.PrimaryKitColor} - {p.SecondaryKitColor}");
                foreach (var pl in p.Games)
                {
                    sb.AppendLine($"{pl.HomeTeam} - {pl.AwayTeam} - {pl.GameDate}");
                }

            }

            return sb.ToString().TrimEnd();
        }
    }
}
