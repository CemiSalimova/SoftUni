using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VaporStore.DataProcessor.Dto.Import
{
    public class ImportUserCard
    {
        //"Cards": [
        //  {
        //    "Number": "1111 1111 1111 1111",
        //    "CVC": "111",
        //    "Type": "Debit"
        //  }

        [Required]
        [RegularExpression(@"^\d{4}\s\d{4}\s\d{4}\s\d{4}$")]
        public string Number { get; set; }

        [Required]
        [RegularExpression(@"^\d{3}$")]
        public string CVC { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
