using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace VaporStore.DataProcessor.Dto.Import
{
    [XmlType("Purchase")]
    public class ImportPurchaseDto
    {
        //      <Purchase title = "Dungeon Warfare 2" >
        //  < Type > Digital </ Type >
        //  < Key > ZTZ3 - 0D2S-G4TJ</Key>
        //  <Card>1833 5024 0553 6211</Card>
        //  <Date>07/12/2016 05:49</Date>
        //</Purchase>

        [XmlAttribute("title")]
        [Required]
        public string GameTitle { get; set; }
        [XmlElement("Type")]
        [Required]
        public string Type { get; set; }

        [XmlElement("Key")]
        [Required]
        [RegularExpression(@"^[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}$")]
        public string Key { get; set; }

        [XmlElement("Date")]
        [Required]
        public string Date { get; set; }
      
        [XmlElement("Card")]
        [Required]
        [RegularExpression(@"^\d{4}\s\d{4}\s\d{4}\s\d{4}$")]
        public string CardNumber { get; set; }
       
      
    }
}
