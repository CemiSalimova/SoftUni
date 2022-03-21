using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VaporStore.DataProcessor.Dto.Import
{
    public class ImportUser
    {
        //    "FullName": "",
        //"Username": "invalid",
        //"Email": "invalid@invalid.com",
        //"Age": 20,
        //"Cards": [
        //  {
        //    "Number": "1111 1111 1111 1111",
        //    "CVC": "111",
        //    "Type": "Debit"
        //  }
        //]
        //nb!

        [Required]
        [RegularExpression(@"^[A-Z]{1}[a-z]{1,}\s[A-Z]{1}[a-z]{1,}$")]
        public string FullName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Range(3, 103)]
        public int Age { get; set; }

        public List<ImportUserCard> Cards { get; set; }
    }
}
