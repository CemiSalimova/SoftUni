using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SoftJail.DataProcessor.ExportDto
{
   public class ExportPrisonerOfficerDto
    {//"Officers": [
        //  {
        //    "OfficerName": "Hailee Kennon",
        //    "Department": "ArtificialIntelligence"
        //  },

            [Required]
            [MinLength(3)]
            [MaxLength(30)]
        public string OfficerName { get; set; }

        [Required]
        public string Department { get; set; }
    }
}

