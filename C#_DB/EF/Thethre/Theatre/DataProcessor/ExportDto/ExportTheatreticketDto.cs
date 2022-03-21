using System;
using System.Collections.Generic;
using System.Text;

namespace Theatre.DataProcessor.ExportDto
{
  public  class ExportTheatreticketDto
    {
        //  "Tickets": [
        //{
        //  "Price": 93.48,
        //  "RowNumber": 3
        //},
        public decimal Price { get; set; }
        public sbyte RowNumber { get; set; }
    }
}
