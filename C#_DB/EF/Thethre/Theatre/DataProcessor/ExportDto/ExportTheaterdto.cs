using System;
using System.Collections.Generic;
using System.Text;

namespace Theatre.DataProcessor.ExportDto
{
   public class ExportTheaterdto
    {

        //    {
        //"Name": "Capitol Theatre Building",
        //"Halls": 10,
        //"TotalIncome": 860.02,
        //"Tickets": [
        //  {
        //    "Price": 93.48,
        //    "RowNumber": 3
        //  },
        public string Name { get; set; }
        public sbyte NumberOfHalls { get; set; }
        public decimal TotalIncome { get; set; }
        public List<ExportTheatreticketDto> Tickets { get; set; }

    }
}
