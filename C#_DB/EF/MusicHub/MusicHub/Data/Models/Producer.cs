using System;
using System.Collections.Generic;

namespace MusicHub.Data.Models
{
    public partial class Producer
    {
        public Producer()
        {
            Albums = new HashSet<Album>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Pseudonym { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}
