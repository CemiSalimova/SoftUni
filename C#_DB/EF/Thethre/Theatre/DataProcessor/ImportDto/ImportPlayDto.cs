using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Theatre.DataProcessor.ImportDto
{ [XmlType("Play")]
   public class ImportPlayDto
    {
        //      <Play>
        //  <Title>The Hsdfoming</Title>
        //  <Duration>03:40:00</Duration>
        //  <Rating>8.2</Rating>
        //  <Genre>Action</Genre>
        //  <Description>A guyat Pinter turns into a debatable conundrum as oth ordinary and menacing. Much of this has to do with the fabled "Pinter Pause," which simply mirrors the way we often respond to each other in conversation, tossing in remainders of thoughts on one subject well after having moved on to another.</Description>
        //  <Screenwriter>Roger Nciotti</Screenwriter>
        //</Play>
        [XmlElement("Title")]
        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [XmlElement("Duration")]
        
        //[Range(typeof(TimeSpan),"1","24")]
        public string Duration { get; set; }

        [XmlElement("Rating")]
        [Required]
        [Range(typeof(float), "0.00", "79228162514264337593543950335")]
        public float Rating { get; set; }

        [XmlElement("Genre")]
        [Required]
        public string Genre { get; set; }
        [XmlElement("Description")]
        [Required]
        [MinLength(1)]
        [MaxLength(700)]
        public string Description { get; set; }

        [XmlElement("Screenwriter")]
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Screenwriter { get; set; }

    }
}
