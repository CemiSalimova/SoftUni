using System;
using System.Collections.Generic;
using System.Text;

namespace VaporStore.DataProcessor.Dto.Export
{
    public class ExportGenreGameDto
    {
        //  "Games": [
        //{
        //  "Id": 49,
        //  "Title": "Warframe",
        //  "Developer": "Digital Extremes",
        //  "Tags": "Single-player, In-App Purchases, Steam Trading Cards, Co-op, Multi-player, Partial Controller Support",
        //  "Players": 6
        //},

        public int GameId { get; set; }
        public string GameName { get; set; }
        public string Developer { get; set; }
        public string Tags { get; set; }
        public int Players { get; set; }
    }
}
