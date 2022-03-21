using System;
using System.Collections.Generic;
using System.Text;

namespace SoftJail.DataProcessor.ExportDto
{
   public class ExportPrisonersDto
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public string IncarcerationDate { get; set; }
        public List<ExportPrisonersMessegesDto> Mails { get; set; }
    }
}
