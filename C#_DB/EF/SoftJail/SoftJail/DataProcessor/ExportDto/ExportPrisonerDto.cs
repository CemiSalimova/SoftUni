using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SoftJail.DataProcessor.ExportDto
{
 public   class ExportPrisonerDto
    {
        //        "Id": 3,
        //"Name": "Binni Cornhill",
        //"CellNumber": 503,
        //"Officers": [
        //  {
        //    "OfficerName": "Hailee Kennon",
        //    "Department": "ArtificialIntelligence"
        //  },
        //  {
        //    "OfficerName": "Theo Carde",
        //    "Department": "Blockchain"
        //  }
        //],
        //"TotalOfficerSalary": 7127.93
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [Range(0,1000)]
        public int CellNumber { get; set; }
        public List<ExportPrisonerOfficerDto> Officers { get; set; }
        public decimal TotalOfficerSalary { get; set; }
    }
}
