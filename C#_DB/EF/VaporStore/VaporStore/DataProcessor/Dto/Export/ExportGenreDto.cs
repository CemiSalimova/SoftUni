using System;
using System.Collections.Generic;
using System.Text;

namespace VaporStore.DataProcessor.Dto.Export
{
   public class ExportGenreDto
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; }
        //nb!
        public List<ExportGenreGameDto> Games { get; set; }
    }
}
