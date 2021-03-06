using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace VaporStore.DataProcessor.Dto.Export
{
    [XmlType("Purchase")]
    public class ExportUserPurchaseDto
    {
       [XmlElement("Card")]
        public string CardNumber { get; set; }

        [XmlElement("Cvc")]
        public string Cvc { get; set; }

        [XmlElement("Date")]
        public string PurchaseDate { get; set; }

        [XmlElement("Game")]
        public ExportUserPurchaseGameDto Game { get; set; }
    }
}
