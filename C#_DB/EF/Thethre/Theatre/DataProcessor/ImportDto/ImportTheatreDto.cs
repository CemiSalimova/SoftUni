using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Theatre.DataProcessor.ImportDto
{
 public   class ImportTheatreDto
    {
        //    {
        //"Name": "",
        //"NumberOfHalls": 7,
        //"Director": "Ulwin Mabosty",
        //"Tickets": [
        //  {
        //    "Price": 7.63,
        //    "RowNumber": 5,
        //    "PlayId": 4
        //  },

        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [Range(typeof(sbyte), "0", "10")]
        public sbyte NumberOfHalls { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Director { get; set; }
        public List<ImportTheatreTicketDto> Tickets { get; set; }
    }
}
